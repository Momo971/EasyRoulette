using System;
using System.Collections.Generic;

namespace EasyRoulette
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            List<uint> jackPotList = new List<uint>();
            List<uint> ballList = new List<uint>();
            
            for (int i = 0; i < 36; i++)
            {
                Roulette.GetInstance().Roll(i);
                jackPotList.Add(Roulette.GetInstance().GetJackPotIndex());
                ballList.Add(Roulette.GetInstance().GetBallIndex());
            }
            Roulette.GetInstance().ShowHistory();

        }
    }
}