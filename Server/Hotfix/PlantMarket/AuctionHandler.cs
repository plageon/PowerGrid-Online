using ETModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETHotfix
{
    [MessageHandler(AppType.AllServer)]
    public class AuctionHandler : AMRpcHandler<C2M_AuctionRequest, M2C_AuctionResponse>
    {
        protected override async ETTask Run(Session session, C2M_AuctionRequest request, M2C_AuctionResponse response, Action reply)
        {
            PlayerComponent playerComponent = Game.Scene.GetComponent<PlayerComponent>();
            Player sessionPlayer = playerComponent.Get(session.GetComponent<SessionPlayerComponent>().Player.Id);
            if (request.IsPassed)
            {
                sessionPlayer.IsPassed = true;
                response.Message = "you chose to pass";
                //检查是否是倒数第二个玩家选择pass或者已购，如果是则通知客户端全体玩家竞拍完成，否则继续拍卖
                int playerInAuction = 0;
                Player[] players = playerComponent.GetAll();
                foreach(Player p in players)
                {
                    if (p.IsPassed || p.HasBoughtOrQuit) playerInAuction++;
                }
                if (playerInAuction == players.Length - 1)//获得电厂
                {
                    //将电厂添加到出价最高者信息里                  
                    Player maxPricePlayer= playerComponent.Get(playerComponent.MaxPriceId);
                    PlantConfig plant = Game.Scene.GetComponent<ConfigComponent>().Get(typeof(PlantConfig), request.PlantId) as PlantConfig;
                    //提醒客户端放弃电厂
                    if (maxPricePlayer.OwnPlants.Count >= 3)
                    {                                        
                        C2M_GiveupPlant giveupresponse=await maxPricePlayer.UniqueSession.Call(new M2C_GiveupPlant() { Message = "you have to give up one" }) as C2M_GiveupPlant;
                        maxPricePlayer.OwnPlants.Remove(giveupresponse.GiveupPlantId);
                    }
                    maxPricePlayer.OwnPlants.Add(request.PlantId, plant.Pic);
                    maxPricePlayer.HasBoughtOrQuit = true;
                    
                    
                    //检查是否所有人已购或退出本轮购买
                    //并试图开启新一轮竞拍(新处理器和消息体)
                    //通知下一个玩家
                    if (CountFreePlayer() == 0)
                    {
                        OwnPlantHelper.SendM2C_OwnPlant(maxPricePlayer, request.PlantId, true);
                        reply();
                        //广播竞拍全部结束的消息
                        PlantMarketFinishHelper.PlantMarketFinish();
                    }
                    else
                    {
                        //告诉所有玩家本轮竞拍完成
                        
                        foreach (Player p in players)
                        {
                            OwnPlantHelper.SendM2C_OwnPlant(p, request.PlantId, false);
                        }
                        playerComponent.MaxPrice = 0;
                        playerComponent.MaxPriceId = 0;
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
                        //choosePlantPlayer.UniqueSession.Send(new M2C_InitAuction());
                        M2C_InitAuctionHelper.SendInit(choosePlantPlayer.UniqueSession);
                        reply();
                    }
                }
                else
                {
                    Player nextPlayer = new Player();
                    //给下一个有资格玩家发出可以拍卖的消息
                    do
                    {
                        playerComponent.AuctionOrder = (playerComponent.AuctionOrder + 1) % playerComponent.Count;
                        nextPlayer = playerComponent.playerOrders[playerComponent.AuctionOrder];
                    } while (nextPlayer.HasBoughtOrQuit);
                    nextPlayer.UniqueSession.Send(new M2C_AuctionReminder() { NowPlantId = request.PlantId, NowPrice = playerComponent.MaxPrice, Message = "Now it's your turn", IsFinished = false });
                    reply();
                }
                //M2C_AskBid m2C_AskBid = new M2C_AskBid();
                //m2C_AskBid.ActorId = players[0].Id;
                //MessageHelper.Broadcast(m2C_AskBid);
            }
            else
            {
                //服务器更新当前最高价格与出最高价的玩家id
                playerComponent.MaxPrice = request.Bid;
                playerComponent.MaxPriceId = sessionPlayer.Id;              
                response.Message = "Waiting";
                //给下一个有资格玩家发出可以拍卖的消息
                Player nextPlayer = new Player();
                if (CountFreePlayer() == 1)//假如只剩一个人在拍卖，直接获得
                {
                    Player freeplayer = playerComponent.Get(session.GetComponent<SessionPlayerComponent>().Player.Id);
                    PlantConfig plant = Game.Scene.GetComponent<ConfigComponent>().Get(typeof(PlantConfig), request.PlantId) as PlantConfig;
                    freeplayer.OwnPlants.Add(request.PlantId, plant.Pic);
                    freeplayer.HasBoughtOrQuit = true;
                    reply();

                    OwnPlantHelper.SendM2C_OwnPlant(freeplayer, request.PlantId, true);
                    PlantMarketFinishHelper.PlantMarketFinish();
                }
                else
                {
                    do
                    {
                        playerComponent.AuctionOrder = (playerComponent.AuctionOrder + 1) % playerComponent.Count;
                        nextPlayer = playerComponent.playerOrders[playerComponent.AuctionOrder];
                    } while (nextPlayer.HasBoughtOrQuit || nextPlayer.IsPassed);
                    nextPlayer.UniqueSession.Send(new M2C_AuctionReminder() { NowPlantId = request.PlantId, NowPrice = playerComponent.MaxPrice, Message = "Now it's your turn", IsFinished = false });
                    reply();
                }

            }
            
            await ETTask.CompletedTask;
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
