using System;
using Calc.Node;

namespace Calc
{
	/*
	 * パーサクラス
	 * 外から使うときは、Parse(Lexer lex)を呼び出すと構文木をくれる。
	 * 再帰下降パーサを実装してある。
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

		private BaseNode ParseExpression ()
		{
			return ParseAdditiveExpression ();
		}

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

		private BaseNode ParseUnaryExpression ()
		{
			BaseNode node = null;
			switch (tokenType) {
			case TokenType.Plus:
			case TokenType.Minus:
				node = ParseUnaryExpression2 ();
				break;
			default:
				node = ParseFirst ();
				break;
			}
			return node;
		}

		private BaseNode ParseUnaryExpression2 ()
		{
			TokenType type = tokenType;
			GetToken ();
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

		private BaseNode ParseFirst ()
		{
			BaseNode node = null;
			switch (tokenType) {
			case TokenType.Number:
				node = new NumberNode ((float)lex.Value);
				GetToken ();
				break;
			case TokenType.OpenParen:
				GetToken ();
				node = ParseParentheses ();
				break;
			default:
				break;
			}
			return node;
		}

		private BaseNode ParseParentheses ()
		{
			BaseNode node = ParseExpression ();
			if (tokenType != TokenType.CloseParen) {
				throw new Exception ("Not Close Paren found");
			}
			GetToken ();
			return node;
		}
	}
}

