using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETModel
{
    [ObjectSystem]
    public class TimeTestComponentAwakeSystem : AwakeSystem<TimeTestComponent>
    {
        public override void Awake(TimeTestComponent self)
        {
            self.Awake();
        }
    }
    public class TimeTestComponent:Component
    {
        private Entity parent;
        private readonly Dictionary<string ,ITimeBehavior> Tbehaviors = new Dictionary<string, ITimeBehavior>();
        
        public void Awake()
        {
            this.parent = this.GetParent<Entity>();
            this.Load();
        }
        public void Run(string type,long time = 0)
        {
            try
            {
                Tbehaviors[type].Behavior(parent, time);
            }
            catch(Exception e)
            {
                throw new Exception($"{type} Time Behavior fault:{e}");
            }
        }

        public void Load()
        {
            this.Tbehaviors.Clear();
            List<Type> types = Game.EventSystem.GetTypes(typeof(TimeBehaviorAttribute));
            foreach (Type type in types)
            {
                object[] attrs = type.GetCustomAttributes(typeof(TimeBehaviorAttribute), false);
                if (attrs.Length == 0) continue;
                TimeBehaviorAttribute attribute = attrs[0] as TimeBehaviorAttribute;
                if (Tbehaviors.ContainsKey(attribute.Type))
                {
                    Log.Debug($"已经存在同类time behavior: {attribute.Type}");
                    throw new Exception($"已经存在同类time behavior: {attribute.Type}");
                }
                object o = Activator.CreateInstance(type);
                ITimeBehavior behavior = o as ITimeBehavior;
                if (behavior == null)
                {
                    Log.Error($"{o.GetType().FullName}没有继承ITimeBehavior");
                    continue;
                }
                this.Tbehaviors.Add(attribute.Type, behavior);
            }
        }
    }
}
