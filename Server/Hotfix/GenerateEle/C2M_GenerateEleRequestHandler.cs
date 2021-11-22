using System;
using System.Collections.Generic;
using System.Text;
using ETModel;

namespace ETHotfix
{
    [MessageHandler(AppType.AllServer)]
    class C2M_GenerateEleRequestHandler : AMRpcHandler<C2M_GenerateEleRequest,M2C_GenerateEleResponse>
    {
        
        protected override async ETTask Run(Session session, C2M_GenerateEleRequest request, M2C_GenerateEleResponse response, Action reply)
        {
            PlayerComponent playerComponent = Game.Scene.GetComponent<PlayerComponent>();
            playerComponent.FinishCount++;
            playerComponent.Get(session.GetComponent<SessionPlayerComponent>().Player.Id).powerCity = request.TotReward;
            response.Message = "success";
            reply();
            if (playerComponent.FinishCount == playerComponent.playerOrders.Count)
            {
                M2C_FinishRoundHelper.RoundFinish();
            }
            await ETTask.CompletedTask;
        }
    }
}
