using System;

namespace LdyCompiler
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            parser2_4_1_a parser = new parser2_4_1_a("+ - + a a a + a a");
            Console.WriteLine(parser.ParseTree.ParsingTokens[0].ToString());
            Console.WriteLine("hello");
            Console.ReadKey();
        }
    }
}