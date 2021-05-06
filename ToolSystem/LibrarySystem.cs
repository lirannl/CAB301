using Interfaces;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Assignment
{
    class LibrarySystem : iToolLibrarySystem
    {
        ToolCollection tools = new ToolCollection();
        MemberCollection members = new MemberCollection();
        Dictionary<string, int> freqs;

        public Member GetMember(string FullName)
        {
            ref iMember member = ref members.get(FullName);
            return member as Member;
        }

        public LibrarySystem(ToolCollection tools, MemberCollection members)
        {
            freqs = new Dictionary<string, int>();
        }

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
            // Register a record of the borrowing
            {
                if (freqs.ContainsKey(existingTool.Name))
                    freqs[existingTool.Name]++;
                else freqs.Add(existingTool.Name, 1);
            }
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
        // Given a contact number, print all of the tools the member has
        public void display(string contactNumber)
        {
            iMember existingMember = members.get(new Member(null, null, contactNumber, null));
            foreach (var toolName in existingMember.Tools)
                Console.WriteLine(toolName);
        }

        public void displayTools(string toolType)
        {
            throw new NotImplementedException();
            // Tool types?
        }

        public void displayTopThree()
        {
            foreach (var topFreq in 
                freqs.OrderBy(freq => freq.Value).Take(3))
            {
                Console.WriteLine(String.Format(
                    "{0} has been borrowed a total of {1} times.",
                    topFreq.Key, topFreq.Value
                ));
            }
        }

        public string[] listTools(iMember member)
        {
            iMember existingMember = members.get(member);
            return existingMember.Tools;
        }

        public void returnTool(iMember member, iTool tool)
        {
            // Access existing member and tool
            ref iMember existingMember = ref members.get(member);
            ref iTool existingTool = ref tools.get(tool);

            if (!existingMember.Tools.Contains(existingTool.Name))
                throw new KeyNotFoundException();
            existingMember.deleteTool(existingTool);
            existingTool.deleteBorrower(existingMember);

        }
    }
}