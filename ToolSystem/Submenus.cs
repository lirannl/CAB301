using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using static Assignment.Utils;

namespace Assignment {
    using MenuOption = Tuple<string, Action<LibrarySystem>>;
    class StaffMenu : Menu
    {
        static void AddTool(LibrarySystem library)
        {
            Console.Write("Enter tool name: ");
            var name = Console.ReadLine();
            int quantity = 
                ReadInt("Enter how many of this tool will be available: ", min: 1);
            iTool tool = new Tool(name, quantity);
            library.add(tool);
        }
        static (int, Tool) ModifyToolPieces(LibrarySystem library, string prompt)
        {
            var tool = Utils.GetTool(library);
            int quantity = Utils.ReadInt(prompt, min: 1);
            return (quantity, tool);
        }
        static void AddPieces(LibrarySystem library)
        {
            var (quantity, tool) = ModifyToolPieces(library, 
                "Enter the amount of pieces to add: ");
            library.add(tool, quantity);
        }
        static void RemovePieces(LibrarySystem library)
        {
            while (true)
            {
                var (quantity, tool) = ModifyToolPieces(library, 
                "Enter the amount of pieces to remove: ");
                try {
                    library.delete(tool, quantity);
                    return;
                }
                catch (ArgumentException ex) {
                    Console.WriteLine(ex.Message);
                }
            }
        }
        static void RegisterMember(LibrarySystem library)
        {
            Console.Write("Enter the member's first name: ");
            var firstName = Console.ReadLine();
            Console.Write("Enter the member's last name: ");
            var lastName = Console.ReadLine();
            var contactNumber = "";
            while (contactNumber == "")
            {
                Console.Write("Enter the member's contact number: ");
                contactNumber = Console.ReadLine();
                if (!new Regex("^[0-9]+$").IsMatch(contactNumber))
                    {
                        contactNumber = "";
                        Console.WriteLine("Invalid contact number.");
                    }
            }
            var PIN = "";
            while (PIN == "")
            {
                Console.Write("Enter the member's PIN: ");
                PIN = Console.ReadLine();
                if (!new Regex("^[0-9]+$").IsMatch(PIN))
                    {
                        PIN = "";
                        Console.WriteLine("Invalid PIN.");
                    }
            }
            library.add(new Member(firstName, lastName, contactNumber, PIN));
        }
        static void RemoveMember(LibrarySystem library)
        {
            Member member = GetMember(library);
            library.delete(member);
        }
        static void FindContactNumber(LibrarySystem library)
        {
            Member member = GetMember(library);
            Console.WriteLine(String.Format(
                "{0} {1} can be contacted at: \"{2}\"", 
                member.FirstName, member.LastName, member.ContactNumber
            ));
        }
        readonly MenuOption[] opts = new MenuOption[] {
            new MenuOption("Add a new tool", AddTool),
            new MenuOption("Add new pieces of an existing tool", AddPieces),
            new MenuOption("Remove some pieces of a tool", RemovePieces),
            new MenuOption("Register a new member", RegisterMember),
            new MenuOption("Remove a member", RemoveMember),
            new MenuOption("Find the contact number of a member", FindContactNumber)
        };
        public override MenuOption[] options => opts;

        public override string name => "Staff Menu";
    }
    class MemberMenu : Menu
    {
        iMember member;
        void Borrow(LibrarySystem library)
        {
            try 
            {
                library.borrowTool(member, GetTool(library));
            }
            catch (OverflowException ex) {
                Console.WriteLine(ex.Message);
            }
            catch (OverBorrowedException) {
                Console.WriteLine(
                    "You have borrowed too many tools. Please return a tool before borrowing more."
                );
            }
        }
        void ReturnTool(LibrarySystem library)
        {
            try { library.returnTool(member, GetTool(library)); }
            catch (KeyNotFoundException)
            {
                Console.WriteLine("You are not holding any of that tool.");
            }
        }
        void ListTools(LibrarySystem library)
        {
            foreach (var tool in member.Tools)
            {
                Console.WriteLine(tool);
            }
        }

        MenuOption[] opts;
        public override MenuOption[] options => opts;

        public new void Run(LibrarySystem library)
        {
            opts = new MenuOption[] {
            new MenuOption("Display all the tools of a tool type", library => {
                Console.Write("Enter tool type: ");
                library.displayTools(Console.ReadLine());
            }),
            new MenuOption("Borrow a tool", Borrow),
            new MenuOption("Return a tool", ReturnTool),
            new MenuOption("List all the tools that I am renting", ListTools),
            new MenuOption("Display top three (3) most frequently rented tools", 
                library => library.displayTopThree())
        };
            (this as Menu).Run(library);
        }
        public override string name => "Member Menu";
        public MemberMenu(Member member)
        {
            this.member = member;
        }
    }
}