using System;
using static Assignment.Utils;

namespace Assignment
{
    class MainMenu : Menu
    {
        public override string name => "Main Menu";
        static void MemberLogin(ref LibrarySystem library)
        {
            Console.Write("Enter username:");
            var username = Console.ReadLine();
            Console.Write("Enter pin:");
            var pin = Console.ReadLine();

            Member member = library.GetMember(username);
            new MemberMenu(member).Run(ref library);
        }
        readonly Tuple<string, LibAction>[] opts = new Tuple<string, LibAction>[] {
            new Tuple<string, LibAction>("Staff Login", ),
            new Tuple<string, LibAction>("Member Login", MemberLogin)
        };
        public override Tuple<string, LibAction>[] options => opts;
        public MainMenu(){ this.main = true; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            // Initialise the library system
            var prog = new Program();
            var library = new LibrarySystem(new ToolCollection(), new MemberCollection());
            // Greet the user
            Console.WriteLine("Welcome to the Tool Library");
            // Run the main menu in a loop
            while (true)
                new MainMenu().Run(ref library);
        }
    }
}
