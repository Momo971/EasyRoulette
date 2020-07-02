using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace EasyRoulette
{
    public class StackTable : Singleton<StackTable>
    {
        private List<Player> _players = new List<Player>();
        private List<StackBill> _billBuffer = new List<StackBill>();

        public void AddPlayer(Player player)
        {
            if(!_players.Contains(player))
                _players.Add(player);
        }
        public void CreateBill(uint playerid, StackBase stackData)
        {
            var bill = new StackBill(playerid, stackData);
            _billBuffer.Add(bill);
        }

        public void SettleBills()
        {
            foreach (var bill in _billBuffer)
            {
                var player =  _players.Find(item => item.IsThis(bill.GetPlayerId()));
                player?.AddMoney(bill.GetRewardAmount());
            }

            _billBuffer.Clear();
        }
        
    }

    public class StackBill
    {
        private uint _playerId;
        private StackBase _stackData;
        
        public StackBill(uint playerid, StackBase stackData)
        {
            _playerId = playerid;
            _stackData = stackData;
        }

        public uint GetPlayerId()
        {
            return _playerId;
        }

        public uint GetRewardAmount()
        {
            uint reValue = 0;
            var ballIndex = Roulette.GetInstance().GetBallIndex();
            if (_stackData.GetIsWin(ballIndex))
            {
                reValue += _stackData.GetRewardMoney();
            }

            return reValue;
        }
    }

    public abstract class StackBase
    {
        protected uint StackMoney;
        protected uint Multiple;

        public uint GetRewardMoney()
        {
            return StackMoney * Multiple;
        }

        public uint GetPrice()
        {
            return StackMoney;
        }

        public abstract bool GetIsWin(uint result);
        
    }

    public class SingleStack : StackBase
    {
        private uint _stackIndex;
        
        public SingleStack(uint index, uint money)
        {
            _stackIndex = index;
            StackMoney = money;
            Multiple = 36;
        }

        public override bool GetIsWin(uint result)
        {
            return _stackIndex == result;
        }
    }

    public class EvenOddStack : StackBase
    {
        private bool _isOdd;
        
        public EvenOddStack(bool isOdd, uint money)
        {
            _isOdd = isOdd;
            StackMoney = money;
            Multiple = 2;
        }

        public override bool GetIsWin(uint result)
        {
            return result != 0 && result % 2 != 1;
        }
    }

    public class LineStack : StackBase
    {
        public enum LineKind
        {
            One = 1,
            Two,
            Three
        }
        
        private readonly LineKind _kind;

        public LineStack(LineKind kind, uint money)
        {
            _kind = kind;
            StackMoney = money;
            Multiple = 3;
        }
        
        public override bool GetIsWin(uint result)
        {
            return result != 0 && (result % 3) == (uint) _kind;
        }
    }

    public class AreaStack : StackBase
    {
        public enum AreaKind
        {
            One = 1,
            Two,
            Three
        }

        private readonly AreaKind _kind;

        public AreaStack(AreaKind kind, uint money)
        {
            _kind = kind;
            StackMoney = money;
            Multiple = 3;
        } 
        
        public override bool GetIsWin(uint result)
        {
            return result != 0 && result/12 + 1 == (uint) _kind;
        }
    }

    public class SizeStack : StackBase
    {
        private bool _isBig;

        public SizeStack(bool isBig, uint money)
        {
            _isBig = isBig;
            StackMoney = money;
            Multiple = 2;
        }

        public override bool GetIsWin(uint result)
        {
            return result != 0 && result > 18;
        }
    }

    public class ColumnStack : StackBase
    {
        private uint _columnNum;

        public ColumnStack(uint column, uint money)
        {
            _columnNum = column > 12 || column < 1 ? 1 : _columnNum;
            StackMoney = money;
            Multiple = 12;
        }
        
        public override bool GetIsWin(uint result)
        {
            return result != 0 && result / 3 + 1 == _columnNum;
        }
    }
}