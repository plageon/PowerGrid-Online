using ETModel;

namespace ETHotfix
{
    [Event(EventIdType.GenerateEleFinish)]
    public class GenerateEleFinish:AEvent
    {
        public override void Run()
        {
            Game.Scene.GetComponent<UIComponent>().Remove(UIType.GenerateEle);
            ETModel.Game.Scene.GetComponent<ResourcesComponent>().UnloadBundle(UIType.GenerateEle.StringToAB());
            UI ui = PlantMarketFactory.Create();
            Game.Scene.GetComponent<UIComponent>().Add(ui);
        }
    }
}