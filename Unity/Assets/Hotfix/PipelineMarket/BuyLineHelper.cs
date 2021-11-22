using ETModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ETHotfix
{
    public static class BuyLineHelper
    {
        public static async ETVoid OnBuyLineTryAsync(string city1,string city2,int price)
        {
            try
            {
                //Debug.Log("send buy line requenst");
                M2C_BuyLineResponse response=await SessionComponent.Instance.Session.Call(new C2M_BuyLineRequest() { City1=city1,City2=city2,Price=price }) as M2C_BuyLineResponse;
                if(response.Message=="success")
                {
                    PlayerComponent.Instance.MyPlayer.OwnCities.Add(response.Boughtcity);
                    PlayerComponent.Instance.MyPlayer.Money -= (price+10);
                    PlayerComponent.Instance.MyPlayer.OwnCities.Add(city2);
                }

                //await OnBuyLineQuitAsync();
                await ETTask.CompletedTask;
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }

        public static async ETVoid BuyFirstCity(string city1,string city2,int price)
        {
            try
            {
                //Debug.Log("send buy line requenst");
                M2C_BuyLineResponse response=await SessionComponent.Instance.Session.Call(new C2M_BuyLineRequest() { City1=city1,City2=city2,Price=price }) as M2C_BuyLineResponse;
                if(response.Message=="success")
                {
                    PlayerComponent.Instance.MyPlayer.OwnCities.Add(response.Boughtcity);
                    PlayerComponent.Instance.MyPlayer.Money -= 10;
                    
                }

                //await OnBuyLineQuitAsync();
                await ETTask.CompletedTask;
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }

        public static async ETVoid OnBuyLineQuitAsync()
        {
            try
            {
                //Game.EventSystem.Run(EventIdType.PipelineMarketFinish);
                C2M_QuitBuyLine c2MQuitBuyLine = new C2M_QuitBuyLine();
                c2MQuitBuyLine.Message = "quit buy line";
                SessionComponent.Instance.Session.Send(c2MQuitBuyLine);
                // Log.Debug("you decide to quit buying");
                // int index = 1;
                // UI world = Game.Scene.GetComponent<UIComponent>().uis["World"];
                // ReferenceCollector rc = world.GetParent<UI>().GameObject.GetComponent<ReferenceCollector>();
                // GameObject line = rc.Get<GameObject>("Line" + index);
                // //循环获取每一个line
                // while (line != null)
                // {
                //     line.SetActive(false);
                //     index++;
                // }
                await ETTask.CompletedTask;
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }
    }
}
