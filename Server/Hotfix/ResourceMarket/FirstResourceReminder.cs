using ETModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETHotfix
{
    [Event(EventIdType.SendFirstResourceReminder)]
    public class FirstResourceReminder : AEvent
    {

        public override void Run()
        {
            
            PlayerComponent playerComponent = Game.Scene.GetComponent<PlayerComponent>();
            playerComponent.NowOrder = 0;
            M2C_ResourceReminder m2C_ResourceReminder = new M2C_ResourceReminder();
            m2C_ResourceReminder.Coal = playerComponent.resourceMarket.coal;
            m2C_ResourceReminder.Oil = playerComponent.resourceMarket.oil;
            m2C_ResourceReminder.Garbage = playerComponent.resourceMarket.garbage;
            m2C_ResourceReminder.Nuclear = playerComponent.resourceMarket.nuclear;
            m2C_ResourceReminder.Message = "buy resource";
            playerComponent.playerOrders[playerComponent.NowOrder].UniqueSession.Send(m2C_ResourceReminder);
        }
    }
}
