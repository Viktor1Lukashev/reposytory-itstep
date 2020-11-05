using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace game
{
    class Player
    {
        private Queue<Karta> karta;
        public string Name { get; private set; }
        public int Count
        {
            get;set;
        }
        public Player(string name)
        {

            karta = new Queue<Karta>();
            Name = name;

        }
        public void SetCar(Karta k)
        {
            karta.Enqueue(k);
            Count++;
        }
        public Karta Getkarta()
        {
            //showkarta();
            Count--;
            return karta.Dequeue();
            
        }
        public void showkarta()
        {
            karta.Peek().GetCard();
            //Karta m = karta.Dequeue();
            //karta.Enqueue(m);
          
        }
        public void ShowAllCards()
        {
            int c = 0;
            Console.WriteLine($"карты {Name}: ");
            while (c < Count)
            {
                showkarta();
                Karta m = karta.Dequeue();
                karta.Enqueue(m);
                c++;
            }
        }
        
    }
}
