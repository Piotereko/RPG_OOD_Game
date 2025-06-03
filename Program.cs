using RPG_wiedzmin_wanna_be.Controller;
using RPG_wiedzmin_wanna_be.Model.Game;

namespace RPG_wiedzmin_wanna_be
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*GameEngine game = new GameEngine();
            game.Run();*/
            GameController controller = new GameController();
            controller.Run(args);
        }
    }
}
    