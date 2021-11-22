using System;
using ETModel;
using UnityEngine;

namespace ETHotfix
{
    public static class ResourceMarketFactory
    {
        public static UI Create()
        {
            try
            {
                ResourcesComponent resourcesComponent = ETModel.Game.Scene.GetComponent<ResourcesComponent>();
                resourcesComponent.LoadBundle(UIType.ResourceMarket.StringToAB());
                GameObject bundleGameObject = (GameObject)resourcesComponent.GetAsset(UIType.ResourceMarket.StringToAB(), UIType.ResourceMarket);
                GameObject gameObject = UnityEngine.Object.Instantiate(bundleGameObject);

                UI ui = ComponentFactory.Create<UI, string, GameObject>(UIType.ResourceMarket, gameObject, false);

                ui.AddComponent<ResourceMarketComponent>();
                return ui;
            }
            catch (Exception e)
            {
                Log.Error(e);
                return null;
            }
        }
    }
}