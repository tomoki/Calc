using System;
using Calc.Node;

namespace Calc
{
	/*
	 * パーサクラス
	 * 外から使うときは、Parse(Lexer lex)を呼び出すと構文木をくれる。
	 * 再帰下降パーサを実装してある。
	 * これを理解するには、実際に数式を紙の上でパースしてみるといい。
	 */
	public class Parser
	{
		private Lexer lex;
		//トークンの種類をもっておく。
		private TokenType tokenType;

		public Parser ()
		{
		}
		/*
		 * 次のトークンを読み込む。
		 * 次のトークンが存在するなら、tokenTypeにそのトークンを、
		 * しないならばTokenType.EOFをセットする
		 */
		private void GetToken ()
		{
			if (lex.Advance ()) {
				tokenType = lex.Token;
			} else {
				tokenType = TokenType.EOF;
			}
		}
		/*
		 * 外から呼ばれるメソッド。
		 * Lexerを受け取ってそれを元に構文木を構築する。
		 */
		public BaseNode Parse (Lexer lexer)
		{
			lex = lexer;
			GetToken ();
			return ParseExpression ();
		}
		//式をパースする。
		private BaseNode ParseExpression ()
		{
			//一番上にくるノードから(評価が一番遅いのから)
			return ParseAdditiveExpression ();
		}

		//足し算引き算をパースする。
		//電卓の中では評価が一番後
		private BaseNode ParseAdditiveExpression ()
		{
			BaseNode node = ParseMultiplicativeExpression ();
			while ((tokenType == TokenType.Plus) ||
                  (tokenType == TokenType.Minus)) {
				TokenType op_type = tokenType;
				GetToken ();
				BaseNode right = ParseMultiplicativeExpression ();
				if (op_type == TokenType.Plus) {
					node = new PlusNode (node, right);
				} else {
					node = new MinusNode (node, right);
				}
			}
			return node;
		}

		//掛け算割り算をパースする。
		private BaseNode ParseMultiplicativeExpression ()
		{
			BaseNode node = ParseUnaryExpression ();
			while ((tokenType == TokenType.Asterisk) ||
                  (tokenType == TokenType.Slash)) {
				TokenType op_type = tokenType;
				GetToken ();
				BaseNode right = ParseUnaryExpression ();
				if (op_type == TokenType.Asterisk) {
					node = new MultiplyNode (right, node);
				} else {
					node = new DivideNode (right, node);
				}
			}
			return node;
		}
		//単項演算子の処理。
		//単項演算子は電卓の場合、具体的には-と+。
		private BaseNode ParseUnaryExpression ()
		{
			BaseNode node = null;
			//単項演算子かどうかで場合分けする
			switch (tokenType) {
			case TokenType.Plus:
			case TokenType.Minus:
				//単項演算子ならParseUnaryExpression2を呼ぶ
				node = ParseUnaryExpression2 ();
				break;
			default:
				//単項演算子がないなら、ParseFirstを呼ぶ
				node = ParseFirst ();
				break;
			}
			return node;
		}
		
		//一番上に単項演算子がある場合に呼ばれる。
		private BaseNode ParseUnaryExpression2 ()
		{
			/*
			 * トークンタイプを保存しておく。
			 * 次のトークンを呼んでしまうの前のを上書きしてしまうから。
			 */
			TokenType type = tokenType;
			GetToken ();
			//単項演算子の対象をパースする。
			BaseNode node = ParseUnaryExpression ();
			switch (type) {
			case TokenType.Plus:
				node = new PositiveNode (node);
				break;
			case TokenType.Minus:
				node = new NegativeNode (node);
				break;
			}
			return node;
		}

		//一番最初に評価される部分。
		//電卓の場合、ただの数値とか。
		private BaseNode ParseFirst ()
		{
			BaseNode node = null;
			
			switch (tokenType) {
			case TokenType.Number:
				node = new NumberNode ((float)lex.Value);
				GetToken ();
				break;
			//開き括弧
			case TokenType.OpenParen:
				GetToken ();
				//括弧の中を優先的に処理する。
				node = ParseParentheses ();
				break;
			default:
				break;
			}
			return node;
		}
		//括弧の中を先に処理する。
		private BaseNode ParseParentheses ()
		{
			//一番上のメソッドを呼ぶ。
			BaseNode node = ParseExpression ();
			//次は閉じ括弧になっているはず。
			if (tokenType != TokenType.CloseParen) {
				throw new Exception ("Not Close Paren found");
			}
			GetToken ();
			return node;
		}
	}
}

