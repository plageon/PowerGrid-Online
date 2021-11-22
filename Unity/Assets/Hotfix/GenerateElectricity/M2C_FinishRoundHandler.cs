using ETModel;

namespace ETHotfix
{
    [MessageHandler]
    public class M2C_FinishRoundHandler:AMHandler<M2C_FinishRound>
    {
        protected override async ETTask Run(ETModel.Session session, M2C_FinishRound message)
        {
            Game.EventSystem.Run(EventIdType.GenerateEleFinish);
        }
    }
}