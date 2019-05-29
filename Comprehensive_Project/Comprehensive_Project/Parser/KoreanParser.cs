using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moda.Korean.TwitterKoreanProcessorCS;
namespace Comprehensive_Project.Parser
{
    class KoreanParser
    {
        private String result;
        private static String[] resultArray;

        public KoreanParser(String input)
        {
            this.result = this.Tokenize(input);
            KoreanParser.resultArray = this.StringSplit(result);
            for (int i = 0; i < resultArray.Length; i++)
            {
                Console.WriteLine(resultArray[i]);
            }
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
        public  String[] StringSplit(String input)
        {
            String[] splitResult = input.Split(new char[] { ' ' });

            return splitResult;
        }

        public static String[] GetResult()
        {
            return resultArray;
        }
    }
}

