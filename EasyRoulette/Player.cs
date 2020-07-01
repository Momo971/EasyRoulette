using System;

namespace EasyRoulette
{
    public class Player
    {
        private uint _id;
        private string _name;
        private uint _amountOfMoney;

        public Player(uint id, string name)
        {
            _id = id;
            _name = name;
            _amountOfMoney = 10000;
            StackTable.GetInstance().AddPlayer(this);
        }

        public uint GetAmountMoney()
        {
            return _amountOfMoney;
        }

        public void AddMoney(uint increasement)
        {
            _amountOfMoney += increasement;
        }
        
        public void Bet(StackBase stackData)
        {
            if (stackData.GetPrice() > this._amountOfMoney)
            {
                Console.WriteLine("代币不足 >_<!");
                return;
            }

            this._amountOfMoney -= stackData.GetPrice();
            StackTable.GetInstance().CreateBill(this._id, stackData);
        }

        public bool IsThis(uint id)
        {
            return id == _id;
        }

        public void ShowMoney()
        {
            Console.WriteLine(" PlayerName : {0}\n Money: {1}", _name, _amountOfMoney);
        }
    }
}