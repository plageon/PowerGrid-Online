using ETModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETHotfix
{
    public static class ReadyHelper
    {
        public static async ETVoid OnTestAsync()
        {
            try
            {
                // 创建一个ETModel层的Session
                ETModel.Session session = ETModel.Game.Scene.GetComponent<NetOuterComponent>().Create(GlobalConfigComponent.Instance.GlobalProto.Address);

                // 创建一个ETHotfix层的Session, ETHotfix的Session会通过ETModel层的Session发送消息
                Session realmSession = ComponentFactory.Create<Session, ETModel.Session>(session);
                R2C_TestResponse r2CTestResponse = await realmSession.Call(new C2R_TestRequest() { Message = "just testing " }) as R2C_TestResponse;
                //R2C_Login r2CLogin = (R2C_Login)await realmSession.Call(new C2R_Login() { Account = account, Password = "111111" });

                //可在此处先预添加判定逻辑，如返回session中不包含特定信息或者Error被设置为特定值时通过UIComponent获取发请求的组件并重置
                //若符合则进入网关绑定，将该客户端与服务端用唯一session绑定

                realmSession.Dispose();

                // 创建一个ETModel层的Session,并且保存到ETModel.SessionComponent中
                ETModel.Session gateSession = ETModel.Game.Scene.GetComponent<NetOuterComponent>().Create(r2CTestResponse.Address);
                ETModel.Game.Scene.AddComponent<ETModel.SessionComponent>().Session = gateSession;

                // 创建一个ETHotfix层的Session, 并且保存到ETHotfix.SessionComponent中
                Game.Scene.AddComponent<SessionComponent>().Session = ComponentFactory.Create<Session, ETModel.Session>(gateSession);

                G2C_LoginGate g2CLoginGate = (G2C_LoginGate)await SessionComponent.Instance.Session.Call(new C2G_LoginGate() { Key = r2CTestResponse.Key });

                /*
                 if (messageGate.Error == ErrorCode.ERR_ConnectGateKeyError)
            {
                login.prompt.text = "连接网关服务器超时";
                login.account.text = "";
                login.password.text = "";
                sessionGate.Dispose();
                login.isLogining = false;
                return;
            }
            //判断通过则登陆Gate成功
                */
                Log.Info("登陆gate成功!");
                //Log.Info($"test: {r2CTestResponse.Message}");
                /*
            login.prompt.text = "";
            User user = ComponentFactory.Create<User, long>(messageGate.UserID);
            GamerComponent.Instance.MyUser = user;
            //Log.Debug("登陆成功");

            //加载透明界面 退出当前界面
            Game.EventSystem.Run(UIEventType.LandLoginFinish);
                 */

                
                // 创建Player
                Player player = ETModel.ComponentFactory.CreateWithId<Player>(g2CLoginGate.PlayerId);
                PlayerComponent playerComponent = ETModel.Game.Scene.GetComponent<PlayerComponent>();
                playerComponent.MyPlayer = player;
                player.Money = 50;//初始资金
                player.OwnPlants = new Dictionary<int, string>();
                player.OwnCities = new List<string>();
                player.Resources = new Dictionary<string, int>();
                player.AllPlayerCities = new List<string>();
                player.Resources.Add("Coal",0);
                player.Resources.Add("Oil",0);
                player.Resources.Add("Garbage",0);
                player.Resources.Add("Nuclear",0);

                Game.EventSystem.Run(EventIdType.LoginTestFinish);


                // 测试消息有成员是class类型
                //G2C_PlayerInfo g2CPlayerInfo = (G2C_PlayerInfo)await SessionComponent.Instance.Session.Call(new C2G_PlayerInfo());

                //该消息属于Actor类型，指现在客户端对服务端指定方法进行调用，即选择函数响应
                //M2C_TestActorResponse testActorResponse = (M2C_TestActorResponse)await SessionComponent.Instance.Session.Call(new C2M_TestActorRequest());

            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }

        public static async ETVoid OnReadyAsync()
        {
            try
            {
                //发送消息表示客户端已准备
                SessionComponent.Instance.Session.Send(new C2R_TestHelloMsg() { SayMessage = "if received it means im ready" });

                await ETTask.CompletedTask;

            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }

        // public static async ETVoid OnAuctionAsync(int id,bool isPassed,int expectedprice)
        // {
        //     Player player = ETModel.Game.Scene.GetComponent<PlayerComponent>().MyPlayer;
        //     M2C_AuctionResponse response;
        //     if (isPassed)
        //     {
        //         player.IsPassed = true;
        //         response = await SessionComponent.Instance.Session.Call(new C2M_AuctionRequest() { IsPassed = true }) as M2C_AuctionResponse;                
        //     }
        //     else
        //     {
        //         player.IsPassed = false;
        //         response = await SessionComponent.Instance.Session.Call(new C2M_AuctionRequest() { PlantId = id, IsPassed = false, Bid = expectedprice }) as M2C_AuctionResponse;
        //     }
        //     if (response.Message == "Success")
        //     {
        //         player.HasBought = true;
        //         PlantConfig plant = Game.Scene.GetComponent<ConfigComponent>().Get(typeof(PlantConfig), id) as PlantConfig;
        //         player.OwnPlants.Add(id, plant.Pic);//如果超过三个要求玩家取舍
        //     }
        // }
    }
}


