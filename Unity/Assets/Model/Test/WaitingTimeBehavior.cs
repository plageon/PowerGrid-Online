using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETModel
{
    [TimeBehaviorAttribute(Typebehavior.Waiting)]
    public class WaitingTimeBehavior:ITimeBehavior
    {
        public TestRoom room;
        public long waitTime;

        public void Behavior(Entity parent,long time)
        {
            room = parent as TestRoom;
            waitTime = time;
            Waiting().Coroutine();
        }

        public async ETVoid Waiting()
        {
            TimerComponent timer = Game.Scene.GetComponent<TimerComponent>();
            room.waitCts = new ETCancellationTokenSource();

            await timer.WaitAsync(waitTime, room.waitCts.Token);
            Log.Info($"{room.GetType().ToString()}-finished waiting");
            room.waitCts.Dispose();
            room.waitCts = null; 
        }
    }
}
