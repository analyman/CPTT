#region Using statements
using System;

using System.Collections.Generic;
#endregion

namespace LdyCompiler
{
    public class parser2_4_1_a
    {
        static Token t_plus = new Token("+"),
                    t_minus = new Token("-"),
                    t_alpha_a = new Token("a");

        private List<Token> m_tokens = null;
        public List<Token> Tokens { get { return m_tokens; } set { this.m_tokens = value; this.m_isValidParseTree = false;} }

        private int lookahead_index = 0;
        private ParserTree production_S()
        {
            ParserTree firstOp, secondOp, Op, retParseTree;
            ParserTree[] childNodes;
            List<Token> this_tokens;
            switch (m_tokens[lookahead_index++].Val){
                case "+":
                    Op = new ParserTree(new List<Token>(){t_plus});
                    firstOp = production_S();
                    secondOp = production_S();
                    childNodes = new ParserTree[3]{Op, firstOp, secondOp};
                    this_tokens = new List<Token>();
                    this_tokens.AddRange(Op.ParsingTokens);
                    this_tokens.AddRange(firstOp.ParsingTokens);
                    this_tokens.AddRange(secondOp.ParsingTokens);
                    retParseTree = new ParserTree(childNodes, this_tokens);
                    return retParseTree;
                case "-":
                    Op = new ParserTree(new List<Token>(){t_minus});
                    firstOp = production_S();
                    secondOp = production_S();
                    childNodes = new ParserTree[3]{Op, firstOp, secondOp};
                    this_tokens = new List<Token>();
                    this_tokens.AddRange(Op.ParsingTokens);
                    this_tokens.AddRange(firstOp.ParsingTokens);
                    this_tokens.AddRange(secondOp.ParsingTokens);
                    retParseTree = new ParserTree(childNodes, this_tokens);
                    return retParseTree;
                case "a":
                    Op = new ParserTree(new List<Token>(){t_alpha_a});
                    return Op;
                default:
                    throw new ArgumentException("Error Token <" + this.m_tokens[lookahead_index - 1].ToString() + ">");
            }
        }

        private ParserTree m_parsetree = null;
        public ParserTree ParseTree
        {
            get
            {
                if(this.m_tokens == null){
                    throw new ArgumentException("Parsing Empty token..."); 
                }
                if (IsValidParseTree)
                    return m_parsetree;
                this.lookahead_index = 0;
                m_parsetree = this.production_S();
                if (lookahead_index != m_tokens.Count)
                    throw new ArgumentException("Error Token <" + this.m_tokens[lookahead_index].ToString() + ">");
                this.m_isValidParseTree = true;
                return m_parsetree;
            }
        }

        private bool m_isValidParseTree = false;
        public bool IsValidParseTree{get { return this.m_isValidParseTree; } }

        public parser2_4_1_a(List<Token> v_tokens)
        {
            this.m_tokens = v_tokens;
        }
        public parser2_4_1_a(string parsingStr){
            this.m_tokens = Token.GetSpaceToken(parsingStr);
        }
        public parser2_4_1_a() {}
    }
}