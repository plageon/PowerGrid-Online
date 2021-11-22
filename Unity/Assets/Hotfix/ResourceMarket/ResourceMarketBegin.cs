using ETModel;

namespace ETHotfix
{
    [Event(EventIdType.ResourceMarketBegin)]
    public class ResourceMarketBegin:AEvent
    {
        public override void Run()
        {
            UI ui = ResourceMarketFactory.Create();
            Game.Scene.GetComponent<UIComponent>().Add(ui);
        }
    }
}