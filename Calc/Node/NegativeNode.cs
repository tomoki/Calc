using System;

namespace Calc.Node
{
	//マイナスが頭についた項のノード
	public class NegativeNode:BaseNode
	{
		private BaseNode Right;
		//単項演算子なので、一つしか項を取らない。
		public NegativeNode (BaseNode right)
		{
			Right = right;
		}

		public override BaseNode eval ()
		{
			//項を評価して、結果をEvaledRightに入れる。
			//ここでは結果はNumberNodeと決め打ちしてるが、
			//これは電卓だからOKであって、プログラミング言語の場合そうはいかない。
			NumberNode EvaledRight = (NumberNode)Right.eval ();
			//評価した値に-1をかけた数をもつNumberNodeを生成。
			return new NumberNode (-1 * EvaledRight.Value);
		}

		public override void print (int depth)
		{
			Console.WriteLine ("[NegativeNode]");
			Console.Write ("|" + (new String ('|', depth)) + "|-");
			Right.print (depth + 1);
		}
	}
}

