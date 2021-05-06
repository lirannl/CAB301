using System;
using static Assignment.Utils;

namespace Assignment
{
    class MainMenu : Menu
    {
        public override string name => "Main Menu";
        readonly Tuple<string, LibAction>[] opts = new Tuple<string, LibAction>[] {
            
        };
        public override Tuple<string, LibAction>[] options => opts;
        // Classify this as the main menu
        public MainMenu() {
            main = true;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            // Initialise the library system
            var prog = new Program();
            var library = new LibrarySystem(new ToolCollection(), new MemberCollection());
            var exit = false;
            // Greet the user
            Console.WriteLine("Welcome to the Tool Library");
            // Run the main menu in a loop
            while (!exit)
            {
                exit = new MainMenu().Run(ref library);
            }
        }
    }
}
