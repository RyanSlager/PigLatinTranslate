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
                Console.WriteLine($"{name}, please choose an option:\n1) Translate English to Pig Latin\n2) Encrypt String\n3) 3) Decrypt String" +
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
                if (hasVowel != 0 && hasNum == -1)
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

                // way is added to the end of words[i] if the first letter of words[i] is a vowel and the length of words[i] is not 1
                else if(hasVowel == 0 && wordLeng != 1)
                {
                    newWordsTemp[i] = String.Concat(words[i], "way");
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
            //if no sentence is passed in, the user is prompted for a sentence
            if(sentence == "")
            {
                Console.WriteLine("Please enter a string you wish to encrypt\n");
                sentence = Console.ReadLine();
                Console.WriteLine();
            }

            // two char arrays are created of equal length, one from teh input, and one that is empty
            char[] chars = sentence.ToCharArray();
            char[] spunChar = new char[chars.Length];

            // a key is provided by the user, input is validated
            Console.WriteLine("Enter how many places you'd like to rotate the sentence by:\n");
            string strKey = Console.ReadLine();
            int key;

            Console.WriteLine();

            while (!Int32.TryParse(strKey, out key))
            {
                Console.WriteLine("Please enter an integer:\n");
                strKey = Console.ReadLine();
            }

            // charLength is set, to be used as the upper bound of the for loop
            int charLeng = chars.Length;


            // chars are rotated one by one as the array is iterated over
            for (int i = 0; i < charLeng; i++)
            {
                // tempKey is set, and charKey is set to int of chars[i](the ascii value)
                int tempKey = key;
                int charKey = (int)chars[i];

                // if char[i] is a letter, the key is added to the ascii value, and set to the int ascii
                if (Char.IsLetter(chars[i]))
                {
                    int ascii = (int)chars[i] + key;

                    // if chars[i] is a space, it is recorded as a space in spunChar[i]
                    if ((char)charKey == ' ')
                    {
                        spunChar[i] = ' ';
                    }

                    // while ascii goes past 'z', tempKey is decremented by 26 and ascii is iset to charKey + tempKey
                    // making sure that the final result is within a-z
                    else if (ascii > 122)
                    {
                        while (ascii > 122)
                        {
                            tempKey -= 26;
                            ascii = charKey + tempKey;
                        }
                    }

                    // same thing as above, but for upper case chars
                    else if (!(ascii < 90) && (ascii > 97))
                    {
                        while(ascii > 90)
                        {
                            tempKey -= 26;
                            ascii = charKey + tempKey;
                        }
                    }

                    // the rotated char is added to the spunChars[] array
                    spunChar[i] = (char)ascii;
                }
            }
            
            // spunChar is turned into a string, printed and returned
            string encryptedString = new string(spunChar);

            Console.WriteLine("Your encrypted string is:\n");
            Console.WriteLine(encryptedString + "\n");

            return encryptedString;
        }
         
        // Decrypt does the same thing as Encrypt, only in reverse
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

            string encryptedString = new string(spunChar);

            Console.WriteLine("Your encrypted string is:\n");
            Console.WriteLine(encryptedString + "\n");

            return encryptedString;
        }

    }
}
