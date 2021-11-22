using ETModel;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace ETHotfix
{
    [MessageHandler(AppType.Realm)]
    public class TestLoginHandler : AMRpcHandler<C2R_TestRequest, R2C_TestResponse>
    {
        protected override async ETTask Run(Session session, C2R_TestRequest request, R2C_TestResponse response, Action reply)
        {
            StartConfig config = StartConfigComponent.Instance.StartConfig;
            //获取账号所在区服的AppId 索取登陆Key
            /*
            if (StartConfigComponent.Instance.GateConfigs.Count == 1)
            { //只有一个Gate服务器时当作AllServer配置处理
                config = StartConfigComponent.Instance.StartConfig;
            }
            else
            { //有多个Gate服务器时当作分布式配置处理
                GateAppId = RealmHelper.GetGateAppIdFromUserId(account.Id);
                config = StartConfigComponent.Instance.GateConfigs[GateAppId - 1];
            }
            */
            IPEndPoint innerAddress = config.GetComponent<InnerConfig>().IPEndPoint;
            Session gateSession = Game.Scene.GetComponent<NetInnerComponent>().Get(innerAddress);
            string outerAddress = config.GetComponent<OuterConfig>().Address2;

            G2R_GetLoginKey g2RGetLoginKey = await gateSession.Call(new R2G_GetLoginKey() { Account="test"}) as G2R_GetLoginKey;
            response.Message = g2RGetLoginKey.Key.ToString();
            reply();
        }
        /*
        
                DBProxyComponent dbProxy = Game.Scene.GetComponent<DBProxyComponent>();
                //验证提交来的的账号和密码
                List<ComponentWithId> result = await dbProxy.Query<AccountInfo>($"{{Account:'{request.Account}',Password:'{request.Password}'}}");

                if (result.Count != 1)
                {
                    response.Error = ErrorCode.ERR_AccountOrPasswordError;
                    reply();
                    return;
                }

                AccountInfo account = (AccountInfo)result[0]
         */
    }
}
