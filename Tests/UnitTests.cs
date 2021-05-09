using Xunit;
using Assignment;
using static ExampleData.ExampleUsers;
using static ExampleData.ExampleTools;
using System.Collections.Generic;

namespace Tests
{

    public class UnitTests
    {
        public UnitTests()
        {
            tools = new Dictionary<string, ToolCollection>();
            members = new MemberCollection();
            library = new LibrarySystem(tools, members);
        }
        LibrarySystem library;
        Dictionary<string, ToolCollection> tools;
        MemberCollection members;

        [Fact]
        void AddMembersToCollection01()
        {
            members.add(Liran);
            members.add(James);
            members.add(Dan);

            Assert.Equal(3, members.Number);
        }
        [Fact]
        void AddMembersToCollection02()
        {
            members.add(Jane);
            members.add(James);
            members.add(Dan);
            members.add(Rachel);

            Assert.Equal(4, members.Number);
        }
        [Fact]
        void TestSorting01()
        {
            members.add(Liran);
            members.add(James);
            // Ensure the new member was inserted in the prev node
            Assert.NotNull(members.root.prev);
        }
        [Fact]
        void TestSorting02()
        {
            members.add(Jane);
            members.add(Jack);
            members.add(Dan);
            members.add(Rachel);
            members.add(Liran);
            // Ensure that members were properly inserted
            Assert.NotNull(members.root.prev.next);
            Assert.NotNull(members.root.next);
        }
        [Fact]
        // A test that involves both insertion, and removal of members
        void TestSorting03()
        {
            members.add(Jane);
            members.add(Jack);
            members.add(Dan);
            members.add(Rachel);
            members.add(Liran);
            Assert.NotNull(members.root.prev.next);
            members.delete(Dan);
            Assert.Null(members.root.prev.next);
            Assert.NotNull(members.root.prev);
            members.delete(Jane);
            Assert.NotNull(members.root);
        }

        [Fact]
        void AddMembersToLibrary01()
        {
            library.add(Liran);
            Member member = library.GetMember(Liran.FullName);
            Assert.Equal(Liran, member);
        }
        [Fact]
        void AddMembersToLibrary02()
        {
            library.add(Jane);
            Member member = library.GetMember(Jane.FullName);
            Assert.Equal(Jane, member);
        }
        [Fact]
        void AddToolsToLibrary01()
        {
            library.addWithCategory(Chisel, "Default");
            library.addWithCategory(Crayon, "Default");
            library.addWithCategory(Scissors, "Default");
            library.addWithCategory(Wire, "Default");

            Assert.Equal(4, tools["Default"].Number);
        }
        [Fact]
        void OverBorrowTest()
        {
            // Add tools
            library.addWithCategory(Chisel, "Default");
            library.addWithCategory(Crayon, "Default");
            library.addWithCategory(Scissors, "Default");
            library.addWithCategory(Wire, "Default");
            library.addWithCategory(Rubber, "Default");

            // Add a member
            library.add(Liran);

            // Borrow the max number of items
            library.borrowTool(Liran, Chisel);
            library.borrowTool(Liran, Crayon);
            library.borrowTool(Liran, Scissors);

            // Try borrowing another
            Assert.Throws<OverBorrowedException>(() => library.borrowTool(Liran, Wire));
        }
        [Fact]
        void AnalyticsTest()
        {
            // Add tools
            library.addWithCategory(Chisel, "Default");
            library.addWithCategory(Crayon, "Default");
            library.addWithCategory(Scissors, "Default");
            library.addWithCategory(Wire, "Default");
            library.addWithCategory(Rubber, "Default");

            // Add a member
            library.add(Liran);

            // Borrow tools
            library.multiBorrow(Liran, Chisel, 10);
            library.multiBorrow(Liran, Crayon, 4);
            library.multiBorrow(Liran, Scissors, 3);
            library.multiBorrow(Liran, Wire, 2);
            library.multiBorrow(Liran, Rubber, 1);

            // Allow reading the text output from the library
            var Output = new System.IO.StringWriter();
            library.Output = Output;
        }
    }
}