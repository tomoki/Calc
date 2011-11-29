using System;

namespace Calc.Node
{
	public class NumberNode:BaseNode
	{
		//実際の値
		private float val;

		//実際の値にアクセスするようのプロパティ。
		//ReadOnly
		public float Value {
			get {
				return val;
			}
		}
		//floatの値を渡して生成
		public NumberNode (float numbervalue)
		{
			val = numbervalue;
		}
		//再帰的なevalの最終地点。
		//ここではじめてevalが終わる。
		public override BaseNode eval ()
		{
			return this;
		}

		public override string ToString ()
		{
			return string.Format ("[NumberNode: {0}]", Value);
		}

		public override void print (int depth)
		{
			Console.WriteLine ("[NumberNode: {0}]", Value);
		}
	}
}

