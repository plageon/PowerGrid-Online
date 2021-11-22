using System.Collections.Generic;

namespace ETModel
{
	[ObjectSystem]
	public class PlayerSystem : AwakeSystem<Player, string>
	{
		public override void Awake(Player self, string a)
		{
			self.Awake(a);
		}
	}

	public sealed class Player : Entity
	{
		public string Account { get; private set; }
		
		public long UnitId { get; set; }

		public bool IsReady { get; set; }

		public bool IsPassed { get; set; }

		public bool HasBoughtOrQuit { get; set; }
		public int Money { get; set; }
		public Dictionary<int,string> OwnPlants { get; set; }
		public List<string> OwnCities { get; set; }

		public Session UniqueSession { get; set; }

		public int powerCity { get; set; }

		public void Awake(string account)
		{
			this.Account = account;
		}
		
		public override void Dispose()
		{
			if (this.IsDisposed)
			{
				return;
			}

			base.Dispose();
		}
	}
}