using ETModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETHotfix
{
    [MessageHandler(AppType.AllServer)]
    public class MarketHandler : AMRpcHandler<C2M_RefreshMarket, M2C_RefreshMarket>
    {
        protected override async ETTask Run(Session session, C2M_RefreshMarket request, M2C_RefreshMarket response, Action reply)
        {
            PlayerComponent playerComponent = Game.Scene.GetComponent<PlayerComponent>();
            int sessionPlayerId = (int)session.GetComponent<SessionPlayerComponent>().Player.Id;
            //如果是第一次请求市场则初始化并返回
            if (playerComponent.isNewRound)
            {
                if (playerComponent.Plantlists.Count == 0)
                {
                    //PlantConfig plantConfig = Game.Scene.GetComponent<ConfigComponent>().Get(typeof(PlantConfig), 1002) as PlantConfig;
                    //PlantConfig[] plants;
                    IConfig[] iconfigs = (IConfig[])Game.Scene.GetComponent<ConfigComponent>().GetAll(typeof(PlantConfig));

                    foreach (IConfig config in iconfigs)
                    {
                        PlantConfig plant = (PlantConfig)config;
                        playerComponent.Plantlists.Add(plant.Id, false);
                    }
                    for (int i = 0; i < 8; i++)
                    {
                        int random = RandomHelper.RandomNumber(1001, 1046);

                        while (playerComponent.Plantlists[random])
                        {
                            random = RandomHelper.RandomNumber(1001, 1046);
                        }
                        playerComponent.NowMarkets[i] = random;
                        response.MarketPlants.Add(random);
                        playerComponent.Plantlists[random] = true;
                    }
                    for (int k = 0; k < playerComponent.playerOrders.Count; k++)
                    {
                        if (sessionPlayerId == playerComponent.playerOrders[k].Id)
                        {
                            playerComponent.AuctionOrder = k;
                            break;
                        }
                    }    
                }
                else
                {
                    for (int i = 0; i < 8; i++)
                        response.MarketPlants.Add(playerComponent.NowMarkets[i]);
                }
                if (session.GetComponent<SessionPlayerComponent>().Player.Id == playerComponent.playerOrders[0].Id)
                {
                    M2C_InitAuctionHelper.SendInit(session);//第一个玩家直接开始拍卖
                    playerComponent.isNewRound = false;
                    int choosePlantPlayerOrder = 0;
                    Player choosePlantPlayer = new Player();
                    foreach (int k in playerComponent.playerOrders.Keys)
                    {
                        if (!playerComponent.playerOrders[k].HasBoughtOrQuit)
                        {
                            choosePlantPlayerOrder = k;
                            choosePlantPlayer = playerComponent.playerOrders[k];
                            break;
                        }
                    }
                    playerComponent.AuctionOrder = choosePlantPlayerOrder;
                }
                
                reply();
                await ETTask.CompletedTask;
            }
            else
            {
                if (request.ReplacePlant == -1)
                {
                    for (int i = 0; i < 8; i++)
                        response.MarketPlants.Add(playerComponent.NowMarkets[i]);
                    reply();
                }
                else if (request.ReplacePlant == 0)//被选中开启一轮竞拍的玩家可以通过置此项为0代表掠过本轮
                {
                    Player player = playerComponent.Get(session.GetComponent<SessionPlayerComponent>().Player.Id);
                    player.HasBoughtOrQuit = true;
                    //此时服务端应选取下一位起拍玩家
                    for (int i = 0; i < 8; i++)
                        response.MarketPlants.Add(playerComponent.NowMarkets[i]);
                    reply();
                    if (CountFreePlayer() == 0)
                    {
                        //广播竞拍全部结束的消息
                        PlantMarketFinishHelper.PlantMarketFinish();
                    }
                    else
                    {
                        int choosePlantPlayerId = 0;
                        Player choosePlantPlayer = new Player();
                        foreach (int k in playerComponent.playerOrders.Keys)
                        {
                            if (!playerComponent.playerOrders[k].HasBoughtOrQuit)
                            {
                                choosePlantPlayerId = k;
                                choosePlantPlayer = playerComponent.playerOrders[k];
                                break;
                            }
                        }
                        playerComponent.AuctionOrder = choosePlantPlayerId;

                        //choosePlantPlayer.UniqueSession.Send(new M2C_InitAuction());
                        M2C_InitAuctionHelper.SendInit(choosePlantPlayer.UniqueSession);
                    }
                    await ETTask.CompletedTask;
                }
                else//否则将上一轮买下（已传回）的电厂编号传来，消息回复更新后的市场
                {
                    int index = 0;
                    int random = RandomHelper.RandomNumber(1001, 1046);

                    while (index < 8 && (playerComponent.NowMarkets[index] != request.ReplacePlant)) index++;
                    while (playerComponent.Plantlists[random]) random = RandomHelper.RandomNumber(1001, 1046);

                    playerComponent.Plantlists[random] = true;
                    playerComponent.NowMarkets[index] = random;//用一个随机的替换选中的电厂
                    playerComponent.currentPlantId = request.ReplacePlant;

                    for (int i = 0; i < 8; i++)
                        response.MarketPlants.Add(playerComponent.NowMarkets[i]);
                    reply();
                    await ETTask.CompletedTask;
                }

            }
            
        }
        private int CountFreePlayer()
        {
            PlayerComponent playerComponent = Game.Scene.GetComponent<PlayerComponent>();
            int freePlayer = 0;
            foreach (Player player1 in playerComponent.playerOrders.Values)
            {
                if (!player1.HasBoughtOrQuit)
                {
                    freePlayer++;
                }
            }
            return freePlayer;
        }
    }
}
