using System.Collections.Generic;
using TakAI.Models;

namespace TakAI.Models
{
    public enum Direction { Right, Left, Up, Down }
    public class Move
    {
        private Stone _targetStone;
        public string MoveNotation;
        private string _targetStoneLocation;
        private Direction _moveDirection;
        //TODO this seems redundant w/the dropstack
        private int _stonesToMove;
        //TODO dropstack
        private Stack<int> _dropStack;
    }
}