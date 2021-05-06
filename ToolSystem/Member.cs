using System.Collections.Generic;
using System.Linq;
namespace Assignment {
    class Member : iMember
    {
        string firstName;
        string lastName;
        string number;
        string pin;
        ToolCollection borrowed;

        public Member(string firstName, string lastName, string number, string pin)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.number = number;
            this.pin = pin;
            this.borrowed = new ToolCollection();
        }
        // Dummy member - must never be inserted into a collection
        public Member(string fullName)
        {
            this.lastName = fullName;
        }

        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public string ContactNumber { get => number; set => number = value; }
        public string PIN { get => pin; set => pin = value; }

        public string FullName { get => LastName + FirstName; }

        public string[] Tools => borrowed.toArray().Select(tool => tool.Name).ToArray();

        public void addTool(iTool tool)
        {
            borrowed.add(tool);
        }

        public void deleteTool(iTool tool)
        {
            borrowed.delete(tool);
        }

        // Two members are equal if they have the same contact number
        public override bool Equals(object obj)
        {
            return obj is Member member &&
                   ContactNumber == member.ContactNumber;
        }

        public static bool operator==(Member first, iMember second) =>
            first.Equals(second);
        
        public static bool operator!=(Member first, iMember second) =>
            !first.Equals(second);

        public override int GetHashCode() =>
            GetHashCode();
    }
}