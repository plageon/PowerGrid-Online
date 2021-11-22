using ETModel;
using UnityEngine;
using UnityEngine.UI;

namespace ETHotfix
{
    [ObjectSystem]
    public class StatusComponentAwakeSystem: AwakeSystem<StatusComponent>
    {
        public override void Awake(StatusComponent self)
        {
            self.Awake();
        }
    }
    public class StatusComponent:Component
    {
        private Text Money;
        private Text Coal;
        private Text Oil;
        private Text Garbage;
        private Text Nuclear;
        public void Awake()
        {
            ReferenceCollector rc = this.GetParent<UI>().GameObject.GetComponent<ReferenceCollector>();
            this.Money=rc.Get<GameObject>("Money").GetComponent<Text>();
            this.Coal=rc.Get<GameObject>("Coal").GetComponent<Text>();
            this.Oil=rc.Get<GameObject>("Oil").GetComponent<Text>();
            this.Garbage=rc.Get<GameObject>("Garbage").GetComponent<Text>();
            this.Nuclear=rc.Get<GameObject>("Nuclear").GetComponent<Text>();
        }

        public void UpdateStatus()
        {
            //Debug.Log("Update status");
            Player player = PlayerComponent.Instance.MyPlayer;
            Debug.Log("Money" + player.Money + "Coal" + player.Resources["Coal"] + "Oil" + player.Resources["Oil"] + "Garbage" +
                player.Resources["Garbage"] + "Nuclear" + player.Resources["Nuclear"]);
            this.Money.text = "Money:$"+player.Money;
            this.Coal.text = "Coal:" + player.Resources["Coal"];
            this.Oil.text = "Oil:" + player.Resources["Oil"];
            this.Garbage.text = "Garbage:" + player.Resources["Garbage"];
            this.Nuclear.text = "Nuclear:" + player.Resources["Nuclear"];
        }
    }
}