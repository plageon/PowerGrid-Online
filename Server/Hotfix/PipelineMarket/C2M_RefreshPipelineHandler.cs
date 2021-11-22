using System;
using System.Collections.Generic;
using System.Text;
using ETModel;

namespace ETHotfix
{
    [MessageHandler(AppType.AllServer)]
    class C2M_RefreshPipelineHandler : AMHandler<C2M_RefreshPipelineMarket>
    {
        protected override async ETTask Run(Session session, C2M_RefreshPipelineMarket message)
        {
            PlayerComponent playerComponent = Game.Scene.GetComponent<PlayerComponent>();
            if (session.GetComponent<SessionPlayerComponent>().Player.Id == playerComponent.playerOrders[0].Id)
            {
                session.Send(new M2C_LayingCircuitReminder() { Message = "your turn to lay circuit" });
            }
            await ETTask.CompletedTask;
        }
    }
}
