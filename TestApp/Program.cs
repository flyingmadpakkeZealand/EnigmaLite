using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using EnigmaLite;

namespace TestApp
{
    class Program
    {
        enum TestEnum
        {
            Value1
        }

        static void Main(string[] args)
        {
            char[] settings = new char[] { 'a', 'b' };
            Enigma test = new Enigma(settings);

            //string encodedString = test.PermString("Awesome_Job!");
            //Console.WriteLine("Encode: Awesome_Job! -> " + encodedString);
            //string saveEncodedString = encodedString;

            //Just some branch Code...

            //test = new Enigma(settings);
            //encodedString = test.PermString(encodedString);
            //Console.WriteLine("Decode: " + saveEncodedString + " -> " + encodedString);

            SolvePassWord("PassWord123", PassWordExample("PassWord123"));

            //SolvePassWord("PassWord123", "a\\v;\\w\\@>.kTn\"VD0GG5w"); //From database

            //test.ContinuousType();
            Console.ReadLine();
        } //E>b9k_-x#xoO 

        public static string PassWordExample(string password)
        {
            
            if (password.Length<=20&&password.Length>=3)
            {
                Enigma enigma = new Enigma(new char[]{password[0], password[password.Length-1], password[password.Length/2]});

                Random rand = new Random(DateTime.Now.Millisecond);
                int fillerLength = 20 - password.Length;
                char fillerLetter = (char) rand.Next(33, 123);
                string fillerString = "";
                for (int i = 0; i < fillerLength; i++)
                {
                    fillerString += fillerLetter;
                }

                string encodedPassword = enigma.PermString(password + fillerString);
                char endCipher = (char)(33 + password.Length);
                encodedPassword += enigma.PermLetter(endCipher);

                Console.WriteLine("What is stored in database:");
                Console.WriteLine(encodedPassword);

                return encodedPassword;
            }

            return null;
        }

        public static void SolvePassWord(string password, string encryptedPassWordFromUser)
        {
            if (password.Length <= 20 && password.Length >= 3)
            {
                Enigma enigma = new Enigma(new char[] { password[0], password[password.Length - 1], password[password.Length / 2] });

                string decryptedPassWord = enigma.PermString(encryptedPassWordFromUser);

                Console.WriteLine("What the raw decrypted value is, the last character is the length of the actual user password stored as a char:");
                Console.WriteLine(decryptedPassWord);

                char endCipher = decryptedPassWord[decryptedPassWord.Length - 1];

                string actualPassword = decryptedPassWord.Substring(0, endCipher-33);

                Console.WriteLine("The actual user password is extracted from the decrypted string and is matched against the typed password:");
                Console.WriteLine(password + " : " + actualPassword);
            }
        }
    }
}
