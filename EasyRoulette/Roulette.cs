using System;
using System.Collections.Generic;

namespace EasyRoulette
{
    public class Roulette : Singleton<Roulette>
    {
        private uint _jackPotIndex = 0;
        private uint _ballIndex = 0;

        private const uint MaxIndex = 37;

        private List<uint> _historyJackPot = new List<uint>();
        private List<uint> _historyBall = new List<uint>();
        
        public void Roll(int seed = 0)
        {
            Random random = new Random(Guid.NewGuid().GetHashCode());
            uint randomStepNum = (uint)random.Next(1000);
            _jackPotIndex = (_jackPotIndex + randomStepNum) % MaxIndex;
            _ballIndex = (uint) random.Next(1000) % MaxIndex;
            
            RecordHistory();
        }
        
       

        public uint GetBallIndex()
        {
            return _ballIndex;
        }

        public uint GetJackPotIndex()
        {
            return _jackPotIndex;
        }

        #region ShowMethods

        public void ShowResult()
        {
            string showStr = "\n" +
                             "****** RESULT ******\n" +
                             "   JackPot:   {0}  \n" +
                             "   BallIndex: {1}  \n" + 
                             "********************\n";
            Console.WriteLine(showStr, _jackPotIndex, _ballIndex);
        } 
        
        public void ShowHistory()
        {
            var jackPotArray = new int[37];
            var ballArray = new int[37];

            foreach (var item in _historyJackPot)
            {
                jackPotArray[item]++;
            }

            foreach (var item in _historyBall)
            {
                ballArray[item]++;
            }

            Console.WriteLine(" JackPot History Times");
            for (int i = 0; i < jackPotArray.Length; i++)
            {
                Console.WriteLine(" The {0} Num: {1} ", i, jackPotArray[i]);
            }
            Console.WriteLine(" Ball History Times");
            for (int i = 0; i < ballArray.Length; i++)
            {
                Console.WriteLine(" The {0} Num: {1} ", i, ballArray[i]);
            }
        }

        #endregion
        
        
        private void RecordHistory()
        {
            _historyJackPot.Add(_jackPotIndex);
            _historyBall.Add(_ballIndex);
        }

    }

}