using ETModel;
using NUnit.Framework;
using UnityEngine;

namespace ETHotfix
{
    [MessageHandler]
    public class M2C_ResourceReminderHandler:AMHandler<M2C_ResourceReminder>
    {
        protected override async ETTask Run(ETModel.Session session, M2C_ResourceReminder message)
        {
            //Debug.Log("handling resource reminder");
            UI resourceMarketUI=Game.Scene.GetComponent<UIComponent>().Get(UIType.ResourceMarket); 
            //Debug.Log(resourceMarketUI.ToString());
            ResourceMarketComponent resourceMarketComponent = resourceMarketUI.GetComponent<ResourceMarketComponent>();
            //Debug.Log(resourceMarketComponent.ToString());
            //resourceMarketComponent.PrintComponent();
            resourceMarketComponent.coalMarket = message.Coal;
            resourceMarketComponent.oilMarket = message.Oil;
            resourceMarketComponent.garbageMarket = message.Garbage;
            resourceMarketComponent.nuclearMarket = message.Nuclear;
            resourceMarketComponent.ResetSlider();
            await ETTask.CompletedTask;
        }
    }
}