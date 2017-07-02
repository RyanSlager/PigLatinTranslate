using System;
using System.Collections.Generic;

namespace PigLatinTranslate
{
    class Program
    {
        static void Main()
        {
            string name = GetName();
            bool cont = DrawMenu(name);

            while(!cont == false)
            {
                cont = DrawMenu(name);
            }
        }

        public static string GetName()
        {
            Console.WriteLine("Hello, welcome to our English to Pig Latin Translater and String Encryption Application!\n" +
                "Please enter your name: \n");
            return Console.ReadLine();
        }
        public static bool DrawMenu(string name)
        {

            Console.WriteLine($"{name}, please choose an option:\n1) Translate English to Pig Latin\n2) Encrypt String\n3) Decrypt String" +
                $"\n4) Quit");
            string choice = Console.ReadLine();
            int choiceInt;

            bool cont = true;

            while(!Int32.TryParse(choice, out choiceInt) && choiceInt != 1 && choiceInt != 2 && choiceInt != 3)
            {
                Console.WriteLine("Sorry, that is not a valid choice.\n");
                Console.WriteLine($"{name}, please choose an option:\n1) Translate English to Pig Latin\n2) Encrypt String\n3) 3) Decrypt String" +
                    $"\n 4) Quit");
                choice = Console.ReadLine();
            }

            if (choiceInt == 1)
            {
                PigLatinize();
            }
            else if (choiceInt == 2)
            {
                Encrypt();
            }
            else if (choiceInt == 3)
            {
                Decrypt();
            }
            else if (choiceInt == 4)
            {
                cont = false;
            }

            return cont;

        }

        public static string PigLatinize()
        {
            Console.WriteLine("Please enter an English sentence that will be translated into pig latin.\n");
            string s = Console.ReadLine();

            char[] punctuation = new char[] { ' ', ',', ':', ';', '?', '\'', '.', '/', '(', ')'};
            char[] vowels = new char[] { 'a', 'e', 'i', 'o', 'u' };

            string[] words = s.Split(' ');
            List<string> newWordsTemp = new List<string>();

            foreach(string word in words)
            {
                int hasVowel = word.IndexOfAny(vowels);
                int wordLeng = word.Length;
                string newWord;
                
                if(hasVowel != -1 && hasVowel != 0)
                {
                    int indexPunc = word.IndexOfAny(punctuation);
                    string sub = word.Substring(0, hasVowel);
                    string tempWord = word.Remove(0, hasVowel);
                    int subChar1 = (int)sub[0];

                    if(subChar1 >= 65 && subChar1 <= 90)
                    {
                        newWord = HandleCaps(word);
                    }
                    else
                    {
                        newWord = String.Concat(tempWord, sub, "ay");
                    }
                 
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

            string cont = "";
            Console.WriteLine("Would you like to Encrypt your sentence?");
            cont = Console.ReadLine();

            while (cont != "y" && cont != "Y" && cont != "n" && cont != "N")
            {
                Console.WriteLine("Please enter either y to encrypt, or n to exit to menu");
                cont = Console.ReadLine();
            }
            if (cont == "y" || cont == "Y")
            {
                Console.WriteLine("Your Pig Latin string is:\n");
                Console.WriteLine(translatedSentence + "\n");

                string encrypted = Encrypt(translatedSentence);
                Console.WriteLine("Your Encrypted Pig Latin String is:\n");
                Console.WriteLine(encrypted + "\n");

                return encrypted;

            }
            else
            {
                Console.WriteLine("Your Pig Latin string is:\n");
                Console.WriteLine(translatedSentence + "\n");
                return translatedSentence;
            }
        }

        public static string HandleCaps(string word)
        {
            char[] punctuation = new char[] { ' ', ',', ':', ';', '?', '\'', '.', '/', '(', ')' };
            char[] vowels = new char[] { 'a', 'e', 'i', 'o', 'u' };
            int hasVowel = word.IndexOfAny(vowels);
            int wordLeng = word.Length;
            string newWord;
            int indexPunc = word.IndexOfAny(punctuation);
            string sub = word.Substring(0, hasVowel);
            string tempWord = word.Remove(0, hasVowel);
            int subChar1 = (int)sub[0];

            if (subChar1 >= 65 && subChar1 <= 90)
            {
                Console.WriteLine("UPPER");
                char tempFirst = char.ToUpper(tempWord[0]);
                tempWord = tempWord.Remove(0, 1);
                tempWord = String.Concat(tempFirst, tempWord);

                char subFirst = char.ToLower(sub[0]);
                sub = sub.Remove(0, 1);
                sub = String.Concat(subFirst, sub);
                Console.WriteLine(sub);
            }

            newWord = String.Concat(tempWord, sub, "ay");

            return newWord;
        }

        public static string Encrypt(string sentence = "")
        {
            if(sentence == "")
            {
                Console.WriteLine("Please enter a string you wish to encrypt\n");
                sentence = Console.ReadLine();
                Console.WriteLine();
            }

            char[] chars = sentence.ToCharArray();
            char[] spunChar = new char[chars.Length];

            Console.WriteLine("Enter how many places you'd like to rotate the sentence by:\n");
            string strKey = Console.ReadLine();
            int key;

            Console.WriteLine();

            while (!Int32.TryParse(strKey, out key))
            {
                Console.WriteLine("Please enter an integer:\n");
                strKey = Console.ReadLine();
            }

            int charLeng = chars.Length;

            for (int i = 0; i < charLeng; i++)
            {
                int tempKey = key;
                int charKey = (int)chars[i];

                if (Char.IsLetter(chars[i]))
                {
                    int ascii = (int)chars[i] + key;
                    //Console.WriteLine(ascii);

                    if ((char)charKey == ' ')
                    {
                        spunChar[i] = ' ';
                    }
                    else if (ascii > 122)
                    {
                        while (ascii > 122)
                        {
                            tempKey -= 26;
                            ascii = charKey + tempKey;
                        }
                    }
                    else if (!(ascii < 90) && (ascii > 97))
                    {
                        while(ascii > 90)
                        {
                            tempKey -= 26;
                            ascii = charKey + tempKey;
                        }
                    }

                    spunChar[i] = (char)ascii;
                }
            }

            char[] encryptedChars = new char[spunChar.Length];

            for(int i = 0; i < spunChar.Length; i++)
            {
                encryptedChars[i] = spunChar[i];
            }
            
            string encryptedString = new string(encryptedChars);

            Console.WriteLine("Your encrypted string is:\n");
            Console.WriteLine(encryptedString + "\n");

            return encryptedString;
        }

        public static string Decrypt(string sentence = "")
        {
            if (sentence == "")
            {
                Console.WriteLine("Please enter a string you wish to decrypt\n");
                sentence = Console.ReadLine();
                Console.WriteLine();
            }

            char[] chars = sentence.ToCharArray();
            char[] spunChar = new char[chars.Length];

            Console.WriteLine("Enter the key that was used to encrypt your string:\n");
            string strKey = Console.ReadLine();
            int key;

            Console.WriteLine();

            while (!Int32.TryParse(strKey, out key))
            {
                Console.WriteLine("Please enter an integer:\n");
                strKey = Console.ReadLine();
            }

            int charLeng = chars.Length;

            for (int i = 0; i < charLeng; i++)
            {
                int tempKey = key;
                int charKey = (int)chars[i];

                if (Char.IsLetter(chars[i]))
                {
                    int ascii = (int)chars[i] - key;
                    //Console.WriteLine(ascii);

                    if ((char)charKey == ' ')
                    {
                        spunChar[i] = ' ';
                    }
                    else if (charKey > 97 && charKey < 122)
                    {
                        while (ascii < 97)
                        {
                            tempKey -= 26;
                            ascii = charKey - tempKey;
                        }
                    }
                    else if (charKey > 65 && charKey < 90)
                    {
                        while (ascii < 65)
                        {
                            tempKey -= 26;
                            ascii = charKey - tempKey;
                        }
                    }

                    spunChar[i] = (char)ascii;
                }
            }

            char[] encryptedChars = new char[spunChar.Length];

            for (int i = 0; i < spunChar.Length; i++)
            {
                encryptedChars[i] = spunChar[i];
            }

            string encryptedString = new string(encryptedChars);

            Console.WriteLine("Your encrypted string is:\n");
            Console.WriteLine(encryptedString + "\n");

            return encryptedString;



        }

    }
}
