using System;
using System.Collections.Generic;

namespace EasyRoulette
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Player player = new Player(1, "Xu");
            
            player.ShowMoney();

            for (int k = 0; k < 10; k++)
            {
                var invest = player.GetAmountMoney() / 40;
                for (int i = 0; i < 10; i++)
                {
                    AreaStack stackA = new AreaStack(AreaStack.AreaKind.One, invest);
                    SingleStack stackB = new SingleStack(11, invest);
                    LineStack stackC = new LineStack(LineStack.LineKind.Two, invest);
                    EvenOddStack stackD = new EvenOddStack(true, invest);
                    player.Bet(stackA);
                    player.Bet(stackB);
                    player.Bet(stackC);
                    player.Bet(stackD);
                    Roulette.GetInstance().Roll(i);
                    StackTable.GetInstance().SettleBills();
                }
                player.ShowMoney();

                if (player.GetAmountMoney() < 1000) break;
                
                Console.ReadKey();
            }

            Console.WriteLine("OVER!");
            Console.ReadKey();
        }
    }
}