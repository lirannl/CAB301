using System;
using static Assignment.Utils;

namespace Assignment {
    class MemberMenu : Menu
    {
        Member member;
        readonly Tuple<string, LibAction>[] opts = new Tuple<string, LibAction>[] {
            
        };
        public override Tuple<string, LibAction>[] options => opts;
        public override string name => "Member Menu";
        public MemberMenu(Member member)
        {
            this.member = member;
        }
    }
}