using Interfaces;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Assignment
{
    class LibrarySystem : iToolLibrarySystem
    {
        Dictionary<string, ToolCollection> tools = new Dictionary<string, ToolCollection>();
        MemberCollection members = new MemberCollection();
        Dictionary<string, int> freqs;

        public Member GetMember(string FullName)
        {
            iMember member = members.get(FullName);
            return member as Member;
        }

        public Tool GetTool(string name)
        {
            iTool tool = null;
            Exception exception = null;
            foreach (var category in tools)
            {
                try {
                    tool = category.Value.get(name);
                }
                catch (Exception ex)
                {
                    exception = ex;
                }
            }
            if (tool == null)
                throw exception;
            return tool as Tool;
        }
        public LibrarySystem(ToolCollection tools, MemberCollection members)
        {
            freqs = new Dictionary<string, int>();
        }

        public void add(iTool tool)
        {
            Console.Write("Please enter the tool's category: ");
            var category = Console.ReadLine();
            if (!tools.ContainsKey(category))
                tools.Add(category, new ToolCollection());
            tools[category].add(tool);
        }

        public void add(iTool tool, int amount)
        {
            if (amount <= 0) throw new ArgumentException("Amount must be a positive number");
            iTool existingTool = GetTool(tool.Name);
            existingTool.Quantity += amount;
        }

        public void add(iMember member)
        {
            members.add(member);
        }

        public void borrowTool(iMember member, iTool tool)
        {
            iTool existingTool = GetTool(tool.Name);
            ref iMember existingMember = ref members.get(((Member)member).FullName);
            // If the member is already borrowing 3 tools, don't allow the borrowing to go through
            if (existingMember.Tools.Length >= 3) throw new OverBorrowedException();
            // Register that the tool has said member borrowing it
            existingTool.addBorrower(member);
            // Register that the member now holds this tool
            existingMember.addTool(existingTool);
            // Register a record of the borrowing
            {
                if (freqs.ContainsKey(existingTool.Name))
                    freqs[existingTool.Name]++;
                else freqs.Add(existingTool.Name, 1);
            }
        }

        public void delete(iTool tool)
        {
            Exception[] errors = new Exception[tools.Keys.Count];
            foreach (var category in tools)
            {
                try {category.Value.delete(tool);}
                catch {}
            }
        }

        public void delete(iTool tool, int amount)
        {
            if (amount <= 0) throw new ArgumentException("Amount must be a positive number");
            iTool existingTool = GetTool(tool.Name);
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
            iMember existingMember = null;
            foreach (var member in members.toArray())
            {
                if (member.ContactNumber == contactNumber) existingMember = member;
            }
            if (existingMember == null) 
                throw new IndexOutOfRangeException("Member with contact number not found.");
            foreach (var toolName in existingMember.Tools)
                Console.WriteLine(toolName);
        }

        public void displayTools(string toolType)
        {
            try {
                foreach(var tool in tools[toolType].toArray())
                {
                    Console.WriteLine(tool.Name);
                }
            }
            catch (KeyNotFoundException)
            {
                Console.WriteLine("There are no tools of this type.");
            }
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
            iMember existingMember = members.get(((Member)member).FullName);
            return existingMember.Tools;
        }

        public void returnTool(iMember member, iTool tool)
        {
            // Access existing member and tool
            ref iMember existingMember = ref members.get(((Member)member).FullName);
            iTool existingTool = GetTool(tool.Name);

            if (!existingMember.Tools.Contains(existingTool.Name))
                throw new KeyNotFoundException();
            existingMember.deleteTool(existingTool);
            existingTool.deleteBorrower(existingMember);

        }
    }
}