using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETModel
{
    [TimeBehavior(Typebehavior.RandTarget)]
    public class RandTargetTimeBehavior : ITimeBehavior
    {
        public TestRoom room;
        public Random rd = new Random();
        public int count = 0;
        public int maxCount;

        public void Behavior(Entity parent, long time)
        {
            room = parent as TestRoom;
            StartRand();
        }

        private void StartRand()
        {
            room.gamers.Add(1, "gamer1");
            room.gamers.Add(2, "gamer2");
            room.gamers.Add(3, "gamer3");
            room.gamers.Add(4, "gamer4");
            room.gamers.Add(5, "gamer5");

            maxCount = rd.Next(2, 4);
            Log.Info($"将点名次数:{maxCount}");

            RandTimeAndTarget().Coroutine();
        }

        private async ETVoid RandTimeAndTarget()
        {
            int num = rd.Next(1, 5);
            string target = room.gamers[num];
            int randtime = rd.Next(3, 12);

            TimerComponent timer = Game.Scene.GetComponent<TimerComponent>();
            room.randCts = new ETCancellationTokenSource();
            await timer.WaitAsync((randtime + 1) * 1000, room.randCts.Token);

            Log.Info($"{room.GetType()}-执行间隔{randtime}秒点名{target}");
            room.randCts.Dispose();
            room.randCts = null;

            count++;
            if (count < maxCount)
            {
                RandTimeAndTarget().Coroutine();
            }
        }
    }
}
