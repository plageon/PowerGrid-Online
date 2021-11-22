namespace ETModel
{
	[Config((int)(AppType.ClientH |  AppType.ClientM | AppType.Gate | AppType.Map | AppType.AllServer))]
	public partial class PlantConfigCategory : ACategory<PlantConfig>
	{
	}

	public class PlantConfig: IConfig
	{
		public long Id { get; set; }
		public int Cost;
		public string ResourceType;
		public int ResourceCost;
		public int Output;
		public string Pic;
	}
}
