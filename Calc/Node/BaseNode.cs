using System;

namespace Calc.Node
{
	/*
	 * すべてのノードの既定クラス。
	 * すべてのオブジェクトが持つべき要素はここで定義する。
	 */
	public abstract class BaseNode
	{
		public BaseNode ()
		{
		}

		public virtual BaseNode eval ()
		{
			return this;
		}
		//構文木表示用メソッド。
		public virtual void print(int depth){
			Console.WriteLine ("[Base]");
		}
	}
}

