using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PheonixTechTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Enter the input string:");
            var s = Console.ReadLine();

            Console.WriteLine("Enter Array of words of same length seperated by a space");
            var searchWords = Console.ReadLine();

            var words = searchWords.Split(' ');
            words = words.Where(x => !string.IsNullOrEmpty(x)).ToArray();
            int wordLength = 0;
            foreach (var word in words)
            {
                if (wordLength == 0)
                    wordLength = word.Length;
                else
                {
                    if (wordLength != word.Length)
                    {
                        Console.WriteLine("Invalid search Words array. Please enter words of same length seperated by space");
                        break;
                    }
                }
            }


            var result = SearchWordsInString(s, words);
            Console.WriteLine("The output indexes are:");
            if (result.Count == 0)
                Console.WriteLine("[]");
            else
            {
                Console.Write("[");
                result.ToList().ForEach(x => Console.Write(x + " "));
                Console.Write("]");
            }
            Console.ReadLine();
        }

        private static IList<int> SearchWordsInString(string s, string[] words)
        {
            var result = new List<int>();
            if (string.IsNullOrEmpty(s) || words == null || words.Length == 0)
                return result;

            var dict = new Dictionary<string, int>();
            foreach (var word in words)
            {
                if (!dict.ContainsKey(word))
                {
                    dict[word] = 0;
                }
                dict[word]++;
            }

            int wordLength = words[0].Length;
            for (int i = 0; i <= s.Length - words.Length * wordLength; i++)
            {
                var notFound = new Dictionary<string, int>(dict);
                for (int j = i; j < i + wordLength * words.Length; j = j + wordLength)
                {
                    var nextWord = s.Substring(j, wordLength);
                    if (!notFound.ContainsKey(nextWord))
                    {
                        break;
                    }
                    else if (--notFound[nextWord] == 0)
                    {
                        notFound.Remove(nextWord);
                    }
                }

                if (notFound.Count == 0)
                    result.Add(i);

            }

            return result;

        }
    }


}
