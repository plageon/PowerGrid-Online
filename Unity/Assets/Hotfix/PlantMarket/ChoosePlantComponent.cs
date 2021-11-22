using System.Collections.Generic;
using ETModel;
using NPOI.SS.Formula.Functions;
using UnityEngine;
using UnityEngine.UI;

namespace ETHotfix
{
    [ObjectSystem]
    public class ChoosePlantComponentAwakeSystem: AwakeSystem<ChoosePlantComponent>
    {
        public override void Awake(ChoosePlantComponent self)
        {
            self.Awake();
        }
    }
    public class ChoosePlantComponent:Component
    {
        private List<Button> buttons;
        private Button notInterested;
        public void Awake()
        {
            this.buttons = new List<Button>();
            ReferenceCollector rc = this.GetParent<UI>().GameObject.GetComponent<ReferenceCollector>();
            this.notInterested = rc.Get<GameObject>("NotInterested").GetComponent<Button>();
            this.notInterested.onClick.Add((() => this.NotInterested()));
            for (int i = 0; i < 4; i++)
            {
                this.buttons.Add(rc.Get<GameObject>("Choose"+(i+1).ToString()).GetComponent<Button>());
                int i1 = i;
                this.buttons[i].onClick.Add((() => ChoosePlant(i1)));
            }
            PlantMarketComponent plantMarketComponent = 
                    Game.Scene.GetComponent<UIComponent>().Get(UIType.PlantMarket).GetComponent<PlantMarketComponent>();
            
            plantMarketComponent.warningText.text = "Select a Plant or Pass";
            
        }

        public async ETVoid ChoosePlant(int index)
        {
            C2M_RefreshMarket c2MRefreshMarket = new C2M_RefreshMarket();
            PlantMarketComponent plantMarketComponent = 
                    Game.Scene.GetComponent<UIComponent>().Get(UIType.PlantMarket).GetComponent<PlantMarketComponent>();
            int plantId = plantMarketComponent.plantIds[index];
            plantMarketComponent.currentPlantId = plantId;
            
            c2MRefreshMarket.ReplacePlant = plantId;
            M2C_RefreshMarket m2CRefreshMarket= (M2C_RefreshMarket)await SessionComponent.Instance.Session.Call(c2MRefreshMarket);
            for(int i=0;i<8;i++)
            {
                plantMarketComponent.plantIds[i] = m2CRefreshMarket.MarketPlants[i];
            }
            PlantConfig plantConfig = Game.Scene.GetComponent<ConfigComponent>().Get(typeof (PlantConfig), plantId) as PlantConfig;
            plantMarketComponent.minValue = plantConfig.Cost;
            plantMarketComponent.RefreshPool();
            plantMarketComponent.bidEnable = true;
            plantMarketComponent.warningText.text = "Make a bid or Pass";
            //退出选择电厂界面
            Game.EventSystem.Run(EventIdType.ChoosePlantFinish);
        }

        public async ETVoid NotInterested()
        {
            C2M_RefreshMarket c2MRefreshMarket = new C2M_RefreshMarket();
            c2MRefreshMarket.ReplacePlant = 0;
            M2C_RefreshMarket m2CRefreshMarket = (M2C_RefreshMarket) await SessionComponent.Instance.Session.Call(c2MRefreshMarket);
            Game.EventSystem.Run(EventIdType.ChoosePlantFinish);
        }
    }
}