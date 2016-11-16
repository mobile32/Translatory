using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Translatory
{
    class Program
    {
        static void Main(string[] args)
        {
            var line = Console.ReadLine();

            var defs = new[]
            {
                new TokenDefinition(@"\d*\.\d+([eE][-+]?\d+)?", "FLOAT"),
                new TokenDefinition(@"\d+", "INT"),
                new TokenDefinition(@"[\*\/\-\+]", "OPERATOR"),
                new TokenDefinition(@"\(", "LEFT"),
                new TokenDefinition(@"\)", "RIGHT"),
                new TokenDefinition(@"\s", "SPACE"),
                new TokenDefinition(@"\t", "TAB"),
                new TokenDefinition(@"\n", "NEW LINE")
            };

            TextReader stringReader = new StringReader(line);
            Lexer lexer = new Lexer(stringReader, defs);
            List<string> tokens = new List<string>();

            while (lexer.Next())
            {
                Console.WriteLine("Token: {0} Contents: {1}", lexer.Token, lexer.TokenContents);

                if (lexer.Token.ToString() == "SPACE" || lexer.Token.ToString() == "TAB" ||
                    lexer.Token.ToString() == "NEW LINE")
                    continue;

                tokens.Add(lexer.Token.ToString());
            }

            Console.WriteLine("Lekser skończył pracę");

            Parser parser = new Parser();

            if (tokens.Count > 0)
            {
                parser.Parse(tokens);
                Console.WriteLine("Parsowanie zakończone sukcesem");
            }
        }
    }
}