using System;

namespace Calc.Node
{
	/*
	 * 足し算ノード
	 * MinusNode,MultiplyNode,DivideNodeは
	 * 結果表示メソッド、+か-かくらいの違いしかない。
	 */
	public class PlusNode:BaseNode
	{
		//左の項と右の項を保存。
		private BaseNode Left, Right;
		
		//左の項と右の項を与えて生成
		public PlusNode (BaseNode left, BaseNode right)
		{
			Left = left;
			Right = right;
		}
		/*
		 * 超重要なメソッド
		 * すべてのノードはevalメソッドを持っていて、
		 * 一番上のノードのevalから一番下のevalまで再帰的に呼び出す。
		 * 最後にこのノードの計算結果を渡す
		 */
		public override BaseNode eval ()
		{
			NumberNode EvaledLeft = (NumberNode)Left.eval ();
			NumberNode EvaledRight = (NumberNode)Right.eval ();
			return new NumberNode (EvaledLeft.Value + EvaledRight.Value);
		}

		public override void print (int depth)
		{
			Console.WriteLine ("[PlusNode]");
			Console.Write ("|" + (new String ('|', depth)) + "|-");
			Left.print (depth + 1);
			Console.Write ("|" + (new String ('|', depth)) + "|-");
			Right.print (depth + 1);
		}
	}
}

