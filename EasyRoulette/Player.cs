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
            _amountOfMoney = 1200;
        }

        public uint GetAmountMoney()
        {
            return _amountOfMoney;
        }
    }
}