using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnigmaLite
{
    /// <summary>
    /// A non-static class that offers encryption of text akin to the infamous Enigma Machine from WW2 - It supports Ascii characters from 33 to 122 inclusive.
    /// (Author: Magnus B. Berger)
    /// </summary>
    public class Enigma
    {
        private Wheel[] _wheels;
        private Wheel _reflector;
        private char[] _initialSettings;

        /// <summary>
        /// The constructor that initializes a single Enigma type object. Various methods can be called on this object to encrypt/decrypt text.
        /// </summary>
        /// <param name="settings">Settings sets the initial state of the machine and it must be set back to this state when decrypting text.
        /// The combination of every individual setting can be thought of as the encryption key. The size of settings can be related to the complexity of the machine</param>
        public Enigma(char[] settings)
        {
            SetupEnigma(settings);
            _initialSettings = settings;
        }

        private void SetupEnigma(char[] settings)
        {
            _reflector = new Wheel('A', 0);
            _wheels = new Wheel[settings.Length];
            for (int i = 0; i < settings.Length; i++)
            {
                _wheels[i] = new Wheel(settings[i], i + 1);
            }
        }

        /// <summary>
        /// Resets the Enigma object to the same state as when the object was first created.
        /// </summary>
        public void ResetToInitialState()
        {
            SetupEnigma(_initialSettings);
        }

        /// <summary>
        /// (FOR CONSOLE APPS ONLY!) Simulates typing on a Enigma typewriter. Each key press is immediately encrypted/decrypted and shown on the screen.
        /// </summary>
        public void ContinuousType()
        {
            StringBuilder encodedString = new StringBuilder();
            while (true)
            {
                Console.Clear();
                Console.Write(encodedString);
                encodedString.Append(PermLetter(Console.ReadKey().KeyChar));
            }
        }

        /// <summary>
        /// Uses the PermLetter method over a whole string. This encrypts/decrypts the whole string at once.
        /// </summary>
        /// <param name="input">The string to be encrypted/decrypted</param>
        /// <returns>The result of encrypting/decrypting a string</returns>
        public string PermString(string input)
        {
            StringBuilder encodedString = new StringBuilder();
            foreach (char letter in input)
            {
                encodedString.Append(PermLetter(letter));
            }

            return encodedString.ToString();
        }

        /// <summary>
        /// The main method of the Enigma class. It uses dynamic permutation tables to associate a symbol with a seemingly random other symbol depending on the state of the Enigma object.
        /// Example: providing 'a' might return '#', providing 'a' again might then return '7'.
        /// </summary>
        /// <param name="input">Any Ascii symbol ranging from 33 to 122 inclusive to be encrypted/decrypted</param>
        /// <returns>The result of passing a symbol through various permutation tables.
        /// Example: if provided with 'a' then 'a', then set the Enigma to its original state and provide the symbols that returned from 'a' and 'a', it will return the original 'a' then 'a'</returns>
        public char PermLetter(char input)
        {
            foreach (Wheel wheel in _wheels)
            {
                input = wheel.PermLetter(input);
            }

            input = _reflector.PermLetter(input);

            foreach (Wheel wheel in _wheels.Reverse())
            {
                input = wheel.PermLetter(input);
            }

            foreach (Wheel wheel in _wheels)
            {
                if (!wheel.TurnWheel())
                {
                    break;
                }
            }

            return input;
        }
    }
}
