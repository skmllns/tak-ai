using System.Collections.Generic;

namespace TakAI.Models
{
    public enum Direction { Right, Left, Up, Down }
    public class MoveTurn : Turn
    {
        public Direction MoveDirection;
        //TODO this seems redundant w/the dropstack
        public int StonesToMove;
        //TODO dropstack
        public Stack<int> DropStack;
    }
}

//validate turn?
    //stone type, direction