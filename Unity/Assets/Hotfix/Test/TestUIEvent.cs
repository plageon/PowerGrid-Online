using ETModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETHotfix
{
    [Event(EventIdType.InitSceneTestStart)]
    public class TestUIEvent_CreateLoginUI : AEvent
    {
        public override void Run()
        {
            Log.Debug("Just testing");
            UI ui = UITestFactory.Create();
            Game.Scene.GetComponent<UIComponent>().Add(ui);
            Log.Debug($"{Game.Scene.GetComponent<UIComponent>().Get(ui.Name).Name} &{UIType.UITest}");
        }
    }

    [Event(EventIdType.LoginTestFinish)]
    public class TestUIEvent_EnterWaitingRoom : AEvent
    {
        public override void Run()
        {
            Log.Debug("Trying get ready");
            Game.Scene.GetComponent<UIComponent>().Remove(UIType.UITest);
            ETModel.Game.Scene.GetComponent<ResourcesComponent>().UnloadBundle(UIType.UITest.StringToAB());
            WaitingRoomHelper.EnterWaitingRoomAsync().Coroutine();
        }
    }

    [Event(EventIdType.EnterRoomFinish)]
    public class TestUIEvent_EnterRoomFinish : AEvent
    {
        public override void Run()
        {
            UI ui = UIReadyFactory.Create();
            Game.Scene.GetComponent<UIComponent>().Add(ui);
        }
    }

    [Event(EventIdType.AuctionFinish)]
    public class TestUIEvent_AuctionFinish : AEvent
    {
        public override void Run()
        {
            //UI ui = UIReadyFactory.Create();
            //Game.Scene.GetComponent<UIComponent>().Add(ui);
        }
    }
}
