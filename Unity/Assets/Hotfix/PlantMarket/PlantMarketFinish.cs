using ETModel;
using UnityEngine;

namespace ETHotfix
{
    [Event(EventIdType.PlantMarketFinish)]
    public class PlantMarketFinish:AEvent
    {
        public override void Run()
        {
            Game.Scene.GetComponent<UIComponent>().Remove(UIType.PlantMarket);
            ETModel.Game.Scene.GetComponent<ResourcesComponent>().UnloadBundle(UIType.PlantMarket.StringToAB());
            Game.EventSystem.Run(EventIdType.ResourceMarketBegin);
            //UI resourceMarketUI=Game.Scene.GetComponent<UIComponent>().Get(UIType.ResourceMarket); 
            //Debug.Log(resourceMarketUI.ToString());
            //ResourceMarketComponent resourceMarketComponent = resourceMarketUI.GetComponent<ResourceMarketComponent>();
            //Debug.Log(resourceMarketComponent.ToString());
            // ResourceMarketComponent resourceMarketComponent =
            //         Game.Scene.GetComponent<UIComponent>().Get(UIType.ResourceMarket).GetComponent<ResourceMarketComponent>();
            //Debug.Log(resourceMarketComponent.ToString());
            //resourceMarketComponent.PrintComponent();
            C2M_RefreshResourceMarket c2MRefreshResourceMarket = new C2M_RefreshResourceMarket();
            c2MRefreshResourceMarket.Message = "refresh resource market";
            SessionComponent.Instance.Session.Send(c2MRefreshResourceMarket);
        }
    }
}