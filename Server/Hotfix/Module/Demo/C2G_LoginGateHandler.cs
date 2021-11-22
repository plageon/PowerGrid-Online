using System;
using ETModel;

namespace ETHotfix
{
	[MessageHandler(AppType.Gate)]
	public class C2G_LoginGateHandler : AMRpcHandler<C2G_LoginGate, G2C_LoginGate>
	{
		protected override async ETTask Run(Session session, C2G_LoginGate request, G2C_LoginGate response, Action reply)
		{
			string account = Game.Scene.GetComponent<GateSessionKeyComponent>().Get(request.Key);
			if (account == null)
			{
				response.Error = ErrorCode.ERR_ConnectGateKeyError;
				response.Message = "Gate key验证失败!";
				reply();
				return;
			}
			Player player = ComponentFactory.Create<Player, string>(account);
			player.IsReady = false;
			player.IsPassed = false;
			player.HasBoughtOrQuit = false;
			player.Money = 50;
			player.OwnCities = new System.Collections.Generic.List<string>();
			player.OwnPlants = new System.Collections.Generic.Dictionary<int, string>();
			player.UniqueSession = session;
			Game.Scene.GetComponent<PlayerComponent>().Add(player);
			session.AddComponent<SessionPlayerComponent>().Player = player;
			session.AddComponent<MailBoxComponent, string>(MailboxType.GateSession);
			Console.WriteLine("player " + Game.Scene.GetComponent<PlayerComponent>().Count.ToString() + " Id " + player.Id.ToString());
			response.PlayerId = player.Id;
			reply();

			//session.Send(new G2C_TestHotfixMessage() { Info = "recv hotfix message success" });
			await ETTask.CompletedTask;
		}
	}
}