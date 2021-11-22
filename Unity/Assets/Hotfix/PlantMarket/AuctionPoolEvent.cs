using ETModel;
using UnityEngine;

namespace ETHotfix
{
    [Event(EventIdType.AuctionPoolEvent)]
    public class AuctionPoolEvent:AEvent
    {
        public override void Run()
        {
            //Debug.Log("auction pool show");
            UI ui = PlantMarketFactory.Create();
            Game.Scene.GetComponent<UIComponent>().Add(ui);
            ui.SetAsFirstSibling();
        }
    }
}