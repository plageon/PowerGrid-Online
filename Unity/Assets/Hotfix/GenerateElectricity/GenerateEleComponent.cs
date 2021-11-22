using System.Collections.Generic;
using System.IO;
using ETModel;
using NPOI.OpenXmlFormats.Wordprocessing;
using UnityEngine;
using UnityEngine.UI;

namespace ETHotfix
{
    [ObjectSystem]
    public class GenerateEleComponentAwakeSystem: AwakeSystem<GenerateEleComponent>
    {
        public override void Awake(GenerateEleComponent self)
        {
            self.Awake();
        }
    }
    public class GenerateEleComponent:Component
    {
        private Dictionary<int, int> RewardList;
        private Button Confirm;
        private Text Warning;
        private Toggle UsePlant1;
        private Toggle UsePlant2;
        private Toggle UsePlant3;
        private Toggle UseOil1;
        private Toggle UseOil2;
        private Toggle UseOil3;
        private List<PlantConfig> plants;
        public void Awake()
        {
            this.CreateRewardList();
            Game.Scene.GetComponent<UIComponent>().Get(UIType.Status).GetComponent<StatusComponent>().UpdateStatus();
            ReferenceCollector rc = this.GetParent<UI>().GameObject.GetComponent<ReferenceCollector>();
            this.Confirm=rc.Get<GameObject>("Confirm").GetComponent<Button>();
            this.Warning=rc.Get<GameObject>("Warning").GetComponent<Text>();
            this.UsePlant1=rc.Get<GameObject>("Toggle1").GetComponent<Toggle>();
            this.UsePlant2=rc.Get<GameObject>("Toggle2").GetComponent<Toggle>();
            this.UsePlant3=rc.Get<GameObject>("Toggle3").GetComponent<Toggle>();
            this.UseOil1=rc.Get<GameObject>("Toggle4").GetComponent<Toggle>();
            this.UseOil2=rc.Get<GameObject>("Toggle5").GetComponent<Toggle>();
            this.UseOil3=rc.Get<GameObject>("Toggle6").GetComponent<Toggle>();
            Player player = PlayerComponent.Instance.MyPlayer;
            this.plants = new List<PlantConfig>();
            int i = 0;
            foreach (int plantId in player.OwnPlants.Keys)
            {
                //Log.Debug(i.ToString());
                i++;
                Image image=rc.Get<GameObject>("Image"+i.ToString()).GetComponent<Image>();
                PlantConfig plantConfig = Game.Scene.GetComponent<ConfigComponent>().Get(typeof (PlantConfig), plantId) as PlantConfig;
                plants.Add(plantConfig);
                string imgPath = Application.dataPath+"/Resources/"+plantConfig.Cost.ToString() + ".png";
                //Debug.Log(imgPath);
                byte[] imgByte = File.ReadAllBytes(imgPath);
                Texture2D texture2D = new Texture2D(1, 1);
                texture2D.LoadImage(imgByte);
                Sprite sprite = Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height), new Vector2(0, 0));
                image.sprite = sprite;
            }
            this.Confirm.onClick.Add(()=>this.ConfirmSelection());
            this.Warning.text = "Select plants to generate electricity";
            
        }

        private async ETVoid ConfirmSelection()
        {
            //Debug.Log("confirm");
            Player player = PlayerComponent.Instance.MyPlayer;
            if (isValid(this.UsePlant1, this.UseOil1, 0) && isValid(this.UsePlant2, this.UseOil2, 1) && isValid(this.UsePlant3, this.UseOil3, 2))
            {
                int totReward = CalulateReward(this.UsePlant1, this.UseOil1, 0) + CalulateReward(this.UsePlant2, this.UseOil2, 1) +
                        CalulateReward(this.UsePlant3, this.UseOil3, 2);
                if (player.OwnCities.Count < totReward)
                {
                    totReward = player.OwnCities.Count;
                }
                
                C2M_GenerateEleRequest c2MGenerateEleRequest = new C2M_GenerateEleRequest();
                c2MGenerateEleRequest.Message = "generate electricity";
                c2MGenerateEleRequest.TotReward = totReward;
                M2C_GenerateEleResponse m2CGenerateEleResponse =
                        (M2C_GenerateEleResponse) await SessionComponent.Instance.Session.Call(c2MGenerateEleRequest);
                if (m2CGenerateEleResponse.Message == "success")
                {
                    player.Money += this.RewardList[totReward];
                    ConsumeResource(this.UsePlant1, this.UseOil1, 0);
                    ConsumeResource(this.UsePlant2, this.UseOil2, 1);
                    ConsumeResource(this.UsePlant3, this.UseOil3, 2);
                    Game.Scene.GetComponent<UIComponent>().Get(UIType.Status).GetComponent<StatusComponent>().UpdateStatus();
                }
                //Game.EventSystem.Run(EventIdType.GenerateEleFinish);
            }
            else
            {
                this.Warning.text = "You don't have enough resource";
            }
        }

        private bool isValid(Toggle UsePlant,Toggle UseOil,int index)
        {
            //Debug.Log("isvalid"+index);
            if (this.plants.Count <= index||!UsePlant.isOn||this.plants[index].ResourceType=="Clean")
            {
                return true;
            }
            string resourceType = this.plants[index].ResourceType;
            if (this.plants[index].ResourceType == "Coal;Oil")
            {
                if (UseOil.isOn)
                {
                    resourceType = "Oil";
                }
                else
                {
                    resourceType = "Coal";
                }
            }

            if (PlayerComponent.Instance.MyPlayer.Resources[resourceType] < this.plants[index].ResourceCost)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private int CalulateReward(Toggle UsePlant,Toggle UseOil,int index)
        {
            //Debug.Log("calculate"+index);
            if (this.plants.Count <= index||!UsePlant.isOn)
            {
                return 0;
            }

            return this.plants[index].Output;
        }

        private void ConsumeResource(Toggle UsePlant,Toggle UseOil,int index)
        {
            //Debug.Log("consume"+index);
            if (this.plants.Count <= index||!UsePlant.isOn||this.plants[index].ResourceType=="Clean")
            {
                return;
            }
            string resourceType = this.plants[index].ResourceType;
            if (this.plants[index].ResourceType == "Coal;Oil")
            {
                if (UseOil.isOn)
                {
                    resourceType = "Oil";
                }
                else
                {
                    resourceType = "Coal";
                }
            }
            

            PlayerComponent.Instance.MyPlayer.Resources[resourceType] -= this.plants[index].ResourceCost;
        }

        private void CreateRewardList()
        {
            this.RewardList = new Dictionary<int, int>()
            {
                { 0, 10 },
                { 1, 22 },
                { 2, 33 },
                { 3, 44 },
                { 4, 54 },
                { 5, 64 },
                { 6, 73 },
                { 7, 82 },
                { 8, 90 },
                { 9, 98 },
                { 10, 105 },
                { 11, 112 },
                { 12, 118 },
                { 13, 124 },
                { 14, 129 },
                { 15, 134 },
                { 16, 138 },
                { 17, 142 },
                { 18, 145 },
                { 19, 148 },
                { 20, 150 }
            };
        }
    }
}