using ETModel;
using UnityEngine;

namespace ETHotfix
{
    [Event(EventIdType.EnterGermanyFinish)]
    public class EnterGermanyFinish_RemoveLobbyUI:AEvent
    {
        public override void Run()
        {
            Game.Scene.GetComponent<UIComponent>().Remove(UIType.UIReady);
            ETModel.Game.Scene.GetComponent<ResourcesComponent>().UnloadBundle(UIType.UIReady.StringToAB());
            //UI backGroundUI = BackGroudFactory.Create();
            //Game.Scene.GetComponent<UIComponent>().Add(backGroundUI);
            
            
            Game.EventSystem.Run(EventIdType.AuctionPoolEvent);
        }
    }
}

