namespace ETModel
{
	public static class SceneType
	{
		public const string Share = "Share";
		public const string Game = "Game";
		public const string Login = "Login";
		public const string Lobby = "Lobby";
		public const string Map = "Map";
		public const string Launcher = "Launcher";
		public const string Robot = "Robot";
		public const string RobotClient = "RobotClient";
		public const string Realm = "Realm";
		public const string ReadyRoom = "ReadyRoom";
		public const string Germany = "Germany";
	}
	
	public sealed class Scene: Entity
	{
		public string Name { get; set; }

		public Scene()
		{
		}

		public Scene(long id): base(id)
		{
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