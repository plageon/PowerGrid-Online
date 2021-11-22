using ETModel;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace ETHotfix
{
	[MessageHandler(AppType.AllServer)]
	public class R2C_TestWaitingMessageHandler : AMHandler<C2R_TestHelloMsg>
	{
		protected override async ETTask Run(ETModel.Session session, C2R_TestHelloMsg message)
		{
			Log.Debug(message.SayMessage);
			Player player=Game.Scene.GetComponent<PlayerComponent>().Get(session.GetComponent<SessionPlayerComponent>().Player.Id);
			player.IsReady = true;

			StartConfig config = Game.Scene.GetComponent<RealmGateAddressComponent>().GetAddress();
			IPEndPoint innerAddress = config.GetComponent<InnerConfig>().IPEndPoint;
			Session gateSession = Game.Scene.GetComponent<NetInnerComponent>().Get(innerAddress);
			M2M_TestGetReadyResponse response = await gateSession.Call(new M2M_TestGetReadyRequest()) as M2M_TestGetReadyResponse;
			Console.WriteLine(response.Message);
            if (!response.Message.Equals("NotReady"))
            {
				//给所有玩家群发,现在只发了一个
				//Session[] sessions=Game.Scene.GetComponent<SessionK>
				session.Send(new M2C_StartGameBroadCast() { PlayerNum = int.Parse(response.Message), Message = "" });
			}
			//session.Send(new G2C_TestHotfixMessage() { Info = $"Player number: {response.Message}" });
			await ETTask.CompletedTask;

		}
	}
}
