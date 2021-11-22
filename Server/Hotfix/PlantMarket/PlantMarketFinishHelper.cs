using ETModel;
using System;
using System.Collections.Generic;
using System.Threading;

namespace ETHotfix
{
    
    public static class PlantMarketFinishHelper
    {
        

        public static void PlantMarketFinish()
        {
            PlayerComponent playerComponent = Game.Scene.GetComponent<PlayerComponent>();
            Player[] players = playerComponent.GetAll();
            foreach (Player p in players)
            {
                p.UniqueSession.Send(new M2C_FinishAuction());
            }
            
            Session firstSession = players[0].UniqueSession;
            //firstSession.Send(new M2C_ResourceReminder());
            playerComponent.NowOrder = 0;
            //Game.EventSystem.Run(EventIdType.SendFirstResourceReminder);
            
        }
        
                            
    }
}
