using ETModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ETHotfix
{
    [MessageHandler]
    public class LayingCircuitReminderHandler : AMHandler<M2C_LayingCircuitReminder>
    {
        protected override async ETTask Run(ETModel.Session session, M2C_LayingCircuitReminder message)
        {
            //收到此消息代表轮到自己购买管线，应激活地图所有ui
            int index = 1;
            // UI world = Game.Scene.GetComponent<UIComponent>().uis["World"];
            // ReferenceCollector rc=world.GetParent<UI>().GameObject.GetComponent<ReferenceCollector>();
            // GameObject line = rc.Get<GameObject>("Line" + index);
            GermanyWorldComponent germanyWorldComponent = 
                    Game.Scene.GetComponent<UIComponent>().Get(UIType.PipelineMarket).GetComponent<GermanyWorldComponent>();
            germanyWorldComponent.Warning.text = "select a pipeline to build";
            germanyWorldComponent.enableChoose = true;
            //循环获取每一个line
            // while (line != null)
            // {
            //     line.SetActive(true);
            //     index++;
            //     line = rc.Get<GameObject>("Line" + index);
            // }
            await ETTask.CompletedTask;
        }
    }

    [MessageHandler]
    public class OwnerUpdateHandler : AMHandler<M2C_CityOwnerUpdate>
    {
        protected override async ETTask Run(ETModel.Session session, M2C_CityOwnerUpdate message)
        {
            //Log.Debug(message.Owner + " has bought " + message.CityName);
            Player player = PlayerComponent.Instance.MyPlayer;
            player.AllPlayerCities.Add(message.CityName);
            await ETTask.CompletedTask;
        }
    }

    [MessageHandler]
    public class FinishLayingCircuitHandler : AMHandler<M2C_FinishLayingCircuit>
    {
        protected override async ETTask Run(ETModel.Session session, M2C_FinishLayingCircuit message)
        {
            Log.Debug("this section has finished");
            Game.EventSystem.Run(EventIdType.PipelineMarketFinish);
            await ETTask.CompletedTask;
        }
    }
}
