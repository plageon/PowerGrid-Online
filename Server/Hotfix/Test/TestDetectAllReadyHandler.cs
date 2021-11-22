using ETModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETHotfix
{
    [MessageHandler(AppType.AllServer)]
    public class TestDetectAllReadyHandler:AMRpcHandler<M2M_TestGetReadyRequest,M2M_TestGetReadyResponse>
    {
        protected override async ETTask Run(Session session, M2M_TestGetReadyRequest request, M2M_TestGetReadyResponse response, Action reply)
        {
            Player[] players = Game.Scene.GetComponent<PlayerComponent>().GetAll();
            foreach(Player player in players)
            {
                if(!player.IsReady)
                {
                    response.Message = "NotReady";
                    break;
                }
            }
            response.Message = players.Length.ToString();
            reply();
            await ETTask.CompletedTask;
        }
    }
}
