using System;
using ETModel;
using UnityEngine;

namespace ETHotfix
{
    public static class StatusFactory
    {
        public static UI Create()
        {
            try
            {
                ResourcesComponent resourcesComponent = ETModel.Game.Scene.GetComponent<ResourcesComponent>();
                resourcesComponent.LoadBundleAsync(UIType.Status.StringToAB());
                GameObject bundleGameObject = (GameObject)resourcesComponent.GetAsset(UIType.Status.StringToAB(), UIType.Status);

                GameObject gameObject = UnityEngine.Object.Instantiate(bundleGameObject);//在已切换的地图上生成world预制件
                UI ui = ComponentFactory.Create<UI, string, GameObject>(UIType.Status, gameObject, false);
                ui.AddComponent<StatusComponent>();
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