
internal class Program
{
	private static void Main(string[] args)
	{
		using var game = new DesktopGL.Game_DesktopGL();
		game.Run();
	}
}