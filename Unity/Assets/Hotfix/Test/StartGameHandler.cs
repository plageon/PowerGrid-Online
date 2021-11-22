using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ETModel;
using UnityEngine;

namespace ETHotfix
{
    //接收来自服务器开始游戏的消息
    [MessageHandler]
    public class StartGameHandler: AMHandler<M2C_StartGameBroadCast>
    {
        protected override async ETTask Run(ETModel.Session session, M2C_StartGameBroadCast message)
        {
            //int playerNum = message.PlayerNum;
            //Debug.Log("playerNum"+playerNum.ToString());
            UI statusUI = StatusFactory.Create();
            Game.Scene.GetComponent<UIComponent>().Add(statusUI);
            GermanyHelper.EnterGermanyAsync().Coroutine();
            await ETTask.CompletedTask;
        }
    }
}
