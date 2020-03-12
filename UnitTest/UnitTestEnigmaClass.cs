using System;
using EnigmaLite;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest
{
    [TestClass]
    public class UnitTestEnigmaClass
    {
        private Enigma enigma;

        [TestMethod]
        public void PermLetter_SymbolIsPermutated()
        {
            Setup();

            char input = 'a';
            char encrypted = enigma.PermLetter(input);

            Assert.AreNotEqual(input, encrypted);
        }

        [TestMethod]
        public void PermLetter_EncryptAndDecryptSymbol()
        {
            Setup();

            char expected = '4';
            char encrypted = enigma.PermLetter(expected);
            Setup();
            char actual = enigma.PermLetter(encrypted);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void PermString_EncryptAndDecryptString()
        {
            Setup();

            string expected = "Expected_String";
            string encrypted = enigma.PermString(expected);
            Setup();
            string actual = enigma.PermString(encrypted);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void PermString_SingleWheelTurnOver_RepeatedPermutation()
        {
            enigma = new Enigma(new []{'!'});

            string Permutation = "";
            for (int i = 0; i < 90; i++)
            {
                Permutation += 'a';
            }

            string expected = enigma.PermString(Permutation);

            Permutation = "";
            for (int i = 0; i < 90; i++)
            {
                Permutation += 'a';
            }

            string actual = enigma.PermString(Permutation);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void PermString_DoubleWheelTurnOver_NonRepeatedPermutation()
        {
            enigma = new Enigma(new[] { '!', 'a' });

            string Permutation = "";
            for (int i = 0; i < 90; i++)
            {
                Permutation += 'a';
            }

            string expected = enigma.PermString(Permutation);

            Permutation = "";
            for (int i = 0; i < 90; i++)
            {
                Permutation += 'a';
            }

            string actual = enigma.PermString(Permutation);

            Assert.AreNotEqual(expected, actual);
        }

        [TestMethod]
        public void ResetToInitialState_ResetSuccessful()
        {
            Setup();

            char expected = '4';
            char encrypted = enigma.PermLetter(expected);
            enigma.ResetToInitialState();
            char actual = enigma.PermLetter(encrypted);

            Assert.AreEqual(expected, actual);
        }

        private void Setup()
        {
            enigma = new Enigma(new []{'a', 'A', 'g'});
        }
    }
}
