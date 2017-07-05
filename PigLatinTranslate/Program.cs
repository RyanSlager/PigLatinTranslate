using System;
using System.Collections.Generic;

namespace PigLatinTranslate
{
    class Program
    {
        static void Main()
        {
            // name is set to return value of GetName and is passed to DrawMenu
            string name = GetName();
            //DrawMenu prints the menu out, and returns false when the user chooses the quit option
            bool cont = DrawMenu(name);
            // menu is drawn until the user enters the quit option
            while(!cont == false)
            {
                cont = DrawMenu(name);
            }
        }

        // GetName gets a users name and returns it
        public static string GetName()
        {
            Console.WriteLine("Hello, welcome to our English to Pig Latin Translater and String Encryption Application!\n" +
                "Please enter your name: \n");
            return Console.ReadLine();
        }

        // DrawMenu draws the menu and returns a false bool when the users chooses the quit option
        public static bool DrawMenu(string name)
        {
            // menu is printed out and the choice is parsed to an int(input is validated)
            Console.WriteLine($"{name}, please choose an option:\n\n1) Translate English to Pig Latin\n2) Encrypt String\n3) Decrypt String" +
                $"\n4) Quit\n\n");
            string choice = Console.ReadLine();
            int choiceInt;
            bool cont = true;

            // input is validated
            while(!Int32.TryParse(choice, out choiceInt) && choiceInt != 1 && choiceInt != 2 && choiceInt != 3)
            {
                Console.WriteLine("Sorry, that is not a valid choice.\n");
                Console.WriteLine($"{name}, please choose an option:\n1) Translate English to Pig Latin\n2) Encrypt String\n3) Decrypt String" +
                    $"\n 4) Quit\n\n");
                choice = Console.ReadLine();
            }

            // if/else tree handles calls to methods

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


        // PigLatinize gets a string from the user and translates it into pig latin
        public static string PigLatinize()
        {
            // input is taken
            Console.WriteLine("Please enter an English sentence that will be translated into pig latin.\n");
            string s = Console.ReadLine();

            // arrays of chars are declared to be used in IndexOfAny. 
            char[] punctuation = new char[] { ',', ':', ';', '?', '!', '.' };
            char[] specChar = new char[] { '@', '#', '$', '%', '^', '&', '*', '(', ')', '/', '[', ']' };
            char[] vowels = new char[] { 'a', 'e', 'i', 'o', 'u' };
            char[] nums = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

            //input is split() on spaces, giving an array of words. empty array of the same length is initialized
            string[] words = s.Split(' ');
            string[] newWordsTemp = new string[words.Length];


            // all string manipulation happens in this for loop. words[] is iterated over and each word is translated and assigned to the
            // corresponding place in newWordsTemp[]
            for(int i = 0; i < words.Length; i++)
            {
                // ints are set for the various IndexOfAny(), letting me make decisions based on whether or not chars exist, and also where
                // they exist at in each word
                int hasVowel = words[i].IndexOfAny(vowels);
                int hasPunct = words[i].IndexOfAny(punctuation);
                int hasSpec = words[i].IndexOfAny(specChar);
                int hasNum = words[i].IndexOfAny(nums);
                int wordLeng = words[i].Length;

                // ensures words that are numbers (e.g. 15.99) are not changed
                if (hasNum != -1)
                {
                    newWordsTemp[i] = words[i];
                }

                // if a vowel is not at index 0 and the word is not a number, consonants are stripped from the front, added to the back along
                // with ay
                if (hasVowel != 0 && hasVowel != -1 && hasNum == -1)
                {
                    // sub is set to the substring containing all chars before the first vowel
                    string sub = words[i].Substring(0, hasVowel);
                    // tempword is set to the word minus all chars before the first vowel
                    string tempWord = words[i].Remove(0, hasVowel);
                    // subChar1 is set to the ascii value for the first char in sub(used for handling capitilization)
                    int subChar1 = (int)sub[0];


                    // if the first char in sub is caps, words[i] is passed to HandleCaps
                    if(subChar1 >= 65 && subChar1 <= 90)
                    {
                        newWordsTemp[i] = HandleCaps(words[i]);
                    }
                    // if the first char in sub is lower, newWordsTemp[i] is set to tempWord+sub+ay
                    else if(subChar1 >= 97 && subChar1 <= 122)
                    {
                        newWordsTemp[i] = String.Concat(tempWord, sub, "ay");
                    }
                 
                }

                // way is added to the end of words[i] if the first letter of words[i] is a vowel and the length of words[i] is not 1 or if the
                // vowel in the word is y
                else if(hasVowel == 0 || hasVowel == -1 && wordLeng != 1)
                {
                    newWordsTemp[i] = String.Concat(words[i], "way");
                    Console.WriteLine(newWordsTemp[i]);
                }

                //if word[i] is a single char, it is left alone
                else if(wordLeng == 1)
                {
                    newWordsTemp[i] = words[i];
                }

                // if there is punctuation in words[i] and it is not a number, newWordsTemp[i] is set to HandlePunct(newWordsTemp[i]
                if(hasPunct != -1 && hasNum == -1)
                {
                    newWordsTemp[i] = HandlePunct(newWordsTemp[i]);
                }

                // if there are special chars in words[i], nothing is changed
                if (hasSpec != -1)
                {
                    newWordsTemp[i] = words[i];
                }

            }

            // newWordsTemp is made into a string
            string translatedSentence = string.Join(" ", newWordsTemp);


            // user is asked if they would like to encrypt their string, if yes, translatedSentence is passed to Encrypt()
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

        // makes sure that if a word in PigLatinizer has a capital letter, the first letter of the translated word is capitalized
        public static string HandleCaps(string word)
        {
            
            char[] vowels = new char[] { 'a', 'e', 'i', 'o', 'u' };
            int hasVowel = word.IndexOfAny(vowels);
            int wordLeng = word.Length;
            string newWord;
            
            // sub is set to the letters in word before the first letter, tempword is set to all letters after the 
            // first vowel
            string sub = word.Substring(0, hasVowel);
            string tempWord = word.Remove(0, hasVowel);

            // tempFirst is set to Upper(tempWord[0]) , the first char in tempword is rremoved, and the new, upper first char is added back 
            // on
            char tempFirst = char.ToUpper(tempWord[0]);
            tempWord = tempWord.Remove(0, 1);
            tempWord = String.Concat(tempFirst, tempWord);

            // first letter in sub is set to lower
            char subFirst = char.ToLower(sub[0]);
            sub = sub.Remove(0, 1);
            sub = String.Concat(subFirst, sub);

            // correct case, translated string is made and then returned
            newWord = String.Concat(tempWord, sub, "ay"); 

            return newWord;
        }

        public static string HandlePunct(string word)
        {
            char[] punctuation = new char[] {',', ':', ';', '?', '!', '.' };
            int indexPunc = word.IndexOfAny(punctuation);

            // punctuation is removed from wherever it is and placed at the end of the word
            string punct = word.Substring(indexPunc, 1);
            string noPunc = word.Remove(indexPunc, 1);
            string newString = string.Concat(noPunc, punct);

            return newString;
        }
         
        // Encrypt is a Caesar cypher, that is fed a sentence or prompts the user for a sentence if none is passed in
        public static string Encrypt(string sentence = "")
        {
            // if no sentence is passed into Encrypt, the user is prompted for one
            if (sentence == "")
            {
                Console.WriteLine("Please enter a string you wish to encrypt");
                sentence = Console.ReadLine();
            }

            // two arrays are made, one from the input and an empty one set to length of the other
            char[] chars = sentence.ToCharArray();
            char[] spunChar = new char[chars.Length];

            // user is prompted for a key between 1 and 25, and the input is validated
            Console.WriteLine("Enter a key between 1 and 25. This key is how many places you'd like to shift the sentence by.\n");
            string tempKey = Console.ReadLine();
            int key;

            while (!Int32.TryParse(tempKey, out key) || key > 25)
            {
                Console.WriteLine("Please enter an integer between 1 and 25:\n");
                tempKey = Console.ReadLine();
            }

            // charLeng is set to the length of the chars array, for use as the upper bounds of the for loop
            int charLeng = chars.Length;

            // for loop iterates through chars and shifts each char accordingly
            for (int i = 0; i < charLeng; i++)
            {
                if (Char.IsLetter(chars[i]))
                {
                    // ascii is set to the ascii code of the char + the key
                    int ascii = (int)chars[i] + key;

                    // conditions are set to help rotate from z-a and from Z-A, as well as handle spaces
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
                    else if (chars[i] == ' ')
                    {
                        ascii = 32;
                    }

                    spunChar[i] = (char)ascii;
                }
            }

            // encryptedString is created from the spunChar array
            string encryptedString = new string(spunChar);

            // the string is printed and returned
            Console.WriteLine("Your encrypted string is:\n");
            Console.WriteLine(encryptedString + "\n");

            return encryptedString;
        }
         
        // Decrypt does the same thing as Encrypt, only in reverse
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
