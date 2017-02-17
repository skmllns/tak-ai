using System.Collections.Generic;

namespace TakAI.Models
{
    public class Board
    {
        public Board(int length)
        {
            SideLength = length;
            Positions = new List<Position>();
        }

        public int SideLength { get; set; }
        public List<Position> Positions { get; set; }
    }
}