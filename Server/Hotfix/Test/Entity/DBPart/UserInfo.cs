using System;
using System.Collections.Generic;
using System.Text;

namespace ETModel
{
    [ObjectSystem]
    public class UserInfoAwakeSystem : AwakeSystem<UserInfo, string>
    {
        public override void Awake(UserInfo self, string name)
        {
            self.Awake(name);
        }
    }
    public class UserInfo:Entity
    {
        
        public string UserName { get; set; }

        public int Level { get; set; }

        public long Money { get; set; }

        //上次游戏角色序列 1/2/3
        public int LastPlay { get; set; }

        public void Awake(string name)
        {
            UserName = name;
            Level = 1;
            Money = 10000;
            LastPlay = 0;
        }
    }  
}
