using System;
using ETModel;
using UnityEngine;

namespace ETHotfix
{
    public static class ChoosePlantFactory
    {
        public static UI Create()
        {
            try
            {
                ResourcesComponent resourcesComponent = ETModel.Game.Scene.GetComponent<ResourcesComponent>();
                resourcesComponent.LoadBundle(UIType.ChoosePlant.StringToAB());
                GameObject bundleGameObject = (GameObject)resourcesComponent.GetAsset(UIType.ChoosePlant.StringToAB(), UIType.ChoosePlant);
                GameObject gameObject = UnityEngine.Object.Instantiate(bundleGameObject);

                UI ui = ComponentFactory.Create<UI, string, GameObject>(UIType.ChoosePlant, gameObject, false);

                ui.AddComponent<ChoosePlantComponent>();
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