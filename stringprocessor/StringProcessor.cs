using System;
using System.Collections;
using System.Text.RegularExpressions;
using System.Linq;

namespace Chaiwatmat.StringProcessor
{
    public class StringProcessor
    {
        public int Add(string input)
        {
            var splitter = new string[] { ",", "\n" };

            if(string.IsNullOrEmpty(input)){
                return 0;
            }

            if(input.StartsWith("//")){
                var texts = input.Split('\n');
                var delimeters = texts[0].Substring(2);

                if(delimeters.Contains("][")){
                    var delimeterSplitter = new string[] { "[", "]" };
                    splitter = delimeters.Split(delimeterSplitter, StringSplitOptions.RemoveEmptyEntries);
                }else{
                    splitter = new string[] { delimeters };
                }

                input = texts[1];
            }else{
                splitter = new string[] { ",", "\n" };
            }

            var numbers = input.Split(splitter, StringSplitOptions.RemoveEmptyEntries).Select(i => int.Parse(i));
            var negativeNumbers = numbers.Where(n => n < 0).Select(n => n.ToString());

            if(negativeNumbers.Any()){
                var errorMessage = string.Join(",", negativeNumbers);
                throw new Exception(errorMessage);
            }

            numbers = numbers.Where(n => n <= 1000);

            return numbers.Sum();
        }
    }
}
