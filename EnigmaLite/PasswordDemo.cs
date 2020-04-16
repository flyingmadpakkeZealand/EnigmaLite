using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnigmaLite
{
    public class PasswordDemo
    {
        private int _maxLength;
        private int _minLength;

        public PasswordDemo(int maxLength, int minLength)
        {
            if (maxLength > 50)
            {
                throw new ArgumentOutOfRangeException(nameof(maxLength), "Please use a length of 50 or less");
            }
            _maxLength = maxLength;
            _minLength = minLength;
        }

        public string StandardLengthEncrypt(string password)
        {
            if (password.Length <= _maxLength && password.Length >= _minLength)
            {
                Enigma enigma = new Enigma(new char[] { password[0], password[password.Length - 1], password[password.Length / 2] });

                Random rand = new Random(DateTime.Now.Millisecond);
                int fillerLength = _maxLength - password.Length;
                char fillerLetter = (char)rand.Next(33, 123);
                string fillerString = "";
                for (int i = 0; i < fillerLength; i++)
                {
                    fillerString += fillerLetter;
                }

                string encodedPassword = enigma.PermString(password + fillerString);
                char endCipher = (char)(33 + password.Length);
                encodedPassword += enigma.PermLetter(endCipher);

                return encodedPassword;
            }

            return null;
        }
    }
}
