using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace game
{
    class Karta
    {
       
        private string _type;
        public int Num { get; private set; }
        public string Suit { get; private set; }
        public Karta(string s, int num) 
        {
            Num = num;
            switch (num)
            {
                case 11:
                    _type = "Валет";
                    break;
                case 12:
                    _type = "Дама";
                    break;
                case 13:
                    _type = "Король";
                    break;
                    case 14:
                    _type = "Туз";
                    break;
                default:
                    _type = num.ToString();
                    break;
            }
            Suit = s;
        }
        public void GetCard()
        {
            Console.Write($" {_type} {Suit}  ");
            
        }
    }
}
