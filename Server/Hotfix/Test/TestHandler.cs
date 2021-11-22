using ETModel;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace ETHotfix
{
    [MessageHandler(AppType.Realm)]
    public class TestHandler : AMRpcHandler<C2R_TestRequest, R2C_TestResponse>
    {
        protected override async ETTask Run(Session session, C2R_TestRequest request, R2C_TestResponse response, Action reply)
        {
            /*
             * 可在此使用DB部分，或纯粹进行逻辑处理
             */

            //生成玩家帐号 这里随机生成区号
            AccountInfo newAccount = ComponentFactory.CreateWithId<AccountInfo>(RealmHelper.GenerateId());
            newAccount.Name = request.Message;
            newAccount.Password = request.Message;

            //生成玩家的用户信息 用户名在消息中提供
            ComponentFactory.CreateWithId<UserInfo, string>(newAccount.Id, request.Message);
            response.Message = $"We have received the message! {newAccount.Name} is sent";

            StartConfig config = Game.Scene.GetComponent<RealmGateAddressComponent>().GetAddress();
            IPEndPoint innerAddress = config.GetComponent<InnerConfig>().IPEndPoint;
			Session gateSession = Game.Scene.GetComponent<NetInnerComponent>().Get(innerAddress);
            string outerAddress = config.GetComponent<OuterConfig>().Address2;
            response.Address = outerAddress;
            G2R_GetLoginKey g2RGetLoginKey = (G2R_GetLoginKey)await gateSession.Call(new R2G_GetLoginKey() { Account = request.Message });
            response.Key = g2RGetLoginKey.Key;
            /*
			//Log.Debug($"gate address: {MongoHelper.ToJson(config)}");
             */
            //MyPlayerComponent myPlayerComponent = Game.Scene.GetComponent<MyPlayerComponent>();
            //UserComponent temp = Game.Scene.GetComponent<UserComponent>();
            //Game.Scene.AddComponent<UserComponent>();
            User user = ComponentFactory.Create<User, string>(request.Message);
            user.AddComponent<MailBoxComponent>();
            //temp.Add(user);
			session.AddComponent<SessionUserComponent>().User = user;
            // 挂上这个组件表示该Entity是一个Actor,接收的消息将会队列处理
			session.AddComponent<MailBoxComponent, string>(MailboxType.GateSession);
            session.Send(new G2C_TestHotfixMessage() { Info = "recv hotfix message success" });

            /*
             * StartConfigComponent config = Game.Scene.GetComponent<StartConfigComponent>();
             * //构建realmSession通知Realm服务器 玩家已上线
                //..
              //设置User的参数
                user.GateAppID = config.StartConfig.AppId;
                user.GateSessionID = session.InstanceId;
                user.ActorID = 0;
            添加 MailBoxComponent时，有带MailboxType.GateSession参数和不带两种方式的，这是有重要用意和区别的，以后我们会讲到。
                //回复客户端
                response.UserID = user.UserID;
                reply();
             */
            reply();
            await ETTask.CompletedTask;
        }
    }
}
