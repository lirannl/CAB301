namespace Assignment
{
    class ToolCollection : iToolCollection
    {
        // An array storing the actual tools in the collection
        iTool[] tools;
        
        // Access the numebr of tools
        public int Number => tools.Length;

        // Add a new tool to the collection
        public void add(iTool tool)
        {
            var newTools = new iTool[Number + 1];
            newTools[newTools.Length - 1] = tool;
            tools.CopyTo(newTools, 0);
            tools = newTools;
        }

        // Remove a tool from the collection
        public void delete(iTool tool)
        {
            var newTools = new iTool[Number - 1];
            int insertionIndex = 0;
            foreach (var currentT in tools)
            {
                if (currentT != tool)
                {
                    newTools[insertionIndex] = currentT;
                    insertionIndex++;
                }
            }
            tools = newTools;
        }

        // Find if a specific tool is in the collection
        public bool search(iTool tool)
        {
            foreach (var currentT in tools) {
                if (currentT == tool) return true;
            }
            return false;
        }

        // Return the tools array
        public iTool[] toArray() => tools;
    }
}