using System;

namespace Calc
{
	//トークンの種類の列挙型。
	public enum TokenType
	{
		// + 足し算
		Plus,
		// - 引き算
		Minus,
		// * 掛け算
		Asterisk,
		// / 割り算
		Slash,
		// 開き括弧 '('
		OpenParen,
		// 閉じ括弧 ')'
		CloseParen,
		// 数値
		Number,
		// 終端記号
		EOF,
	}
}

