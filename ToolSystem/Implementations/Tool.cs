using System;

namespace Assignment
{
    class Tool : iTool
    {
        string name;
        int quantity;
        MemberCollection borrowers;
        public Tool(string name, int quantity)
        {
            this.name = name;
            this.quantity = quantity;
            this.borrowers = new MemberCollection();
        }
        public string Name { get => name; set => name = value; }
        public int Quantity { get => quantity; set => quantity = value; }
        public int AvailableQuantity { get => quantity - borrowers.Number; }
        public int NoBorrowings { get => borrowers.Number; }
        public iMemberCollection GetBorrowers => borrowers;
        public void addBorrower(iMember member)
        {
            if (AvailableQuantity <= 0) throw new OverflowException("Tool is unavailable");
            borrowers.add(member);
        }
        public void deleteBorrower(iMember member)
        {
            borrowers.delete(member);
        }
    }
}