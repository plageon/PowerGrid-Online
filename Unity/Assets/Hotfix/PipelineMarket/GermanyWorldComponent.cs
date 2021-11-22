using ETModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace ETHotfix
{

    [ObjectSystem]
    public class GermanyWorldComponentAwakeSystem : AwakeSystem<GermanyWorldComponent>
    {
        public override void Awake(GermanyWorldComponent self)
        {
            self.Awake();
        }
    }
    public class GermanyWorldComponent:Component
    {
	    public Text Warning;
	    public bool enableChoose;
        public void Awake()
        {
	        Game.Scene.GetComponent<UIComponent>().Get(UIType.Status).GetComponent<StatusComponent>().UpdateStatus();
			ReferenceCollector rc = this.GetParent<UI>().GameObject.GetComponent<ReferenceCollector>();
			this.Warning=rc.Get<GameObject>("Warning").GetComponent<Text>();
			this.enableChoose = false;
			//加载略过组件
            rc.Get<GameObject>("PassButton").GetComponent<Button>().onClick.Add(() => Pass());
			//rc.Get<GameObject>("PassButton").SetActive(false);
            int index=1;
 
            GameObject line;GameObject text; String[] info;
			line = rc.Get<GameObject>("Line" + index);
			text = rc.Get<GameObject>("Info" + index);
            //循环获取每一个line
            while(line!=null){
				info=text.GetComponent<Text>().text.Split(',');
				string info0 = info[0];string info1 = info[1];int info2 = Int32.Parse(info[2]);
				line.GetComponent<Button>().onClick.Add(() => BuyLine(info0, info1, info2));
				//line.SetActive(false);//new added//地图加载出来时在进入铺电网阶段前都不需要其进入激活状态，事件发出后由客户端一次性全部启用完毕后在全部禁用
				index++;
				line = rc.Get<GameObject>("Line" + index);
				text = rc.Get<GameObject>("Info" + index);
				//line.SetActive(true);
				
			}
            C2M_RefreshPipelineMarket c2MRefreshPipelineMarket = new C2M_RefreshPipelineMarket();
            c2MRefreshPipelineMarket.Message = "refresh pipeline market";
            SessionComponent.Instance.Session.Send(c2MRefreshPipelineMarket);
		}

		public void BuyLine(string city1,string city2,int price)
		{
			if (this.enableChoose)
			{
				Debug.Log("youclicked");
				Debug.Log(city1+city2+price.ToString());
				Player myplayer = PlayerComponent.Instance.MyPlayer;
				//Log.Debug(myplayer.ToString());
				//先检查点击的管道是否至少一端连接玩家已有城市
				if (myplayer.OwnCities.Count == 0)
				{
					if (myplayer.Money >= 10)
					{
						BuyLineHelper.BuyFirstCity(city1, city2, price).Coroutine();
					}
					else
					{
						this.Warning.text = "You don't have enough money";
					}
				}
				else if (myplayer.AllPlayerCities.Contains(city1) && myplayer.AllPlayerCities.Contains(city2))
				{
					//客户端提醒玩家无法购买已拥有或不必要的管道
					Log.Debug("you can't buy what you have or is connected already");
				}
				else if (!(myplayer.AllPlayerCities.Contains(city1) || myplayer.AllPlayerCities.Contains(city2)))
				{
					//客户端提醒玩家所购买管道不与已有城市相邻
					Log.Debug("you can't buy what is not nearby");
				}
				else
				{
					if (myplayer.Money >= price + 10)
					{
						//客户端将该管道消息发往服务端以发起购买请求，由服务端判断是否成功//写一个helper以转移异步方法
						BuyLineHelper.OnBuyLineTryAsync(city1, city2, price).Coroutine();
					}
					else
					{
						this.Warning.text = "You don't have enough money";
					}
				}
			}
			else
			{
				Debug.Log("not your turn");
				this.Warning.text = "Not your turn";
			}
		}
		public void Pass()
		{
			if (this.enableChoose)
			{
				BuyLineHelper.OnBuyLineQuitAsync().Coroutine();
			}
			else
			{
				this.Warning.text = "not your turn";
			}
		}
	}
}
