using ETModel;
using UnityEngine;

namespace ETHotfix
{
    [MessageHandler]
    public class M2C_AuctionReminderHandler:AMHandler<M2C_AuctionReminder>
    {
        protected override async ETTask Run(ETModel.Session session, M2C_AuctionReminder message)
        {
            UI plantMarketUI=Game.Scene.GetComponent<UIComponent>().Get(UIType.PlantMarket);
            PlantMarketComponent plantMarketComponent = plantMarketUI.GetComponent<PlantMarketComponent>();
            if (message.IsFinished)
            {
                await plantMarketComponent.RefreshPoolFromServer();
                
            }
            else
            {
                plantMarketComponent.minValue = message.NowPrice+1;
                plantMarketComponent.maxValue = message.NowPrice * 2;
                plantMarketComponent.currentPlantId = message.NowPlantId;
                plantMarketComponent.ResetCurrentPlant();
                plantMarketComponent.bidEnable = true;
                plantMarketComponent.warningText.text = "Make a Bid or Pass";
            }
            
            await ETTask.CompletedTask;
        }
    }
}