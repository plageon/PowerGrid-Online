using ETModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETHotfix
{
    
    public static class ResourceMarketFinishHelper
    {
        public static void ResourceMarketFinish()
        {
            M2C_ResourceMarketFinish m2C_ResourceMarketFinish = new M2C_ResourceMarketFinish();
            m2C_ResourceMarketFinish.Message = "resource market finish";
            PlayerComponent playerComponent = Game.Scene.GetComponent<PlayerComponent>();
            Player[] players = playerComponent.GetAll();
            playerComponent.NowOrder = 0;
            foreach (Player p in players)
            {
                p.UniqueSession.Send(m2C_ResourceMarketFinish);
            }
        }
    }
}
