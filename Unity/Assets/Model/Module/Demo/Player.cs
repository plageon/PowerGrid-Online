using System.Collections.Generic;

namespace ETModel
{
	public sealed class Player : Entity
	{
		public long UnitId { get; set; }

		public bool IsReady { get; set; }

		public bool IsPassed { get; set; }

		public bool HasBought { get; set; }
		public int Money { get; set; }
		public Dictionary<string,int> Resources { get; set; }
		public List<string> OwnCities;
		public List<string> AllPlayerCities;
		public Dictionary<int,string> OwnPlants { get; set; }
		
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