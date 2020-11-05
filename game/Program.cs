using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace game
{
    class Program
    {
        static void Main(string[] args)
        {
            //вводим количество игроков и проверяем на корректность
            Console.WriteLine("введите количество игроков для игры (минимум 2) но не больше 36");
            
            bool count = Int32.TryParse(Console.ReadLine(), out int res);
               
                    
            if (count && res >0 && res<36) //если ввод корректный то:
            {
                
                int result = res;
                List<Player> player = new List<Player>(res);
                int names = 0;
                while (res > 0)
                {
                    player.Add(new Player($"игрок {++names}")); //присваисваем игроку имя типа: игрок 1, игрок 2 и т.д
                    res--;
                }
                //создаем массив мастей карт 
                string[] suits = new string[4] { "Черви", "пик", "бубны", "крести" };
                //создаем массив номиналов карт(6-10 так и будут идти по порядку, 11-14 - это валет,дама,король и туз)
                int[] numtype = new int[9] { 6, 7, 8, 9, 10, 11, 12, 13, 14 };
                Stack<Karta> st = new Stack<Karta>(36);//колоду карт я решил хранить в структуре данных "стек", т.к раздача будет по принципу LIFO
                List<Karta> koloda = new List<Karta>(); //перед помещением карт в колоду я кладу их в структуру List для перетасовки
                //генерируем карты и кладем их в List. 
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        koloda.Add(new Karta(suits[i], numtype[j]));
                    }
                }

                //для перетасовки карт была создана переменная типа Random
                Random rnd = new Random();
                for (int i = 0; i < koloda.Count; ++i)
                {

                    Karta v = koloda[i];//берем первую карту из списка и запоминаем ее
                    koloda.RemoveAt(i);//удаляем первую карту из списка
                    koloda.Insert(rnd.Next(koloda.Count), v);//вставляем запомненную карту на рандомную позицию. выбор позиции ограничен размером списка              
                    
                }
                
                for (int i = 0; i < koloda.Count; ++i)
                {
                    st.Push(new Karta(koloda[i].Suit, koloda[i].Num));  //после перетасовки кладем карты в структуру стека

                }

                int CoutnKartsForPlayer = 36 / result;//решаем сколько карт нужно раздать (каждому игроку) в зависимости от кол-ва игроков
                foreach (var i in player)
                {
                    for (int j = 0; j < CoutnKartsForPlayer; ++j)
                    {

                        i.SetCar(st.Pop());//извлекаем необходимое число карт из колоды и разаем каждому игроку

                    }
                }
                //когда у кого-то из игроков закончатся карты - его удалим из списка, поэтому пока в списке игроков кто-то есть:
                Console.WriteLine("Карты разданы все готово к игре!");
                string choise; //переменная для определения когда пользователь захочет выйти из игры
                do
                {

                    Console.ResetColor();
                    Console.Clear();
                    foreach (var i in player)
                    {
                        Console.WriteLine($"{i.Name} - {i.Count} карт");//будем каждый раз выводить сколько карт у игроков
                    }

                    Console.WriteLine("игроки выбрасывают карты: ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    List<Karta> PlayersCards = new List<Karta>();// list для хранения выброшенных карт
                        foreach (var p in player) // в этом цикле будем выводить информацию о имени игрока и его карте
                        {
                        Console.Write($"{p.Name}:");
                        p.showkarta();
                        Console.WriteLine();
                        PlayersCards.Add(p.Getkarta());// добавляем карту в list созданный ранее
                        }
                        int max = PlayersCards[0].Num; // в созданном list определяем какая карта выйграла 
                        int index = 0;// и запоминаем ее индекс

                        for (int i = 1; i < PlayersCards.Count; ++i)
                        {
                            if (PlayersCards[i].Num > max) //если какая-то карта больше по номеналу - меняем max
                            {
                                max = PlayersCards[i].Num;
                                index = i;
                            }
                        }
                  
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine();
                        Console.WriteLine($"карты забирает {player[index].Name}");
                        for (int i = 0; i < PlayersCards.Count; ++i) //после того как мы определили какая карта старше
                        {
                            player[index].SetCar(PlayersCards[i]);//выйгравший игрок забирает все карты из списка
                        }
                        foreach (var p in player.ToArray()) // тут мы преобразуем все элементы  list player в массив и перебираем его
                        {
                            if (p.Count == 0)  //если у какого-то игрока закончились карты
                            {
                               Console.WriteLine($"у {p.Name} закончились карты - он выбывает из игры!");
                                player.Remove(p);//удаляем его из списка (т.к. нельзя менять коллекцию в foreach - я и преобразовал ее в массив)
                                
                            }

                        }
                    // для наглядности выводим какие карты у какого игрока.
                    foreach (var p in player)
                    {
                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        p.ShowAllCards();
                        Console.WriteLine();                        
                    }
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("чтобы играть нажмите любую клавишу, чтобы выйти нажмите 'y'");
                    choise = Console.ReadLine();
                
                } while (player.Any() || choise != "y");
                Console.WriteLine($"поздравляем! победитель игры: {player.First().Name} ");
            }
            else
            {
                Console.WriteLine("Вы ввели некорректное значение ");

            }
            Console.ReadLine();

        }
       
    }
}
