using System;

namespace Calc.Node
{
	// プラスが頭についた項のノード
	// Node/NegativeNode.csを参照
	public class PositiveNode:BaseNode
	{
		private BaseNode Right;

		public PositiveNode (BaseNode right)
		{
			Right = right;
		}

		public override BaseNode eval ()
		{
			NumberNode EvaledRight = (NumberNode)Right.eval ();
			return new NumberNode (1 * EvaledRight.Value);
		}

		public override void print (int depth)
		{
			Console.WriteLine ("[NegativeNode]");
			Console.Write ("|" + (new String ('|', depth)) + "|-");
			Right.print (depth + 1);
		}
	}
}

