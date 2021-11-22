using System;
using System.Collections.Generic;
using System.Text;
using ETModel;
namespace ETHotfix
{
    [MessageHandler(AppType.AllServer)]

    class C2M_RefreshResourceMarketHandler : AMHandler<C2M_RefreshResourceMarket>
    {
        protected override async ETTask Run(Session session, C2M_RefreshResourceMarket message)
        {
            Game.EventSystem.Run(EventIdType.SendFirstResourceReminder);
            await ETTask.CompletedTask;
        }
    }
}
