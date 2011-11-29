using System;
using Calc.Node;

namespace Calc
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			//インタプリタを作る。
			Parser parser = new Parser ();
			while (true) {
				Console.Write ("Calc > ");
				string input = Console.ReadLine ();
				Lexer lex = new Lexer (input);
				try {
					BaseNode parsed = parser.Parse (lex);
					if (parsed != null) {
						//構文木を出力
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
