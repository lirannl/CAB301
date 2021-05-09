using System;

namespace Assignment
{
    using MenuOption = Tuple<string, Action<LibrarySystem>>;
    class MainMenu : Menu
    {
        public override string name => "Main Menu";
        static void MemberLogin(LibrarySystem library)
        {
            Console.Write("Enter username: ");
            var username = Console.ReadLine();
            Console.Write("Enter pin: ");
            var pin = Console.ReadLine();
            Member member;
            try
            {
                member = library.GetMember(username);
            }
            catch
            {
                Console.WriteLine("\nLogin Failed.\n");
                return;
            }
            // If authentication failed, go back to the main menu
            if (member.PIN != pin) { Console.WriteLine("\nLogin failed.\n"); return; }
            new MemberMenu(member).Run(library);
        }

        static void StaffLogin(LibrarySystem library)
        {
            Console.Write("Enter username: ");
            var username = Console.ReadLine();
            Console.Write("Enter password: ");
            var password = Console.ReadLine();
            if (username == "staff" && password == "today123")
                // If authentication is successful, go to the menu
                new StaffMenu().Run(library);
            else Console.WriteLine("\nLogin failed.\n");
        }
        readonly MenuOption[] opts = new MenuOption[] {
            new MenuOption("Staff Login", StaffLogin),
            new MenuOption("Member Login", MemberLogin)
        };
        public override MenuOption[] options => opts;
        public MainMenu() { this.main = true; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            // Initialise the library system
            var prog = new Program();
            var library = new LibrarySystem(
                new System.Collections.Generic.Dictionary<string, ToolCollection>(),
                new MemberCollection()
            );
            // Greet the user
            Console.WriteLine("Welcome to the Tool Library");
            // Run the main menu in a loop
            while (true)
                new MainMenu().Run(library);
        }
    }
}
