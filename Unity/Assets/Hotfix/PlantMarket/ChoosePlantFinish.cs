using ETModel;

namespace ETHotfix
{
    [Event(EventIdType.ChoosePlantFinish)]
    public class ChoosePlantFinish:AEvent
    {
        public override void Run()
        {
            Game.Scene.GetComponent<UIComponent>().Remove(UIType.ChoosePlant);
            ETModel.Game.Scene.GetComponent<ResourcesComponent>().UnloadBundle(UIType.ChoosePlant.StringToAB());
        }
    }
}