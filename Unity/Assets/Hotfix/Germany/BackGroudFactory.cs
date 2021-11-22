using System;
using ETModel;
using UnityEngine;

namespace ETHotfix
{
    public static class BackGroudFactory
    {
        public static UI Create()
        {
            try
            {
                ResourcesComponent resourcesComponent = ETModel.Game.Scene.GetComponent<ResourcesComponent>();
                resourcesComponent.LoadBundleAsync(UIType.BackGround.StringToAB());
                GameObject bundleGameObject = (GameObject)resourcesComponent.GetAsset(UIType.BackGround.StringToAB(), UIType.BackGround);

                GameObject gameObject = UnityEngine.Object.Instantiate(bundleGameObject);//在已切换的地图上生成world预制件
                UI ui = ComponentFactory.Create<UI, string, GameObject>(UIType.BackGround, gameObject, false);
                ui.AddComponent<BackGroundComponent>();
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