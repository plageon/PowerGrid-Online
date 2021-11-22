using ETModel;

namespace ETHotfix
{
    [MessageHandler]
    public class M2C_ResourceMarketFinishHandler:AMHandler<M2C_ResourceMarketFinish>
    {
        protected override async ETTask Run(ETModel.Session session, M2C_ResourceMarketFinish message)
        {
            Game.Scene.GetComponent<UIComponent>().Remove(UIType.ResourceMarket);
            ETModel.Game.Scene.GetComponent<ResourcesComponent>().UnloadBundle(UIType.ResourceMarket.StringToAB());
            UI ui = PipelineFactory.Create();
            Game.Scene.GetComponent<UIComponent>().Add(ui);
            // Game.Scene.GetComponent<UIComponent>().Remove(UIType.ResourceMarket);
            // ETModel.Game.Scene.GetComponent<ResourcesComponent>().UnloadBundle(UIType.ResourceMarket.StringToAB());
            // UI ui = GenerateEleFactory.Create();
            // Game.Scene.GetComponent<UIComponent>().Add(ui);
            await ETTask.CompletedTask;
        }
    }
}