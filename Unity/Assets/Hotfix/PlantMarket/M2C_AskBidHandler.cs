using ETModel;
using UnityEngine;

namespace ETHotfix
{
    [MessageHandler]
    public class M2C_AskBidHandler:AMHandler<M2C_AskBid>
    {
        protected override async ETTask Run(ETModel.Session session, M2C_AskBid message)
        {
            Player player= PlayerComponent.Instance.MyPlayer;
            Debug.Log("actorid"+message.ActorId.ToString()+"playerid"+player.Id);
            if (message.ActorId == player.Id)
            {
                
            }
            else
            {
                
            }

            await ETTask.CompletedTask;
        }
    }
}