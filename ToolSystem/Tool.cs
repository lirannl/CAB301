using System;

namespace Assignment
{
    class Tool : iTool
    {
        string name;
        int quantity;
        MemberCollection borrowers;
        public string Name { get => name; set => name = value; }
        public int Quantity { get => quantity; set => quantity = value; }
        public int AvailableQuantity { get => quantity - borrowers.Number; set => throw new System.NotImplementedException(); }
        public int NoBorrowings { get => borrowers.Number; set => throw new System.NotImplementedException(); }

        public iMemberCollection GetBorrowers => borrowers;

        public void addBorrower(iMember member)
        {
            borrowers.add(member);
        }

        public void deleteBorrower(iMember member)
        {
            borrowers.delete(member);
        }
    }
}