using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnigmaLite
{
    class Wheel
    {
        public int WheelRanking { get; set; }
        private int _setting;

        private const int AsciiFirst = 33, AsciiLast = 122;

        private List<int> _pickTable;
        private HashSet<int> _searchPickTable;

        private char[] _permLetters;

        private Random rand;

        public Wheel(char setting, int wheelRanking)
        {
            WheelRanking = wheelRanking;
            if (setting>=AsciiFirst&&setting<=AsciiLast)
            {
                _setting = setting*WheelRanking;
            }
            else
            {
                throw new ArgumentOutOfRangeException($"The provided symbol '{setting}' is not supported. Only use Ascii characters between {AsciiFirst} and {AsciiLast} inclusive");
            }
            SetupPermTable();
        }

        public char PermLetter(char input)
        {
            if (_searchPickTable.Contains(input))
            {
                char letter1;
                char letter2;
                do
                {
                    int pick1 = rand.Next(0, _pickTable.Count);
                    letter1 = (char)_pickTable[pick1];

                    _pickTable.RemoveAt(pick1);
                    _searchPickTable.Remove(letter1);

                    int pick2 = rand.Next(0, _pickTable.Count);
                    letter2 = (char)_pickTable[pick2];

                    _pickTable.RemoveAt(pick2);
                    _searchPickTable.Remove(letter2);

                    _permLetters[letter1 - AsciiFirst] = letter2;
                    _permLetters[letter2 - AsciiFirst] = letter1;

                } while (letter1!=input&&letter2!=input);
            }
            else if (input<AsciiFirst||input>AsciiLast)
            {
                throw new ArgumentOutOfRangeException($"The provided symbol '{input}' is not supported. Only use Ascii characters between {AsciiFirst} and {AsciiLast} inclusive");
            }

            char result = _permLetters[input - AsciiFirst];
            return result;
        }

        public bool TurnWheel()
        {
            _setting++;
            if (_setting==AsciiLast*WheelRanking+1)
            {
                _setting = AsciiFirst * WheelRanking;
            }
            SetupPermTable();
            return _setting == AsciiFirst * WheelRanking;
        }

        private void SetupPermTable() //This should be different. We want the picktable and searchPickTable to have a default value they can be set back to, instead of doing the loop.
        {
            rand = new Random(_setting);
            _pickTable = new List<int>();
            _searchPickTable = new HashSet<int>();
            _permLetters = new char[AsciiLast - AsciiFirst + 1];
            for (int i = AsciiFirst; i <= AsciiLast; i++)
            {
                _pickTable.Add(i);
            }
            _searchPickTable.UnionWith(_pickTable);
        }
    }
}
