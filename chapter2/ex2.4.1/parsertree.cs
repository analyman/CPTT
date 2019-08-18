#region using statements
#endregion

using System;
using System.Collections.Generic;
using System.Linq;

namespace LdyCompiler
{
    public class ParserTree
    {
        private int m_childCount;
        public int ChildCount{get{return m_childCount;}}
        private List<Token> m_parsingTokens;
        public List<Token> ParsingTokens {get{return m_parsingTokens;}}
        private List<ParserTree> m_children;
        public ParserTree(ParserTree[] v_children, List<Token> parsingTokens){
            m_childCount = v_children.Length;
            m_children = new List<ParserTree>();
            Enumerable.Select<ParserTree, bool>(v_children, (ParserTree treeNode) => {m_children.Add(treeNode);return true;});
            m_parsingTokens = parsingTokens;
        }

        public ParserTree(List<Token> parsingTokens){
            m_childCount = 0;
            m_children = null;
            m_parsingTokens = parsingTokens;
        }
    }
}