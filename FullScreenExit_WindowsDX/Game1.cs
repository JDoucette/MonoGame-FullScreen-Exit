// Jason Allen Doucette
// September 4, 2023
//
// Version:
//	-	MonoGame 3.8.1.303
//	-	MonoGame.Framework.WindowsDX 3.8.1.303
//
// Reference Links:
//	-	https://github.com/JDoucette/MonoGame-FullScreen-Exit
//	-	https://github.com/MonoGame/MonoGame/issues/8063
//	-	https://community.monogame.net/t/full-screen-from-windowed-hangs-on-exit/19361
//
// Repro:
//	1.	Game starts in windowed mode
//		(NOTE: if the game starts in full-screen, all is well.)
//	2.	Press "F to toggle into full screen
//		(NOTE: if the game is toggled back to windowed mode, all is well.)
//	3.	Press "ESC" to exit the app while in full screen
//
// Result:
//	-	When fullscreen mode is set, exit hangs.
//		-	In Release mode: Task Manager moves the app from "Apps" to "Background processes"
//		-	In Debug mode:

/*
THREADS:

	16552	9	Worker Thread	.NET System Events	System.Private.CoreLib.dll!System.Threading.WaitHandle.WaitOneNoCheck
	6940	4	Worker Thread	.NET ThreadPool Gate	System.Private.CoreLib.dll!System.Threading.WaitHandle.WaitOneNoCheck
	12520	3	Worker Thread	.NET ThreadPool Worker	System.Private.CoreLib.dll!Interop.Kernel32.GetQueuedCompletionStatus
	14468	5	Worker Thread	.NET ThreadPool Worker	System.Private.CoreLib.dll!Interop.Kernel32.GetQueuedCompletionStatus
	14156	8	Worker Thread	.NET ThreadPool Worker	System.Private.CoreLib.dll!Interop.Kernel32.GetQueuedCompletionStatus
	2532	0	Worker Thread	<No Name>	
	18208	0	Worker Thread	<No Name>	
	18512	0	Worker Thread	<No Name>	System.Private.CoreLib.dll!System.Threading.Thread.Join
	11612	1	Main Thread	Main Thread	

.NET SYSTEM EVENTS THREAD, CALL STACK:

	16552	9	Worker Thread	.NET System Events	System.Private.CoreLib.dll!System.Threading.WaitHandle.WaitOneNoCheck

	System.Private.CoreLib.dll!System.Threading.WaitHandle.WaitOneNoCheck(int millisecondsTimeout) Line 139	C#
	System.Private.CoreLib.dll!System.Threading.WaitHandle.WaitOne(int millisecondsTimeout) Line 111	C#
	System.Private.CoreLib.dll!System.Threading.WaitHandle.WaitOne(int millisecondsTimeout, bool exitContext) Line 421	C#
	System.Windows.Forms.dll!System.Windows.Forms.Control.WaitForWaitHandle(System.Threading.WaitHandle waitHandle) Line 3896	C#
	System.Windows.Forms.dll!System.Windows.Forms.Control.MarshaledInvoke(System.Windows.Forms.Control caller, System.Delegate method, object[] args, bool synchronous) Line 6979	C#
	System.Windows.Forms.dll!System.Windows.Forms.Control.Invoke(System.Delegate method, object[] args) Line 6426	C#
	System.Windows.Forms.dll!System.Windows.Forms.WindowsFormsSynchronizationContext.Send(System.Threading.SendOrPostCallback d, object state) Line 88	C#
	Microsoft.Win32.SystemEvents.dll!Microsoft.Win32.SystemEvents.SystemEventInvokeInfo.Invoke(bool checkFinalization, object[] args)	Unknown
	Microsoft.Win32.SystemEvents.dll!Microsoft.Win32.SystemEvents.RaiseEvent(bool checkFinalization, object key, object[] args)	Unknown
	Microsoft.Win32.SystemEvents.dll!Microsoft.Win32.SystemEvents.OnUserPreferenceChanging(int msg, System.IntPtr wParam, System.IntPtr lParam)	Unknown
	Microsoft.Win32.SystemEvents.dll!Microsoft.Win32.SystemEvents.WindowProc(System.IntPtr hWnd, int msg, System.IntPtr wParam, System.IntPtr lParam)	Unknown
	[Native to Managed Transition]	
	[Managed to Native Transition]	
	Microsoft.Win32.SystemEvents.dll!Microsoft.Win32.SystemEvents.WindowThreadProc()	Unknown
	System.Private.CoreLib.dll!System.Threading.Thread.StartCallback() Line 106	C#
*/

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FullScreenExit_WindowsDX
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
