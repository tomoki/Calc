using System;

namespace Calc.Node
{
	//割り算ノード。
	//詳しくはNode/PlusNodeを参照
	public class DivideNode:BaseNode
	{
		private BaseNode Left, Right;

		public DivideNode (BaseNode left, BaseNode right)
		{
			Left = left;
			Right = right;
		}

		public override BaseNode eval ()
		{
			NumberNode EvaledLeft = (NumberNode)Left.eval ();
			NumberNode EvaledRight = (NumberNode)Right.eval ();
			return new NumberNode (EvaledRight.Value / EvaledLeft.Value);
		}
		
		public override void print(int depth){
			Console.WriteLine ("[DivideNode]");
			Console.Write ("|"+(new String('|',depth))+"|-");
			Left.print (depth+1);
			Console.Write ("|"+(new String('|',depth))+"|-");
			Right.print (depth+1);
		}
	}
}

