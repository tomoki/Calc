using System;

namespace Calc.Node
{
	//引き算ノード。
	//詳しくはNode/PlusNodeを参照
	public class MinusNode:BaseNode
	{
		private BaseNode Left, Right;

		public MinusNode (BaseNode left, BaseNode right)
		{
			Left = left;
			Right = right;
		}

		public override BaseNode eval ()
		{
			NumberNode EvaledLeft = (NumberNode)Left.eval ();
			NumberNode EvaledRight = (NumberNode)Right.eval ();
			return new NumberNode (EvaledLeft.Value - EvaledRight.Value);
		}

		public override void print (int depth)
		{
			Console.WriteLine ("[MinusNode]");
			Console.Write ("|" + (new String ('|', depth)) + "|-");
			Left.print (depth + 1);
			Console.Write ("|" + (new String ('|', depth)) + "|-");
			Right.print (depth + 1);
		}
	}
}

