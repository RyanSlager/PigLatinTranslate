using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PigLatinTranslate
{
    class Program
    {
        static void Main(string[] args)
        {
            string sentence = GetInput("Ryan");

            string translate = PigLatinize(sentence);

            Console.WriteLine(translate);
        }

        public static string GetInput(string name)
        {

            Console.WriteLine($"{name}, please enter a sentence that you'd like translated:\n");

            return Console.ReadLine();

        }

        public static string PigLatinize(string sentence)
        {
            string s = sentence;
            char[] format = new char[] { ' ', ',', ':', ';' };
            char[] vowels = new char[] { 'a', 'e', 'i', 'o', 'u' };

            string[] words = s.Split(format);
            List<string> newWordsTemp = new List<string>();

            foreach(string word in words)
            {
                int hasVowel = word.IndexOfAny(vowels);
                int wordLeng = word.Length;
                string newWord;
                
                if(hasVowel != -1 && hasVowel != 0)
                {
                    string sub = word.Substring(0, hasVowel);
                    string tempWord = word.Remove(0, hasVowel);
                    newWord = String.Concat(tempWord, sub, "ay");
                }
                else if(hasVowel == 0 && wordLeng != 1)
                {
                    newWord = String.Concat(word, "way");
                }
                else
                {
                    newWord = word;
                }

                newWordsTemp.Add(newWord);
            }

            string translatedSentence = string.Join(" ", newWordsTemp);

            return translatedSentence;
        }

        //public static string Encrypt(string sentence)
        //{
        //    string s = sentence;

        //}

        //public static bool Resume(bool cont)
        //{
        //    bool c = cont;
        //}
    }
}
