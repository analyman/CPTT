#region using statement
using System;
using System.Linq;
using System.Collections.Generic;
#endregion

namespace LdyCompiler
{
    public class Token
    {
        ///<summary>
        /// Token class Initialization.
        ///</summary>
        static Token(){get_valid_char();}

        private static char[] valid_char = new char[256];
        private static bool valid_char_getted = false;
        private static char[][] valid_char_range = new char[][]
        {
            new char[]{'!', '~'}
        };
        private static char[] blank_char = new char[] {'\t', ' ', '\n', '\r'};
        static char[] invalid_char = new char[256];
        private static char[] get_valid_char(){
            if(valid_char_getted)
                return valid_char;
            valid_char_getted = true;
            int next_ref_count = 0;
            foreach(char[] char_range in valid_char_range){
                Int16 begin_char = System.Convert.ToInt16(char_range[0]), 
                      end_char   = System.Convert.ToInt16(char_range[1]);
                for(;begin_char<=end_char;begin_char++)
                    valid_char[next_ref_count++] = System.Convert.ToChar(begin_char);
            }
            return valid_char;
        }
        private static string base64Encode(string str)
        {
            byte[] data = System.Text.Encoding.UTF8.GetBytes(str);
            return System.Convert.ToBase64String(data);
        }
        private static string base64Decoce(string str)
        {
            byte[] data = System.Convert.FromBase64String(str);
            return System.Text.Encoding.UTF8.GetString(data);
        }

        public static List<Token> GetSpaceToken(string str){
            List<Token> ret = new List<Token>();
            int string_length = str.Length;
            int left_index = 0, right_index = -1;
            int last_index = str.LastIndexOfAny(valid_char);
            if(last_index == -1){
                throw new ArgumentException("The string is invalid in GetSpaceToken()");
            }
            for(;;){
                left_index = str.IndexOfAny(valid_char, right_index + 1);
                right_index = str.IndexOfAny(blank_char, left_index);
                if (right_index == -1)
                {
                    ret.Add(new Token(str.Substring(left_index, last_index - left_index + 1)));
                    return ret;
                }
                if (left_index == -1){
                    return ret;
                }
                ret.Add(new Token(str.Substring(left_index, right_index - left_index)));
            }
        }

        private string m_token;
        public string Val
        {
            get
            {
                return m_token;
            }
        }
        public override string ToString()
        {
            return Val;
        }
        public Token(string str)
        {
            m_token = str;
        }

        public override bool Equals(object obj)
        {
            Token oth = obj as Token;
            if (oth == null || GetType() != oth.GetType())
            {
                return false;
            }
            return m_token.Equals(oth.m_token);
        }
        public bool Equals(Token obj){
            if(obj == null){
                return false;
            }
            return m_token.Equals(obj.m_token);
        }
        
        public override int GetHashCode()
        {
            return m_token.GetHashCode() ^ 0x051501113;
        }
    }
}