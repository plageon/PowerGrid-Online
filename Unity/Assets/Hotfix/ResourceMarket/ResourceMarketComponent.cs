using System.Collections.Generic;
using ETModel;
using NPOI.SS.Formula.Functions;
using UnityEngine;
using UnityEngine.UI;
using Text = UnityEngine.UI.Text;

namespace ETHotfix
{
    [ObjectSystem]
    public class ResourceMarketComponentAwakeSystem: AwakeSystem<ResourceMarketComponent>
    {
        public override void Awake(ResourceMarketComponent self)
        {
            self.Awake();
        }
    }
    public class ResourceMarketComponent:Component
    {
        private Slider coalSlider;
        private Slider oilSlider;
        private Slider garbageSlider;
        private Slider nuclearSlider;
        private Button ConfirmButton;
        public int coalMarket;
        public int oilMarket;
        public int nuclearMarket;
        public int garbageMarket;
        private bool enableConfirm;
        private Text totalCostText;
        private Text WarningText;
        public void Awake()
        {
            Game.Scene.GetComponent<UIComponent>().Get(UIType.Status).GetComponent<StatusComponent>().UpdateStatus();
            ReferenceCollector rc = this.GetParent<UI>().GameObject.GetComponent<ReferenceCollector>();
            this.coalSlider = rc.Get<GameObject>("CoalSlider").GetComponent<Slider>();
            this.oilSlider = rc.Get<GameObject>("OilSlider").GetComponent<Slider>();
            this.garbageSlider = rc.Get<GameObject>("GarbageSlider").GetComponent<Slider>();
            this.nuclearSlider = rc.Get<GameObject>("NuclearSlider").GetComponent<Slider>();
            this.ConfirmButton = rc.Get<GameObject>("Confirm").GetComponent<Button>();
            this.totalCostText = rc.Get<GameObject>("Total").GetComponent<Text>();
            this.WarningText = rc.Get<GameObject>("Warning").GetComponent<Text>();
            this.ConfirmButton.onClick.Add(()=>this.Confirm());
            this.enableConfirm = false;
            
        }

        public async ETVoid Confirm()
        {
            if (this.enableConfirm)
            {
                Player player= PlayerComponent.Instance.MyPlayer;
                int totCost = this.CalculateCoalCost() + this.CalculateOilCost() + this.CalculateGarbageCost() + this.CalculateNuclearCost();
                this.totalCostText.text = "Total Cost: " + totCost.ToString();
                if (totCost > player.Money)
                {
                    this.WarningText.text = "You don't have enough money";
                }
                else
                {
                    C2M_BuyResourceRequest c2MBuyResourceRequest = new C2M_BuyResourceRequest();
                    c2MBuyResourceRequest.Coal = (int) this.coalSlider.value;
                    c2MBuyResourceRequest.Oil = (int) this.oilSlider.value;
                    c2MBuyResourceRequest.Garbage = (int) this.garbageSlider.value;
                    c2MBuyResourceRequest.Nuclear = (int) this.nuclearSlider.value;
                    M2C_BuyResourceResponse m2CBuyResourceResponse = (M2C_BuyResourceResponse)await SessionComponent.Instance.Session.Call(c2MBuyResourceRequest);
                
                    if (m2CBuyResourceResponse.Message == "buy resource success")
                    {
                        Debug.Log("totCost "+totCost);
                        player.Money -= totCost;
                        player.Resources["Coal"] += (int) this.coalSlider.value;
                        player.Resources["Oil"] += (int) this.oilSlider.value;
                        player.Resources["Garbage"] += (int) this.garbageSlider.value;
                        player.Resources["Nuclear"] += (int) this.nuclearSlider.value;
                        Game.Scene.GetComponent<UIComponent>().Get(UIType.Status).GetComponent<StatusComponent>().UpdateStatus();
                        this.enableConfirm = false;
                        this.WarningText.text = "you have bought resource";
                    }
                }
                
            }
            else
            {
                this.WarningText.text = "Not your turn";
            }
            await ETTask.CompletedTask;
        }

        public void ResetSlider()
        {
            int coalRequired = 0;
            int oilRequired = 0;
            int garbageRequired = 0;
            int nuclearRequired = 0;
            // this.coalMarket = coal;
            // this.oilMarket = oil;
            // this.garbageMarket = garbage;
            // this.nuclearMarket = nuclear;
            //Debug.Log("reset slider");
            //Debug.Log(PlayerComponent.Instance.MyPlayer.OwnPlants.ToString());
            foreach (int id in PlayerComponent.Instance.MyPlayer.OwnPlants.Keys)
            {
                PlantConfig plantConfig = Game.Scene.GetComponent<ConfigComponent>().Get(typeof (PlantConfig), id) as PlantConfig;
                Debug.Log(plantConfig.Cost+plantConfig.ResourceType+plantConfig.ResourceCost.ToString());
                if (plantConfig.ResourceType == "Oil")
                {
                    oilRequired += plantConfig.ResourceCost;
                }
                else if (plantConfig.ResourceType == "Garbage")
                {
                    garbageRequired += plantConfig.ResourceCost;
                }
                else if (plantConfig.ResourceType == "Coal")
                {
                    coalRequired += plantConfig.ResourceCost;
                }
                else if (plantConfig.ResourceType == "Nuclear")
                {
                    nuclearRequired += plantConfig.ResourceCost;
                }
                else if (plantConfig.ResourceType == "Coal;Oil")
                {
                    coalRequired += plantConfig.ResourceCost;
                    oilRequired += plantConfig.ResourceCost;
                }
            }

            this.coalSlider.maxValue = this.MinValue(coalRequired, this.coalMarket) * 2;
            this.oilSlider.maxValue = this.MinValue(oilRequired, this.oilMarket) * 2;
            this.garbageSlider.maxValue = this.MinValue(garbageRequired, this.garbageMarket) * 2;
            this.nuclearSlider.maxValue = this.MinValue(nuclearRequired, this.nuclearMarket) * 2;
            this.enableConfirm = true;
            this.WarningText.text = "Now you can buy resource";
        }

        private int MinValue(int a, int b)
        {
            if (a < b)
            {
                return a;
            }
            else
            {
                return b;
            }
        }

        private int CalculateCoalCost()
        {
            int totCost = 0;
            int currentSlot = 8 - this.coalMarket / 3;
            int numInSlot = this.coalMarket % 3;
            for (int i = 0; i < this.coalSlider.value; i++)
            {
                if (numInSlot == 0)
                {
                    currentSlot++;
                }

                numInSlot--;
                totCost += currentSlot;
            }

            return totCost;
        }

        private int CalculateOilCost()
        {
            int totCost = 0;
            int currentSlot = 8 - this.oilMarket / 3;
            int numInSlot = this.oilMarket % 3;
            for (int i = 0; i < this.oilSlider.value; i++)
            {
                if (numInSlot == 0)
                {
                    currentSlot++;
                }

                numInSlot--;
                totCost += currentSlot;
            }

            return totCost;
        }
        private int CalculateGarbageCost()
        {
            int totCost = 0;
            int currentSlot = 8 - this.garbageMarket / 3;
            int numInSlot = this.garbageMarket % 3;
            for (int i = 0; i < this.garbageSlider.value; i++)
            {
                if (numInSlot == 0)
                {
                    currentSlot++;
                }

                numInSlot--;
                totCost += currentSlot;
            }

            return totCost;
        }

        private int CalculateNuclearCost()
        {
            int totCost = 0;
            int currentSlot = 12 - this.nuclearMarket;
            for (int i = 0; i < this.nuclearSlider.value; i++)
            {
                if (currentSlot >= 8)
                {
                    currentSlot += 2;
                }
                else
                {
                    currentSlot += 1;
                }

                totCost += currentSlot;
            }

            return totCost;
        }
        public void PrintComponent()
        {
            Debug.Log("this is a resourcemarket component");
        }
    }
}