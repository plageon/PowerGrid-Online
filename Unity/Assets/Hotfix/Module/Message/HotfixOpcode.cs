using ETModel;
namespace ETHotfix
{
	[Message(HotfixOpcode.C2R_Login)]
	public partial class C2R_Login : IRequest {}

	[Message(HotfixOpcode.R2C_Login)]
	public partial class R2C_Login : IResponse {}

	[Message(HotfixOpcode.C2G_LoginGate)]
	public partial class C2G_LoginGate : IRequest {}

	[Message(HotfixOpcode.G2C_LoginGate)]
	public partial class G2C_LoginGate : IResponse {}

	[Message(HotfixOpcode.G2C_TestHotfixMessage)]
	public partial class G2C_TestHotfixMessage : IMessage {}

	[Message(HotfixOpcode.M2M_TestGetReadyRequest)]
	public partial class M2M_TestGetReadyRequest : IRequest {}

	[Message(HotfixOpcode.M2M_TestGetReadyResponse)]
	public partial class M2M_TestGetReadyResponse : IResponse {}

	[Message(HotfixOpcode.C2M_TestActorRequest)]
	public partial class C2M_TestActorRequest : IActorLocationRequest {}

	[Message(HotfixOpcode.M2C_TestActorResponse)]
	public partial class M2C_TestActorResponse : IActorLocationResponse {}

	[Message(HotfixOpcode.PlayerInfo)]
	public partial class PlayerInfo : IMessage {}

	[Message(HotfixOpcode.C2G_PlayerInfo)]
	public partial class C2G_PlayerInfo : IRequest {}

	[Message(HotfixOpcode.G2C_PlayerInfo)]
	public partial class G2C_PlayerInfo : IResponse {}

	[Message(HotfixOpcode.C2R_TestHelloMsg)]
	public partial class C2R_TestHelloMsg : IMessage {}

	[Message(HotfixOpcode.C2R_TestRequest)]
	public partial class C2R_TestRequest : IRequest {}

	[Message(HotfixOpcode.R2C_TestResponse)]
	public partial class R2C_TestResponse : IResponse {}

	[Message(HotfixOpcode.C2M_AuctionRequest)]
	public partial class C2M_AuctionRequest : IRequest {}

	[Message(HotfixOpcode.M2C_AuctionResponse)]
	public partial class M2C_AuctionResponse : IResponse {}

	[Message(HotfixOpcode.M2C_AuctionReminder)]
	public partial class M2C_AuctionReminder : IMessage {}

	[Message(HotfixOpcode.M2C_GiveupPlant)]
	public partial class M2C_GiveupPlant : IRequest {}

	[Message(HotfixOpcode.C2M_GiveupPlant)]
	public partial class C2M_GiveupPlant : IResponse {}

	[Message(HotfixOpcode.C2M_RefreshMarket)]
	public partial class C2M_RefreshMarket : IRequest {}

	[Message(HotfixOpcode.M2C_RefreshMarket)]
	public partial class M2C_RefreshMarket : IResponse {}

	[Message(HotfixOpcode.M2C_InitAuction)]
	public partial class M2C_InitAuction : IMessage {}

	[Message(HotfixOpcode.M2C_FinishAuction)]
	public partial class M2C_FinishAuction : IMessage {}

	[Message(HotfixOpcode.M2C_StartGameBroadCast)]
	public partial class M2C_StartGameBroadCast : IActorMessage {}

	[Message(HotfixOpcode.M2C_AskBid)]
	public partial class M2C_AskBid : IActorMessage {}

	[Message(HotfixOpcode.M2C_OwnPlant)]
	public partial class M2C_OwnPlant : IMessage {}

	[Message(HotfixOpcode.C2M_BuyResourceRequest)]
	public partial class C2M_BuyResourceRequest : IRequest {}

	[Message(HotfixOpcode.M2C_BuyResourceResponse)]
	public partial class M2C_BuyResourceResponse : IResponse {}

	[Message(HotfixOpcode.M2C_ResourceReminder)]
	public partial class M2C_ResourceReminder : IMessage {}

	[Message(HotfixOpcode.M2C_ResourceMarketFinish)]
	public partial class M2C_ResourceMarketFinish : IMessage {}

	[Message(HotfixOpcode.C2M_RefreshResourceMarket)]
	public partial class C2M_RefreshResourceMarket : IMessage {}

	[Message(HotfixOpcode.M2C_InitLayingCircuit)]
	public partial class M2C_InitLayingCircuit : IMessage {}

	[Message(HotfixOpcode.M2C_FinishLayingCircuit)]
	public partial class M2C_FinishLayingCircuit : IMessage {}

	[Message(HotfixOpcode.C2M_RefreshPipelineMarket)]
	public partial class C2M_RefreshPipelineMarket : IMessage {}

	[Message(HotfixOpcode.M2C_LayingCircuitReminder)]
	public partial class M2C_LayingCircuitReminder : IMessage {}

	[Message(HotfixOpcode.C2M_BuyLineRequest)]
	public partial class C2M_BuyLineRequest : IRequest {}

	[Message(HotfixOpcode.M2C_BuyLineResponse)]
	public partial class M2C_BuyLineResponse : IResponse {}

	[Message(HotfixOpcode.C2M_QuitBuyLine)]
	public partial class C2M_QuitBuyLine : IMessage {}

	[Message(HotfixOpcode.M2C_CityOwnerUpdate)]
	public partial class M2C_CityOwnerUpdate : IMessage {}

	[Message(HotfixOpcode.C2M_GenerateEleRequest)]
	public partial class C2M_GenerateEleRequest : IRequest {}

	[Message(HotfixOpcode.M2C_GenerateEleResponse)]
	public partial class M2C_GenerateEleResponse : IResponse {}

	[Message(HotfixOpcode.M2C_FinishRound)]
	public partial class M2C_FinishRound : IMessage {}

}
namespace ETHotfix
{
	public static partial class HotfixOpcode
	{
		 public const ushort C2R_Login = 10001;
		 public const ushort R2C_Login = 10002;
		 public const ushort C2G_LoginGate = 10003;
		 public const ushort G2C_LoginGate = 10004;
		 public const ushort G2C_TestHotfixMessage = 10005;
		 public const ushort M2M_TestGetReadyRequest = 10006;
		 public const ushort M2M_TestGetReadyResponse = 10007;
		 public const ushort C2M_TestActorRequest = 10008;
		 public const ushort M2C_TestActorResponse = 10009;
		 public const ushort PlayerInfo = 10010;
		 public const ushort C2G_PlayerInfo = 10011;
		 public const ushort G2C_PlayerInfo = 10012;
		 public const ushort C2R_TestHelloMsg = 10013;
		 public const ushort C2R_TestRequest = 10014;
		 public const ushort R2C_TestResponse = 10015;
		 public const ushort C2M_AuctionRequest = 10016;
		 public const ushort M2C_AuctionResponse = 10017;
		 public const ushort M2C_AuctionReminder = 10018;
		 public const ushort M2C_GiveupPlant = 10019;
		 public const ushort C2M_GiveupPlant = 10020;
		 public const ushort C2M_RefreshMarket = 10021;
		 public const ushort M2C_RefreshMarket = 10022;
		 public const ushort M2C_InitAuction = 10023;
		 public const ushort M2C_FinishAuction = 10024;
		 public const ushort M2C_StartGameBroadCast = 10025;
		 public const ushort M2C_AskBid = 10026;
		 public const ushort M2C_OwnPlant = 10027;
		 public const ushort C2M_BuyResourceRequest = 10028;
		 public const ushort M2C_BuyResourceResponse = 10029;
		 public const ushort M2C_ResourceReminder = 10030;
		 public const ushort M2C_ResourceMarketFinish = 10031;
		 public const ushort C2M_RefreshResourceMarket = 10032;
		 public const ushort M2C_InitLayingCircuit = 10033;
		 public const ushort M2C_FinishLayingCircuit = 10034;
		 public const ushort C2M_RefreshPipelineMarket = 10035;
		 public const ushort M2C_LayingCircuitReminder = 10036;
		 public const ushort C2M_BuyLineRequest = 10037;
		 public const ushort M2C_BuyLineResponse = 10038;
		 public const ushort C2M_QuitBuyLine = 10039;
		 public const ushort M2C_CityOwnerUpdate = 10040;
		 public const ushort C2M_GenerateEleRequest = 10041;
		 public const ushort M2C_GenerateEleResponse = 10042;
		 public const ushort M2C_FinishRound = 10043;
	}
}
