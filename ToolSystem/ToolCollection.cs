using System;
namespace Assignment
{
    class ToolCollection : iToolCollection
    {
        // An array storing the actual tools in the collection
        iTool[] tools;

        public ToolCollection()
        {
            this.tools = new iTool[0];
        }

        // Access the numebr of tools
        public int Number => tools.Length;

        // Add a new tool to the collection
        public void add(iTool tool)
        {
            var newTools = new iTool[Number + 1];
            // Set the final tool in the new array to be the new one
            // Since array access is O(1), there's no reason not to 
            // set the values in any arbitrary order.
            newTools[newTools.Length - 1] = tool;
            // Copy all other tools from the old array into the new one
            tools.CopyTo(newTools, 0);
            // Reassign the tools array
            tools = newTools;
        }

        // Remove a tool from the collection
        public void delete(iTool tool)
        {
            var newTools = new iTool[Number - 1];
            int insertionIndex = 0;
            foreach (var currentT in tools)
                // If the tool isn't the deletion target - insert it into the new array.
                if (currentT != tool) try
                    {
                        newTools[insertionIndex] = currentT;
                        insertionIndex++;
                    }
                    // If none of the elements in the collection match the given tool, an attempt to insert outside the newTools' bounds will occur
                    catch (IndexOutOfRangeException)
                    {
                        throw new ArgumentException(String.Format("{0} wasn't in the collection and therefore couldn't be removed.", tool.Name));
                    }
            // Reassign the tools array
            tools = newTools;
        }

        // Find if a specific tool is in the collection
        public bool search(iTool tool)
        {
            try
            {
                get(tool.Name);
                return true;
            }
            catch (IndexOutOfRangeException) { return false; }
        }

        // Get a tool from the collection
        public ref iTool get(string name)
        {
            for (int i = 0; i < tools.Length; i++)
                if (tools[i].Name == name) return ref tools[i];
            // After going through all tools without any of them matching, that means that it isn't in the collection.
            throw new IndexOutOfRangeException("Tool wasn't found in the collection.");
        }

        // Return the tools array
        public iTool[] toArray() => tools;
    }
}