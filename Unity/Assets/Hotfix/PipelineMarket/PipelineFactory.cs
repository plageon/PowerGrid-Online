using System;
using ETModel;
using UnityEngine;

namespace ETHotfix
{
    public static class PipelineFactory
    {
        public static UI Create()
        {
            try
            {
                ResourcesComponent resourcesComponent = ETModel.Game.Scene.GetComponent<ResourcesComponent>();
                resourcesComponent.LoadBundleAsync(UIType.PipelineMarket.StringToAB());
                GameObject bundleGameObject = (GameObject)resourcesComponent.GetAsset(UIType.PipelineMarket.StringToAB(), UIType.PipelineMarket);

                GameObject gameObject = UnityEngine.Object.Instantiate(bundleGameObject);//在已切换的地图上生成world预制件
                UI ui = ComponentFactory.Create<UI, string, GameObject>(UIType.PipelineMarket, gameObject, false);
                ui.AddComponent<GermanyWorldComponent>();
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