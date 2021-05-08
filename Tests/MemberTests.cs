using System;
using Xunit;
using Assignment;
using static ExampleData.ExampleUsers;
using static ExampleData.ExampleTools;

namespace Tests
{
    public class Shared
    {
        private LibrarySystem library;

        public Shared()
        {
            this.library = new LibrarySystem(new ToolCollection(), new MemberCollection());
        }

        public class UnitTests
        {
            public UnitTests(Shared shared)
            {
                this.shared = shared;
                this.library = new LibrarySystem(new ToolCollection(), new MemberCollection());
            }

            Shared shared;
            LibrarySystem library;

            [Fact]
            void AddMembersToLibrary()
            {
                library.add(Liran);
                Member member = library.GetMember(Liran.FullName);
                Assert.Equal(Liran, member);
            }
        }
    }
}
