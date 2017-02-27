using System.Collections.Generic;

namespace TakAI.Models
{
    public class Game
    {
        public Game(int length)
        {
            Turns = new List<Turn>();
            Boards = new List<Board>
            {
                new Board(length)
            };
        }

        public List<Board> Boards;
        public List<Turn> Turns;
    }
}