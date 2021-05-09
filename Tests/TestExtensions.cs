using Assignment;
using System.IO;

namespace Tests
{
static class TestExtensions
    {
        // Test the addition of a tool with a custom category
        public static void addWithCategory(this LibrarySystem lib, iTool tool, string category)
        {
            lib.Input = new StringReader(category + "\n");
            lib.add(tool);
        }

    }
}