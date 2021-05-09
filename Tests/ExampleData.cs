using Assignment;

namespace ExampleData
{
    static class ExampleUsers
    {
        public static Member Liran => new Member("Liran", "Piade", "04", "1111");
        public static Member James => new Member("James", "Doe", "045", "1111");
        public static Member Dan => new Member("Dan", "Danny", "0123", "1111");
        public static Member Rachel => new Member("Rachel", "Doe", "0133", "1111");
        public static Member Jane => new Member("Jane", "Person", "0163", "1111");
        public static Member Jack => new Member("Jack", "Smith", "0513", "1111");
    }

    static class ExampleTools
    {
        public static Tool Chisel => new Tool("Chisel", 2);
        public static Tool Scissors => new Tool("Scissors", 1);
        public static Tool Crayon => new Tool("Crayon", 4);
        public static Tool Rubber => new Tool("Rubber", 4);
        public static Tool Wire => new Tool("Wire", 2);
    }
}