using System;
using Calc.Node;

namespace Calc
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Parser parser = new Parser ();
			while (true) {
				Console.Write ("Calc > ");
				string input = Console.ReadLine ();
				Lexer lex = new Lexer (input);
				try {
					BaseNode parsed = parser.Parse (lex);
					if (parsed != null) {
						parsed.print (0);
						BaseNode result = parsed.eval ();
						Console.WriteLine (string.Format (" => {0}", result));
					}
				} catch (Exception e) {
					Console.WriteLine (e);
				}
			}
		}
	}
}
