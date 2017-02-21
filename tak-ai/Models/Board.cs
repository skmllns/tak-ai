using System.Collections.Generic;

namespace TakAI.Models
{
    public class Board
    {
        public Board(int length)
        {
            SideLength = length;
            Positions = new Dictionary<string, Stack<Stone>>();
        }
        public int SideLength { get; set; }
        public Dictionary<string, Stack<Stone>> Positions;
    }
}