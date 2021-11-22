using ETModel;

namespace ETHotfix
{
    [Event(EventIdType.PipelineMarketFinish)]
    public class PilelineMarketFinish:AEvent
    {
        public override void Run()
        {
            Game.Scene.GetComponent<UIComponent>().Remove(UIType.PipelineMarket);//这么写估计地图会消失
            ETModel.Game.Scene.GetComponent<ResourcesComponent>().UnloadBundle(UIType.PipelineMarket.StringToAB());
            UI ui = GenerateEleFactory.Create();
            Game.Scene.GetComponent<UIComponent>().Add(ui);
        }
    }
}