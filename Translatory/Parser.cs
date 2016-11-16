using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Translatory
{
    public class Parser
    {
        public string Current { get; set; }
        public int CurrentIndex { get; set; }
        public List<string> Tokens { get; set; }

        public void Parse(List<string> tokens)
        {
            Tokens = tokens;

            Current = tokens.First();
            CurrentIndex = 0;

            W();
        }

        private void Next()
        {
            if (CurrentIndex + 1 < Tokens.Count)
            {
                CurrentIndex++;
                Current = Tokens[CurrentIndex];
            }
        }
        private void Accept(string expect)
        {
            if (expect == Current)
            {
                Next();
            }
            else
            {
                throw new System.ArgumentException("aktualny symbol rozni sie od oczekiwanego");
            }
        }

        private void W()
        {
            S();
            X();
        }

        private void S()
        {
            C();
            Y();
        }

        private void C()
        {
            if (Current == "FLOAT")
            {
                Accept("FLOAT");
            }
            else if (Current == "INT")
            {
                Accept("INT");
            }
            else if (Current == "SYMBOL")
            {
                Accept("SYMBOL");

            }
            else
            {
                Accept("LEFT");
                W();
                Accept("RIGHT");
            }
        }

        private void X()
        {
            if (Current == "OPERATOR")
            {
                Accept("OPERATOR");
                S();
                X();
            }
            else if (Current == "OPERATOR")
            {
                Accept("OPERATOR");
                S();
                X();
            }
        }

        private void Y()
        {
            if (Current == "OPERATOR")
            {
                Accept("OPERATOR");
                C();
                Y();
            }
            else if (Current == "OPERATOR")
            {
                Accept("OPERATOR");
                C();
                Y();
            }
        }
    }
}
