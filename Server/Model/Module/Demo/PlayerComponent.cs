using System.Collections.Generic;
using System.Linq;

namespace ETModel
{
	[ObjectSystem]
	public class PlayerComponentSystem : AwakeSystem<PlayerComponent>
	{
		public override void Awake(PlayerComponent self)
		{
			self.Awake();
		}
	}
	
	public class PlayerComponent : Component
	{
		public static PlayerComponent Instance { get; private set; }

		public Player MyPlayer;

		public int MaxPrice { get; set; }
		public long MaxPriceId { get; set; }
		public int NowOrder { get; set; }
		public int AuctionOrder { get; set; }//小循环
		public int RoundCount { get; set; }
		public bool isNewRound { get; set; }
		public int FinishCount { get; set; }

		public Dictionary<long, bool> Plantlists { get; set; }
		public int currentPlantId { get; set; }

		public int[] NowMarkets { get; set; }

		private readonly Dictionary<long, Player> idPlayers = new Dictionary<long, Player>();

		public Dictionary<int,Player> playerOrders { get; set; }
		public ResourceMarket resourceMarket { get; set; }
		public void Awake()
		{
			Instance = this;
			NowOrder = 0;
			AuctionOrder = 0;
			resourceMarket = new ResourceMarket();
			playerOrders = new Dictionary<int, Player>();
			NowMarkets = new int[8];
			RoundCount = 0;
			isNewRound = true;
			FinishCount = 0;
			Plantlists = new Dictionary<long, bool>();
		}
		
		public void Add(Player player)
		{
			this.idPlayers.Add(player.Id, player);
			this.playerOrders.Add(NowOrder, player);
			NowOrder++;
		}

		public Player Get(long id)
		{
			this.idPlayers.TryGetValue(id, out Player gamer);
			return gamer;
		}

		public void Remove(long id)
		{
			this.idPlayers.Remove(id);
		}

		public int Count
		{
			get
			{
				return this.idPlayers.Count;
			}
		}

		public Player[] GetAll()
		{
			return this.idPlayers.Values.ToArray();
		}

		public override void Dispose()
		{
			if (this.IsDisposed)
			{
				return;
			}
			
			base.Dispose();

			foreach (Player player in this.idPlayers.Values)
			{
				player.Dispose();
			}

			Instance = null;
		}
	}
}