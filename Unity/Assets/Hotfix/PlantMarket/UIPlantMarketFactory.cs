using System;
using ETModel;
using UnityEngine;

namespace ETHotfix
{
    public static class PlantMarketFactory
    {
        public static UI Create()
        {
            try
            {
                ResourcesComponent resourcesComponent = ETModel.Game.Scene.GetComponent<ResourcesComponent>();
                resourcesComponent.LoadBundle(UIType.PlantMarket.StringToAB());
                GameObject bundleGameObject = (GameObject)resourcesComponent.GetAsset(UIType.PlantMarket.StringToAB(), UIType.PlantMarket);
                GameObject gameObject = UnityEngine.Object.Instantiate(bundleGameObject);

                UI ui = ComponentFactory.Create<UI, string, GameObject>(UIType.PlantMarket, gameObject, false);

                ui.AddComponent<PlantMarketComponent>();
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