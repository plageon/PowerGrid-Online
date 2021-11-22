using ETModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETHotfix
{
    public static class M2C_FinishRoundHelper
    {
        public static void RoundFinish()
        {
            PlayerComponent playerComponent = Game.Scene.GetComponent<PlayerComponent>();
            M2C_FinishRound m2C_FinishRound = new M2C_FinishRound();
            m2C_FinishRound.Message = "round" + playerComponent.RoundCount.ToString() + " finished";
            playerComponent.RoundCount++;
            playerComponent.isNewRound = true;
            playerComponent.NowOrder = 0;
            playerComponent.FinishCount = 0;
            SetPlayerOrder(playerComponent.playerOrders.Count);
            AddResource(playerComponent.playerOrders.Count);
            foreach (Player player1 in playerComponent.playerOrders.Values)
            {
                player1.HasBoughtOrQuit = false;
                player1.IsPassed = false;
            }
            foreach (Player player in playerComponent.playerOrders.Values)
            {
                player.UniqueSession.Send(m2C_FinishRound);
            }
        }

        public static void AddResource(int playerNum)
        {
            ResourceMarket resourceMarket = Game.Scene.GetComponent<PlayerComponent>().resourceMarket;
            if (playerNum == 2)
            {
                resourceMarket.coal += 3;
                resourceMarket.oil += 2;
                resourceMarket.garbage += 1;
                resourceMarket.nuclear += 1;
            }
            if (playerNum == 3)
            {
                resourceMarket.coal += 4;
                resourceMarket.oil += 2;
                resourceMarket.garbage += 1;
                resourceMarket.nuclear += 1;
            }
            if (playerNum == 4)
            {
                resourceMarket.coal += 5;
                resourceMarket.oil += 3;
                resourceMarket.garbage += 2;
                resourceMarket.nuclear += 1;
            }
        }
        public static void SetPlayerOrder(int playerNum)//发电量越少的人在越前面
        {
            PlayerComponent playerComponent = Game.Scene.GetComponent<PlayerComponent>();
            Dictionary<int, Player> allPlayers = new Dictionary<int, Player>(playerComponent.playerOrders);
            
            playerComponent.playerOrders.Clear();
            for (int i = 0; i < playerNum; i++)
            {
                int minPowerCity = 21;
                Player minPowerCityPlayer = new Player();
                foreach(Player player in allPlayers.Values)
                {
                    if (player.powerCity < minPowerCity)
                    {
                        minPowerCity = player.powerCity;
                        minPowerCityPlayer = player;
                    }
                }
                playerComponent.playerOrders.Add(i, minPowerCityPlayer);
            }
        }
    }
}
