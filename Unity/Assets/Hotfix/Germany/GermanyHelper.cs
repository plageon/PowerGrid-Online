using ETModel;
using UnityEngine;

namespace ETHotfix
{
    public static class GermanyHelper
    {
        
        public static async ETVoid EnterGermanyAsync()
        {
            ResourcesComponent resourcesComponent = ETModel.Game.Scene.GetComponent<ResourcesComponent>();
            await ETModel.Game.Scene.GetComponent<ResourcesComponent>().LoadBundleAsync("germany.unity3d");
            using (SceneChangeComponent sceneChangeComponent = ETModel.Game.Scene.AddComponent<SceneChangeComponent>())
            {
                await sceneChangeComponent.ChangeSceneAsync(SceneType.Germany);
            }
            
            
            Game.EventSystem.Run(EventIdType.EnterGermanyFinish);
        }
    }
}