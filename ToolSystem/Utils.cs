using System;

namespace Assignment {
    static class Utils {
        // Given a string, pad it on both sides equally
        public static string padString(string input, char padding, int n)
        {
            string pad = "";
            for (int i = 0; i < n; i++)
            {
                pad += padding;
            }
            return string.Format("{0}{1}{0}", pad, input);
        }
        public static int ReadInt(
            string prompt,
            int min = int.MinValue, int max = int.MaxValue
        )
        {
            int returnVal = 0;
            bool success = false;
            while (!success)
            {
                Console.Write(prompt);
                try {
                    returnVal = int.Parse(Console.ReadLine());
                    if (returnVal < min || returnVal > max)
                        throw new IndexOutOfRangeException();
                    success = true;
                }
                catch (Exception ex)
                {
                    if (ex is FormatException || ex is IndexOutOfRangeException)
                    Console.WriteLine("Invalid number.");
                }
            }
            return returnVal;
        }
        public static Tool GetTool(LibrarySystem library)
        {
            Tool returnVal = null;
            while (returnVal == null)
            {
                Console.Write("Enter tool name: ");
                var name = Console.ReadLine();
                try {
                    returnVal = library.GetTool(name);
                }
                catch (IndexOutOfRangeException)
                {
                    Console.WriteLine("No such tool.");
                }
            }
            return returnVal;
        }
        public static Member GetMember(LibrarySystem library)
        {
            Member initMember = new Member("", "", "", "");
            Member member = initMember;
            while (member == initMember)
            {
                Console.Write("Enter the member's first name: ");
                var firstName = Console.ReadLine();
                Console.Write("Enter the member's last name: ");
                var lastName = Console.ReadLine();
                try {
                    member = library.GetMember(lastName + firstName);
                }
                catch (Exception ex) {
                    Console.WriteLine(ex.Message);
                }
            }
            return member;
        }
    }
}