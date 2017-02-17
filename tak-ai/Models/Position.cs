using System.Collections.Generic;
using tak_ai.Models;

namespace TakAI.Models
{
    public class Position
    {
        public int X { get; set; }
        public int Y { get; set; }
        public List<Piece> Pieces { get; set; }
    }
}