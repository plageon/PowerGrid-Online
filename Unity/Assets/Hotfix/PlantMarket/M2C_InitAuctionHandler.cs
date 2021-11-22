using ETModel;
using UnityEngine;

namespace ETHotfix
{
    [MessageHandler]
    public class M2C_InitAuctionHandler:AMHandler<M2C_InitAuction>
    {
        protected override async ETTask Run(ETModel.Session session, M2C_InitAuction message)
        {
            Debug.Log("initauction received");
            PlantMarketComponent plantMarketComponent =
                    Game.Scene.GetComponent<UIComponent>().Get(UIType.PlantMarket).GetComponent<PlantMarketComponent>();
            //await plantMarketComponent.RefreshPoolFromServer();
            if (plantMarketComponent.plantIds.Count != 0)
            {
                for (int i = 1; i < message.MarketPlants.count; i++)
                {
                    plantMarketComponent.plantIds[i] = message.MarketPlants[i];
                }
                plantMarketComponent.RefreshPool();
            }
            plantMarketComponent.ChoosePlant();
            plantMarketComponent.makeBidOnly = true;
            await ETTask.CompletedTask;
        }
    }
}