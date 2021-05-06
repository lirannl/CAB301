using System;
using static Assignment.Utils;

namespace Assignment {
    abstract class Menu {
        public abstract Tuple<string, LibAction>[] options {
            get;
        }
        public abstract string name {
            get;
        }
        public bool main = false;
        public void Run(ref LibrarySystem library)
        {
            string initMessage = padString(name, '=', 10);
            string zeroAction;
            if (main)
                zeroAction = "exit";
            else
                zeroAction = "return to main menu";
            Console.WriteLine(initMessage);
            // Initialise selection with an impossible value
            int selection = -2;
            // Print each option
            for (int i = 0; i < options.Length; i++)
            {
                Console.WriteLine(
                    String.Format("{0}. {1}", i+1, options[i].Item1)
                );
            }
            // Print the back/exit option
            Console.WriteLine("0. " + 
            // With the first letter capitalised
                zeroAction.Substring(0, 1).ToUpper() + 
                zeroAction.Substring(1));
            Console.WriteLine(new String('=', initMessage.Length));
            // -1 is the exit option, otherwise it's the index of the selected option
            do
            {
                Console.Write(
                    String.Format("Please make a selection (1-{0}, or 0 to {1}): ", 
                    options.Length, zeroAction)
                );
                // Read a selection made by the user
                var selectionStr = Console.ReadLine();
                try {
                    // Try interpreting the selection
                    selection = int.Parse(selectionStr) - 1;
                    // Throw an exception of the selection is an invaild number
                    if (selection < -1 || selection >= options.Length)
                        throw new IndexOutOfRangeException();
                }
                // If the user didn't enter a valid selection
                catch (Exception ex) {
                    if (ex is FormatException || ex is IndexOutOfRangeException)
                    {
                        // Notify the user
                        Console.WriteLine("\nInvalid selection.");
                        // Ask again
                        selection = -2;
                    }
                    else throw;
                }
            } while(selection == -2);
            // If a non-exit has been selected, perform the selected action
            if (selection >= 0)
                options[selection].Item2(ref library);
            else if (main) Environment.Exit(0);
        }
    }
}