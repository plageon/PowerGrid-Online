using ETModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETHotfix
{
    [MessageHandler(AppType.AllServer)]
    class C2M_BuyResourceRequestHandler : AMRpcHandler<C2M_BuyResourceRequest, M2C_AuctionResponse>
    {
        protected override async ETTask Run(Session session, C2M_BuyResourceRequest request, M2C_AuctionResponse response, Action reply)
        {
            PlayerComponent playerComponent = Game.Scene.GetComponent<PlayerComponent>();
            ResourceMarket resourceMarket = playerComponent.resourceMarket;
            resourceMarket.coal -= request.Coal;
            resourceMarket.oil -= request.Oil;
            resourceMarket.garbage -= request.Garbage;
            resourceMarket.nuclear -= request.Nuclear;
            response.Message = "buy resource success";
            playerComponent.NowOrder++;
            if (playerComponent.NowOrder < playerComponent.playerOrders.Count)
            {
                M2C_ResourceReminder m2C_ResourceReminder = new M2C_ResourceReminder();
                m2C_ResourceReminder.Message = "buy resource";
                m2C_ResourceReminder.Coal = playerComponent.resourceMarket.coal;
                m2C_ResourceReminder.Oil = playerComponent.resourceMarket.oil;
                m2C_ResourceReminder.Garbage = playerComponent.resourceMarket.garbage;
                m2C_ResourceReminder.Nuclear = playerComponent.resourceMarket.nuclear;
                playerComponent.playerOrders[playerComponent.NowOrder].UniqueSession.Send(m2C_ResourceReminder);
            }
            else
            {
                ResourceMarketFinishHelper.ResourceMarketFinish();
            }
            
            reply();
            await ETTask.CompletedTask;
        }
    }
}
