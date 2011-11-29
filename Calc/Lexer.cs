using System;
using System.Text;
using Calc.IO;

namespace Calc
{
	/*
     * Lexical Analyzerクラス
     * 文字列を読み込んで、それをトークンに分解する。
     * トークンの種類はTokenType.csを参照。
     * 外から使う場合、Advance()とValue,Tokenを使用する。
     * Valueはトークンの持つ値で、Numberトークンでは数値。
     * Tokenはトークンの種類。
     */
	public class Lexer
	{
		//トークンの種類。
		//外部からアクセスする場合はToken(読み取り専用)
		private TokenType tokenType;

		public TokenType Token {
			get {
				return tokenType;
			}
		}
		//トークンの持つ値
		//外部からアクセスする場合はValue(読み取り専用)
		//外部で任意の型にキャストしてから使う。
		//ex. Numberトークンのときは(float)(lexer.Value)とか。
		private object tokenValue;

		public object Value {
			get {
				return tokenValue;
			}
		}

		private LexerReader reader;

		//コンストラクタ。
		//stringでコードを読み込む。
		public Lexer (string code)
		{
			reader = new LexerReader (code);
		}

		//外部から呼ばれるメソッド。
		//次のトークンを読みこんで
		//Token,Valueを設定する。
		//コードが終わっていたら(EOF)、false、次のを読めていたらtrue;
		public bool Advance ()
		{
			return LexToken ();
		}
		//Advanceから呼ばれる。
		//一つ、次のトークンを読み込んで、
		//TokenとValueの値を設定する。
		private bool LexToken ()
		{
			SkipWhiteSpace ();
			int c = reader.Peek ();
			//コードが終了していたら
			if (c == -1) {
				return false;
			}
			LexVisibles ();
			return true;
		}

		//空白の読み飛ばし
		private void SkipWhiteSpace ()
		{
			int c = reader.Peek ();
			while (c != -1 && c!= '\n' && char.IsWhiteSpace((char)c)) {
				reader.Read ();
				c = reader.Peek ();
			}
		}

		//電卓で使える数字は以下の形式。
		//0を含む任意の数からなる数字の列。
		//ex.123,0123
		//小数。ただし、ドットの後に何もない場合は許さない。
		//ex.0.123,01.123
		//TODO:もうちょっと綺麗にかけないかなぁ……。
		//IO.LexerReaderがある理由にここがある。
		//小数の読み込みには2つ先の文字を読まなければならない。
		private void LexNumber ()
		{
			int ch = reader.Peek ();
			//すでに.が来ているか。小数の判定に使う。
			bool alreadyDoted = false;
			StringBuilder sb = new StringBuilder ();
			while (ch!=-1) {
				if (char.IsDigit ((char)ch)) {
					sb.Append ((char)ch);
					reader.Read ();
					//'.'がはじめてきたとき、
				} else if ((char)ch == '.' && !alreadyDoted) {
					alreadyDoted = true;
					//'.'の次が数字ならば小数として処理する。
					if (reader.Peek (1) != -1 && char.IsDigit ((char)reader.Peek (1))) {
						// '.'を追加
						sb.Append ((char)ch);
						reader.Read ();
						// '.'の次の数値を追加
						sb.Append ((char)reader.Read ());
					}
				} else {
					break;
				}
				ch = reader.Peek ();
			}
			//Token,Valueを設定
			tokenType = TokenType.Number;
			tokenValue = float.Parse (sb.ToString ());
		}

		//Advanceから直接呼ばれる。
		private void LexVisibles ()
		{
			int c = reader.Peek ();
			//トークンの最初の文字で場合分けを行う。
			switch (c) {
			case '+':
				reader.Read ();
				tokenType = TokenType.Plus;
				break;
			case '-':
				reader.Read ();
				tokenType = TokenType.Minus;
				break;
			case '*':
				reader.Read ();
				tokenType = TokenType.Asterisk;
				break;
			case '/':
				reader.Read ();
				tokenType = TokenType.Slash;
				break;
			case '(':
				reader.Read ();
				tokenType = TokenType.OpenParen;
				break;
			case ')':
				reader.Read ();
				tokenType = TokenType.CloseParen;
				break;
			default:
				if (char.IsDigit ((char)c)) {
					LexNumber ();
				} else {
					throw new Exception (string.Format ("unknown char:{0}", (char)c));
				}
				break;
			}
		}
	}
}

