using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moda.Korean.TwitterKoreanProcessorCS;
namespace preprocessor
{
    class Parser
    {
        //extractPhraseResult;
        private String result;

        public Parser(String input)
        {
            this.result = this.Tokenize(input);
        }
        public String Tokenize(String input)
        {
            StringBuilder TokenizeResult = new StringBuilder();

            var tokens = TwitterKoreanProcessorCS.Tokenize(input);

            foreach (var token in tokens)
            {
                if ((token.Pos.ToString()).Equals("Noun"))
                {
                    TokenizeResult.Append(token.Text + " ");
                }
            }
            return TokenizeResult.ToString();
        }

        public String getResult()
        {
            return result;
        }
    }
}

