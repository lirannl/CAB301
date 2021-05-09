using Assignment;
using System.IO;
using System.Linq;

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
        // Borrow a tool over and over
        public static void multiBorrow(this LibrarySystem library,
            iMember member,
            iTool tool,
            int times)
        {
            foreach (var i in Enumerable.Range(0, times))
            {
                library.borrowTool(member, tool);
                library.returnTool(member, tool);
            }
        }
    }
}