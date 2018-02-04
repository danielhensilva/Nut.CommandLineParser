using System;
using Nut.CommandLineParser.Attributes;

namespace Nut.CommandLineParser.Docs
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Nut.CommandLineParser sample application");

            Console.WriteLine();
            Console.WriteLine("# Example1");
            Example1();

            Console.WriteLine();
            Console.WriteLine("# Example2");
            Example2();

            Console.WriteLine();
            Console.WriteLine("# Example3");
            Example3();

            Console.WriteLine();
            Console.WriteLine("Exit...");
            Console.ReadKey();
        }

        static void Example1()
        {
            var args = "primary=blue secondary=\"purple and red\" --optional yes";
            var output = new Parser().ParseToKeyValuePairs(args);

            foreach (var item in output)
                Console.WriteLine(item.Key + " -> " + item.Value);
        }

        static void Example2()
        {
            var args = "first=Alice last=Souza --fullname \"Alice Souza\" -a \"AS\"";
            var output = new Parser().ParseToKeyValuePairs(args);

            foreach (var item in output)
                Console.WriteLine(item.Key + " -> " + item.Value);
        }

        static void Example3()
        {
            var args = "-i 19 -n \"A duck story\" -q 4 -v 14.99";
            var output = new Parser().ParseToObject<MyBook>(args);

            Console.WriteLine("BookId -> {0}", output.BookId);
            Console.WriteLine("BookName -> {0}", output.BookName);
            Console.WriteLine("StockQtd -> {0}", output.StockQtd);
            Console.WriteLine("StockPrice -> {0}", output.StockPrice);
        }
    }

    class MyBook
    {
        [OptionAlias('i')]
        [OptionAlias('k')]
        [OptionName("id")]
        public int BookId { get; set; }
        
        [OptionAlias('n')]
        [OptionName("name")]
        public string BookName { get; set; }
        
        [OptionAlias('q')]
        [OptionName("qtd")]
        public long StockQtd { get; set; }

        [OptionAlias('v')]
        [OptionName("value")]
        [OptionName("pricing")]
        public decimal StockPrice { get; set; }
    }
}
