using ETModel;

namespace ETHotfix
{
    [MessageHandler]
    public class M2C_FinishAuctionHandler:AMHandler<M2C_FinishAuction>
    {
        protected override async ETTask Run(ETModel.Session session, M2C_FinishAuction message)
        {
            Game.EventSystem.Run(EventIdType.PlantMarketFinish);
            await ETTask.CompletedTask;
        }
    }
}