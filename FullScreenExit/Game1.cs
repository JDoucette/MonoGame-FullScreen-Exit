// Jason Allen Doucette
// September 4, 2023
//
// Testing:
//	-	Start in windowed mode
//	-	Change to full screen mode
//	-	Exit
//	-	See if the app exits cleanly.

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FullScreenExit
{
	public class Game1 : Game
	{
		private GraphicsDeviceManager graphicsDeviceManager;
		//private SpriteBatch spriteBatch;
		private KeyboardState keyStatePrev;
		private KeyboardState keyStateCurr;

		public Game1()
		{
			graphicsDeviceManager = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
			IsMouseVisible = true;
		}

		protected override void Initialize()
		{
			base.Initialize();
		}

		protected override void LoadContent()
		{
			//spriteBatch = new SpriteBatch(GraphicsDevice);
		}

		protected override void Update(GameTime gameTime)
		{
			keyStateCurr = Keyboard.GetState();
			if (WasJustPressed(Keys.F))
				graphicsDeviceManager.ToggleFullScreen();
			if (WasJustPressed(Keys.Escape))
				Exit();
			base.Update(gameTime);
			keyStatePrev = keyStateCurr;
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);
			base.Draw(gameTime);
		}

		private bool WasJustPressed(Keys key)
		{
			return (keyStatePrev.IsKeyUp(key) && keyStateCurr.IsKeyDown(key));
		}
	}
}
