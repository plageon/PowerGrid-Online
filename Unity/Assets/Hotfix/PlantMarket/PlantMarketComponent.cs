using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using ETModel;
using NPOI.SS.Formula.Functions;
using UnityEngine;
using UnityEngine.UI;
using Text = UnityEngine.UI.Text;

namespace ETHotfix
{
    [ObjectSystem]
    public class PlantMarketComponentAwakeSystem: AwakeSystem<PlantMarketComponent>
    {
        public override void Awake(PlantMarketComponent self)
        {
            //Debug.Log("PlantMarketComponent awake");
            self.Awake();
        }
    }
    
    public class PlantMarketComponent:Component
    {
        public bool makeBidOnly = false;
        public bool bidEnable = false;
        private Button passButton;
        private Button bidButton;
        private Slider slider;
        private List<Image> images;
        private Image currentImg;
        public Text priceText,warningText;
        public int minValue, maxValue, currentPlantId;
        public List<int> plantIds;
        public void Awake()
        {
            //appdomain.DelegateManager.RegisterMethodDelegate<Single>();
            Game.Scene.GetComponent<UIComponent>().Get(UIType.Status).GetComponent<StatusComponent>().UpdateStatus();
            this.plantIds = new List<int>();
            this.images = new List<Image>();
            ReferenceCollector rc = this.GetParent<UI>().GameObject.GetComponent<ReferenceCollector>();
            //Debug.Log("PlantMarketComponent awake");

            //添加事件
            this.passButton = rc.Get<GameObject>("Pass").GetComponent<Button>();
            this.bidButton = rc.Get<GameObject>("Buy").GetComponent<Button>();
            this.passButton.onClick.Add(() => this.PassRound());
            this.bidButton.onClick.Add(() => this.MakeBid());
            slider = rc.Get<GameObject>("Slider").GetComponent<Slider>();
            //this.slider.onValueChanged.AddListener(delegate {ShowPrice();});
            this.currentImg = rc.Get<GameObject>("Image").GetComponent<Image>();
            this.priceText = rc.Get<GameObject>("PriceText").GetComponent<Text>();
            this.warningText = rc.Get<GameObject>("Warning").GetComponent<Text>();
            for (int i = 0; i < 8; i++)
            {
                this.images.Add(rc.Get<GameObject>("Image" + (i+1).ToString()).GetComponent<Image>());
            }

            this.RefreshPoolFromServer();
        }

        public async ETVoid PassRound()
        {
            if (this.bidEnable)
            {
                if (this.makeBidOnly)
                {
                    this.warningText.text = "you must make a bid";
                }
                else
                {
                    this.bidEnable = false;
                    C2M_AuctionRequest c2MAuctionRequest= new C2M_AuctionRequest();
                    c2MAuctionRequest.IsPassed = true;
                    c2MAuctionRequest.Bid = 0;
                    c2MAuctionRequest.PlantId = this.currentPlantId;
                    M2C_AuctionResponse m2CAuctionResponse=(M2C_AuctionResponse)await SessionComponent.Instance.Session.Call(c2MAuctionRequest);
                    //Debug.Log(m2CAuctionResponse.Message);
                }
            }
            else
            {
                this.warningText.text = "Not Your Turn";
            }
            //Debug.Log("make a bid");
            
        }

        public async ETVoid MakeBid()
        {
            if (this.bidEnable)
            {
                // Debug.Log("id: "+ETModel.Game.Scene.GetComponent<PlayerComponent>().MyPlayer.Id+ETModel.Game.Scene.GetComponent<PlayerComponent>().MyPlayer.Money);
                // Debug.Log("id: "+PlayerComponent.Instance.MyPlayer.Id+PlayerComponent.Instance.MyPlayer.Money);
                if (this.slider.value <= PlayerComponent.Instance.MyPlayer.Money)
                {
                    this.bidEnable = false;
                    C2M_AuctionRequest c2MAuctionRequest= new C2M_AuctionRequest();
                    c2MAuctionRequest.IsPassed = false;
                    c2MAuctionRequest.Bid = (int) this.slider.value;
                    c2MAuctionRequest.PlantId = this.currentPlantId;
                    M2C_AuctionResponse m2CAuctionResponse=(M2C_AuctionResponse)await SessionComponent.Instance.Session.Call(c2MAuctionRequest);
                }
                else
                {
                    this.warningText.text = "You don't have enough money";
                }
                //Debug.Log(m2CAuctionResponse.Message);
                this.makeBidOnly = false;
            }
            else
            {
                this.warningText.text = "Not Your Turn";
            }
            //Debug.Log("pass");
            
        }

        public async ETVoid RefreshPoolFromServer()
        {
            //向服务端请求拍卖池的信息
            
            C2M_RefreshMarket c2MRefreshMarket = new C2M_RefreshMarket();
            //c2MRefreshMarket.ReplacePlant=
            c2MRefreshMarket.ReplacePlant = -1;
            M2C_RefreshMarket m2CRefreshMarket = (M2C_RefreshMarket)await SessionComponent.Instance.Session.Call(c2MRefreshMarket);
            
            //List<int> plantIds = new List<int>{ 1001, 1003, 1006, 1034, 1040, 1023, 1027, 1016 };
            this.plantIds.Clear();
            for (int i = 0; i < 8; i++)
            {
                this.plantIds.Add(m2CRefreshMarket.MarketPlants[i]);
            }

            
            this.currentPlantId = plantIds[0];
            this.RefreshPool();

            //this.currentPlantId = 6;
            
        }

        public void ResetCurrentPlant()
        {
            PlantConfig plantConfig = Game.Scene.GetComponent<ConfigComponent>().Get(typeof (PlantConfig), this.currentPlantId) as PlantConfig;
            string imgPath = Application.dataPath+"/Resources/"+plantConfig.Cost + ".png";
            byte[] imgByte = File.ReadAllBytes(imgPath);
            Texture2D texture2D = new Texture2D(1, 1);
            texture2D.LoadImage(imgByte);
            Sprite sprite = Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height), new Vector2(0, 0));
            this.currentImg.sprite = sprite;

            this.slider.minValue = this.minValue;
            this.slider.maxValue = this.minValue*2;
        }

        void PriceValue(float value)
        {
            this.priceText.text = (value is int? (int) value : 0).ToString();
        }

        public void ChoosePlant()
        {
            UI uiChoosePlant = ChoosePlantFactory.Create();
            Game.Scene.GetComponent<UIComponent>().Add(uiChoosePlant);
            uiChoosePlant.SetAsFirstSibling();
        }

        public void RefreshPool()
        {
            plantIds.Sort();
            this.ResetCurrentPlant();
            List<int> plantCost = new List<int>();
            for (int i = 0; i < 8; i++)
            {
                
                PlantConfig plantConfig = Game.Scene.GetComponent<ConfigComponent>().Get(typeof (PlantConfig), plantIds[i]) as PlantConfig;
                plantCost.Add(plantConfig.Cost);
                string imgPath = Application.dataPath+"/Resources/"+plantCost[i].ToString() + ".png";
                //Debug.Log(imgPath);
                byte[] imgByte = File.ReadAllBytes(imgPath);
                Texture2D texture2D = new Texture2D(1, 1);
                texture2D.LoadImage(imgByte);
                Sprite sprite = Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height), new Vector2(0, 0));
                this.images[i].sprite = sprite;
            }
        }

        public void ShowPrice()
        {
            this.priceText.text = this.slider.value.ToString();
        }
    }
}