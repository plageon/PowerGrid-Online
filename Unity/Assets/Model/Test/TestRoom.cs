using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETModel
{
    public sealed class TestRoom:Entity
    {
        public ETCancellationTokenSource waitCts;
        public ETCancellationTokenSource randCts;
        public Dictionary<int, string> gamers = new Dictionary<int, string>();
    }
}
