using ETModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETHotfix
{
    [MessageHandler(AppType.AllServer)]
    public class PipeLineHandler : AMRpcHandler<C2M_BuyLineRequest, M2C_BuyLineResponse>
    {
        protected override async ETTask Run(Session session, C2M_BuyLineRequest request, M2C_BuyLineResponse response, Action reply)
        {
            PlayerComponent playerComponent = Game.Scene.GetComponent<PlayerComponent>();
            Player player = playerComponent.Get(session.GetComponent<SessionPlayerComponent>().Player.Id);
            //不够钱请求直接失败
            //if (player.Money < request.Price)
            //{
            //    response.Message = "failure";
            //    reply();
            //    await ETTask.CompletedTask;
            //}
            //因客户端确保请求内仅一座城市未拥有，但客户端没有其他玩家的城市信息，在此先不考虑玩家间城市冲突，留出增添位置供以后改进
            if (player.OwnCities.Contains(request.City1))
            {
                string gotcity = request.City2;
                player.Money -= request.Price;
                player.OwnCities.Add(gotcity);
                response.Message = "success";
                response.Boughtcity = gotcity;
                reply();
                //广播新购买的城市信息以供所有玩家更新地图
                Player[] players= playerComponent.GetAll();
                foreach (Player p in players)
                {
                    p.UniqueSession.Send(new M2C_CityOwnerUpdate() { Owner = "tester", CityName = gotcity });
                }
                await ETTask.CompletedTask;
            }
            else if(player.OwnCities.Contains(request.City2))
            {
                string gotcity = request.City1;
                player.Money -= request.Price;
                player.OwnCities.Add(gotcity);
                response.Message = "success";
                response.Boughtcity = gotcity;
                reply();
                //广播新购买的城市信息以供所有玩家更新地图
                Player[] players = playerComponent.GetAll();
                foreach (Player p in players)
                {
                    p.UniqueSession.Send(new M2C_CityOwnerUpdate() { Owner = "tester", CityName = gotcity });
                }
                await ETTask.CompletedTask;
            }
            else
            {
                string gotcity = request.City1;
                player.Money -= request.Price;
                player.OwnCities.Add(gotcity);
                response.Message = "success";
                response.Boughtcity = gotcity;
                reply();
                //广播新购买的城市信息以供所有玩家更新地图
                Player[] players = playerComponent.GetAll();
                foreach (Player p in players)
                {
                    p.UniqueSession.Send(new M2C_CityOwnerUpdate() { Owner = "tester", CityName = gotcity });
                }
                await ETTask.CompletedTask;
            }
        }

        [MessageHandler(AppType.AllServer)]
        public class PipeLineQuitHandler : AMHandler<C2M_QuitBuyLine>
        {
            protected override async ETTask Run(Session session, C2M_QuitBuyLine message)
            {
                PlayerComponent playerComponent = Game.Scene.GetComponent<PlayerComponent>();
                playerComponent.NowOrder++;
                //因为管线购买仅循环一轮，所以小于总数count才发送下一个购买的消息
                if (playerComponent.NowOrder < playerComponent.Count)
                {
                    Player nextplayer = playerComponent.playerOrders[playerComponent.NowOrder];
                    nextplayer.UniqueSession.Send(new M2C_LayingCircuitReminder() { Message = "your turn to lay circuit" });
                }
                //否则说明循环完毕，向所有玩家发送该阶段结束消息
                else
                {
                    Player[] players = playerComponent.GetAll();
                    foreach (Player p in players)
                    {
                        p.UniqueSession.Send(new M2C_FinishLayingCircuit());
                    }
                    playerComponent.NowOrder = 0;
                }
                await ETTask.CompletedTask;
            }

        }


    }
}
