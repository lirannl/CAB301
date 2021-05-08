using System;
using Xunit;
using Assignment;
using static ExampleData.ExampleUsers;
using static ExampleData.ExampleTools;

namespace Tests
{

    public class MemberTests
    {
        public MemberTests()
        {
            members = new MemberCollection();
            library = new LibrarySystem(new ToolCollection(), members);
        }
        LibrarySystem library;
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

            Assert.NotNull(members.root.prev.next);
            Assert.NotNull(members.root.next);
        }
        [Fact]
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
    }
}

