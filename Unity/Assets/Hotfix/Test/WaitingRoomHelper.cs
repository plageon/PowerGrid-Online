using ETModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETHotfix
{
    public static class WaitingRoomHelper
    {
        public static async ETVoid EnterWaitingRoomAsync()
        {
            try
            {
                // 加载Unit资源
                ResourcesComponent resourcesComponent = ETModel.Game.Scene.GetComponent<ResourcesComponent>();
                //await resourcesComponent.LoadBundleAsync($"unit.unity3d");

                // 加载场景资源
                await ETModel.Game.Scene.GetComponent<ResourcesComponent>().LoadBundleAsync("readyroom.unity3d");
                // 切换到map场景
                using (SceneChangeComponent sceneChangeComponent = ETModel.Game.Scene.AddComponent<SceneChangeComponent>())
                {
                    await sceneChangeComponent.ChangeSceneAsync(SceneType.ReadyRoom);
                }

                //G2C_EnterMap g2CEnterMap = await ETModel.SessionComponent.Instance.Session.Call(new C2G_EnterMap()) as G2C_EnterMap;
                //PlayerComponent.Instance.MyPlayer.UnitId = g2CEnterMap.UnitId;

                //Game.Scene.AddComponent<OperaComponent>();

                Game.EventSystem.Run(EventIdType.EnterRoomFinish);
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }
    }
}
