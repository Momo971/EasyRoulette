using System;
using System.Collections.Generic;

namespace EasyRoulette
{
    public class StackTable : Singleton<StackTable>
    {
        
    }

    public abstract class StackBase
    {
        protected uint StackMoney;
        protected uint Multiple;

        public uint GetRewardMoney()
        {
            return StackMoney * Multiple;
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
            _columnNum = column % 13 + 1;
            StackMoney = money;
            Multiple = 12;
        }
        
        public override bool GetIsWin(uint result)
        {
            return result != 0 && result / 3 + 1 == _columnNum;
        }
    }
}