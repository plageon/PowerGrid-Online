﻿using ETModel;
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
    public class TestReadyComponentAwakeSystem : AwakeSystem<TestReadyComponent>
    {
        public override void Awake(TestReadyComponent self)
        {
            self.Awake();
        }
    }
    public class TestReadyComponent : Component
    {
        //public Text prompt;
        //public bool isLogging;
        //public bool isRegistering;
        public bool isTesting;

        public void Awake()
        {
            ReferenceCollector rc = this.GetParent<UI>().GameObject.GetComponent<ReferenceCollector>();

            //prompt = rc.Get<GameObject>("Prompt").GetComponent<Text>();
            //this.isLogging = false;
            //this.isRegistering = false;
            this.isTesting = false;

            //添加事件
            rc.Get<GameObject>("MyButton").GetComponent<Button>().onClick.Add(() => MyBtnOnClick());
            //rc.Get<GameObject>("RegisterButton").GetComponent<Button>().onClick.Add(() => RegisterBtnOnClick());
        }
        public void MyBtnOnClick()
        {
            if (this.isTesting || this.IsDisposed)
            {
                return;
            }
            this.isTesting = true;
            Log.Debug("You tried to get ready");
            ReadyHelper.OnReadyAsync().Coroutine();
            this.isTesting = false;
        }
    }
}
