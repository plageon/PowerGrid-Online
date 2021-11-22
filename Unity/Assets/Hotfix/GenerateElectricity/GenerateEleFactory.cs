using System;
using ETModel;
using UnityEngine;

namespace ETHotfix
{
    public static class GenerateEleFactory
    {
        public static UI Create()
        {
            try
            {
                ResourcesComponent resourcesComponent = ETModel.Game.Scene.GetComponent<ResourcesComponent>();
                resourcesComponent.LoadBundle(UIType.GenerateEle.StringToAB());
                GameObject bundleGameObject = (GameObject) resourcesComponent.GetAsset(UIType.GenerateEle.StringToAB(), UIType.GenerateEle);
                GameObject gameObject = UnityEngine.Object.Instantiate(bundleGameObject);

                UI ui = ComponentFactory.Create<UI, string, GameObject>(UIType.GenerateEle, gameObject, false);
                ui.AddComponent<GenerateEleComponent>();
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