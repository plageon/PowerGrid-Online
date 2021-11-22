using ETModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETHotfix
{
    public static class M2C_InitAuctionHelper
    {
        public static void SendInit(Session session)
        {
            M2C_InitAuction m2C_InitAuction = new M2C_InitAuction();
            foreach(int plantId in Game.Scene.GetComponent<PlayerComponent>().NowMarkets)
            {
                m2C_InitAuction.MarketPlants.Add(plantId);
            }
            session.Send(m2C_InitAuction);
        }

    }
}
