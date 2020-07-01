using System;

namespace EasyRoulette
{
    public class Roulette : Singleton<Roulette>
    {
        private uint _jackPotIndex = 0;
        private uint _ballIndex = 0;

        private const uint MaxIndex = 37;
        
        private readonly int[] _rouletteArray = new int[]
        {
            0,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27,28,29,30,31,32,33,34,35,36
        };

        public void Roll()
        {
            Random random = new Random();
            uint randomStepNum = (uint)random.Next(1000);
            _jackPotIndex = (_jackPotIndex + randomStepNum) % MaxIndex;

            _ballIndex = (uint) random.Next(1000) % MaxIndex;
        }
        
    }

}