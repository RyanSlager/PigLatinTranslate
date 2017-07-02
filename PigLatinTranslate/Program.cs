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

        public static string Encrypt(string sentence = "")
        {
            if(sentence == "")
            {
                Console.WriteLine("Please enter a string you wish to encrypt");
                sentence = Console.ReadLine();
            }

            char[] chars = sentence.ToCharArray();
            List<char> spunChar = new List<char>();
            Console.WriteLine("Enter how many places you'd like to rotate the sentence by:\n");
            string tempKey = Console.ReadLine();
            int key;

            while (!Int32.TryParse(tempKey, out key))
            {
                Console.WriteLine("Please enter an integer:\n");
                tempKey = Console.ReadLine();
            }

            int charLeng = chars.Length;

            for (int i = 0; i < charLeng; i++)
            {
                if (Char.IsLetter(chars[i]))
                {
                    int ascii = (int)chars[i] + key;
                    //Console.WriteLine(ascii);

                    if (ascii > 122)
                    {
                        ascii -= 26;
                    }
                    else if (ascii < 65)
                    {
                        ascii += 26;
                    }
                    else if (ascii > 90 && ascii < 97)
                    {
                        ascii -= 26;
                    }
                    else if(ascii == 32)
                    {
                        spunChar.Add(' ');
                    }

                    spunChar.Add((char)ascii);
                }
            }


            string encryptedString = string.Join("", spunChar);

            Console.WriteLine("Your encrypted string is:\n");
            Console.WriteLine(encryptedString + "\n");

            return encryptedString;
        }

        public static string Decrypt(string sentence = "")
        {
            if (sentence == "")
            {
                Console.WriteLine("Please enter a string you wish to decrypt");
                sentence = Console.ReadLine();
            }

            char[] chars = sentence.ToCharArray();
            List<char> spunChar = new List<char>();
            Console.WriteLine("Enter the key that was used to encrypt your sentence\n");
            string tempKey = Console.ReadLine();
            int key;

            while (!Int32.TryParse(tempKey, out key))
            {
                Console.WriteLine("Please enter an integer:\n");
                tempKey = Console.ReadLine();
            }

            int charLeng = chars.Length;

            for (int i = 0; i < charLeng; i++)
            {
                if (Char.IsLetter(chars[i]))
                {
                    int ascii = (int)chars[i] - key;
                    //Console.WriteLine(ascii);

                    if (ascii > 122)
                    {
                        ascii -= 26;
                    }
                    else if (ascii < 65)
                    {
                        ascii += 26;
                    }
                    else if (ascii < 97)
                    {
                        ascii += 26;
                    }
                    else if (ascii == 32)
                    {
                        spunChar.Add(' ');
                    }

                    spunChar.Add((char)ascii);
                }
            }


            string encryptedString = string.Join("", spunChar);

            Console.WriteLine("Your encrypted string is:\n");
            Console.WriteLine(encryptedString + "\n");

            return encryptedString;
        }

    }
}
