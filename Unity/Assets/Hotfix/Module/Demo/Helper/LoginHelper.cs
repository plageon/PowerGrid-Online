using System;
using ETModel;
using UnityEngine;

namespace ETHotfix
{
    public static class LoginHelper
    {
        public static async ETVoid OnLoginAsync(string account)
        {
            try
            {
                // // 创建一个ETModel层的Session
                // ETModel.Session session = ETModel.Game.Scene.GetComponent<NetOuterComponent>().Create(GlobalConfigComponent.Instance.GlobalProto.Address);
				            //
                // // 创建一个ETHotfix层的Session, ETHotfix的Session会通过ETModel层的Session发送消息
                // Session realmSession = ComponentFactory.Create<Session, ETModel.Session>(session);
                // R2C_Login r2CLogin = (R2C_Login) await realmSession.Call(new C2R_Login() { Account = account, Password = "111111" });
                //
                // //可在此处先预添加判定逻辑，如返回session中不包含特定信息或者Error被设置为特定值时通过UIComponent获取发请求的组件并重置
                // //若符合则进入网关绑定，将该客户端与服务端用唯一session绑定
                //
                // realmSession.Dispose();
                //
                // // 创建一个ETModel层的Session,并且保存到ETModel.SessionComponent中
                // ETModel.Session gateSession = ETModel.Game.Scene.GetComponent<NetOuterComponent>().Create(r2CLogin.Address);
                // ETModel.Game.Scene.AddComponent<ETModel.SessionComponent>().Session = gateSession;
				            //
                // // 创建一个ETHotfix层的Session, 并且保存到ETHotfix.SessionComponent中
                // Game.Scene.AddComponent<SessionComponent>().Session = ComponentFactory.Create<Session, ETModel.Session>(gateSession);
				            //
                // G2C_LoginGate g2CLoginGate = (G2C_LoginGate)await SessionComponent.Instance.Session.Call(new C2G_LoginGate() { Key = r2CLogin.Key });
                // Debug.Log("success");
                // Debug.Log("id: "+ETModel.Game.Scene.GetComponent<PlayerComponent>().MyPlayer.Id.ToString());
                // Debug.Log("id: "+PlayerComponent.Instance.MyPlayer.Id.ToString());
                //
                // PlayerComponent.Instance.MyPlayer.Money = 50;
                // // 创建Player
                // Player player = ETModel.ComponentFactory.CreateWithId<Player>(g2CLoginGate.PlayerId);
                // PlayerComponent playerComponent = ETModel.Game.Scene.GetComponent<PlayerComponent>();
                // playerComponent.MyPlayer = player;
                // Debug.Log("id: "+ETModel.Game.Scene.GetComponent<PlayerComponent>().MyPlayer.Id.ToString());
                // PlayerComponent.Instance.MyPlayer = player;
                // player.Money = 50;//初始资金
                //
                // Debug.Log("id: "+PlayerComponent.Instance.MyPlayer.Id.ToString());

                Game.EventSystem.Run(EventIdType.LoginFinish);

                
                // 测试消息有成员是class类型
                //G2C_PlayerInfo g2CPlayerInfo = (G2C_PlayerInfo) await SessionComponent.Instance.Session.Call(new C2G_PlayerInfo());

                //该消息属于Actor类型，指现在客户端对服务端指定方法进行调用，即选择函数响应
                //M2C_TestActorResponse testActorResponse = (M2C_TestActorResponse)await SessionComponent.Instance.Session.Call(new C2M_TestActorRequest());

            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        } 
    }
}