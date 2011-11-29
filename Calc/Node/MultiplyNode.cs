using System;

namespace Calc.Node
{
	//掛け算ノード。
	//詳しくはNode/PlusNodeを参照
	public class MultiplyNode:BaseNode
	{
		private BaseNode Left, Right;

		public MultiplyNode (BaseNode left, BaseNode right)
		{
			Left = left;
			Right = right;
		}

		public override BaseNode eval ()
		{
			NumberNode EvaledLeft = (NumberNode)Left.eval ();
			NumberNode EvaledRight = (NumberNode)Right.eval ();
			return new NumberNode (EvaledLeft.Value * EvaledRight.Value);
		}

		public override void print (int depth)
		{
			Console.WriteLine ("[MultiplyNode]");
			Console.Write ("|" + (new String ('|', depth)) + "|-");
			Left.print (depth + 1);
			Console.Write ("|" + (new String ('|', depth)) + "|-");
			Right.print (depth + 1);
		}
	}
}

