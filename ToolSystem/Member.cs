using System.Linq;
namespace Assignment {
    class Member : iMember
    {
        string firstName;
        string lastName;
        string number;
        string pin;
        ToolCollection borrowed;
        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public string ContactNumber { get => number; set => number = value; }
        public string PIN { get => pin; set => pin = value; }

        public string[] Tools => borrowed.toArray().Select(tool => tool.Name).ToArray();

        public void addTool(iTool tool)
        {
            borrowed.add(tool);
        }

        public void deleteTool(iTool tool)
        {
            borrowed.delete(tool);
        }
    }
}