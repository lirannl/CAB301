using Interfaces;
using System;

namespace Assignment
{
    class LibrarySystem : iToolLibrarySystem
    {
        ToolCollection tools = new ToolCollection();
        MemberCollection members = new MemberCollection();
        public void add(iTool tool)
        {
            tools.add(tool);
        }

        public void add(iTool tool, int amount)
        {
            if (amount <= 0) throw new ArgumentException("Amount must be a positive number");
            ref iTool existingTool = ref tools.get(tool);
            existingTool.Quantity += amount;
        }

        public void add(iMember member)
        {
            members.add(member);
        }

        public void borrowTool(iMember member, iTool tool)
        {
            ref iTool existingTool = ref tools.get(tool);
            ref iMember existingMember = ref members.get(member);
            // If the member is already borrowing 3 tools, don't allow the borrowing to go through
            if (existingMember.Tools.Length >= 3) throw new OverBorrowedException();
            // Register that the member now holds this tool
            existingMember.addTool(existingTool);
            // Register that the tool has said member borrowing it
            existingTool.addBorrower(member);
        }

        public void delete(iTool tool)
        {
            tools.delete(tool);
        }

        public void delete(iTool tool, int amount)
        {
            if (amount <= 0) throw new ArgumentException("Amount must be a positive number");
            ref iTool existingTool = ref tools.get(tool);
            if (existingTool.AvailableQuantity < amount)
                throw new ArgumentException(String.Format(
                    "Cannot remove {0} pieces of the tool, only {1} are available right now.",
                    amount, existingTool.AvailableQuantity));
            existingTool.Quantity -= amount;
        }

        public void delete(iMember member)
        {
            members.delete(member);
        }

        public void display(string contactNumber)
        {
            iMember existingMember = members.get(new Member());
        }

        public void displayTools(string toolType)
        {
            throw new System.NotImplementedException();
        }

        public void displayTopTHree()
        {
            throw new System.NotImplementedException();
        }

        public string[] listTools(iMember member)
        {
            throw new System.NotImplementedException();
        }

        public void returnTool(iMember member, iTool tool)
        {
            throw new System.NotImplementedException();
        }
    }
}