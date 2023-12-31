﻿// Jason Allen Doucette
// September 4, 2023
//
// Version:
//	-	MonoGame 3.8.1.303
//	-	MonoGame.Framework.DesktopGL 3.8.1.303
//
// Reference Links:
//	-	https://github.com/JDoucette/MonoGame-FullScreen-Exit
//	-	https://github.com/MonoGame/MonoGame/issues/8063
//	-	https://community.monogame.net/t/full-screen-from-windowed-hangs-on-exit/19361
//
// Repro:
//	1.	Game starts in windowed mode
//	2.	Press "F to toggle into full screen
//	3.	Press "ESC" to exit the app while in full screen
//
// Result:
//	-	GOOD:
//		-	No hang on fullscreen exit.
//		-	No double window during fullscreen.
//	-	BAD:
//		-	Other apps that are maximized are resized.

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DesktopGL
{
	public class Game_DesktopGL : Game
	{
		private GraphicsDeviceManager graphicsDeviceManager;
		//private SpriteBatch spriteBatch;
		private KeyboardState keyStatePrev;
		private KeyboardState keyStateCurr;

		public Game_DesktopGL()
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
