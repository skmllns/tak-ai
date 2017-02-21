using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TakAI.Models;

namespace TakAI.Services
{
    public class TurnParser
    {
        public Dictionary<string, Direction> DirectionDict = new Dictionary<string, Direction>()
        {
         {">", Direction.Right},
         {"<", Direction.Left},
         {"+", Direction.Up},
         {"-", Direction.Down}
        };

        public void ParseFile()
        {
           

        }

        //TODO this just returns an empty move, have to hook this up with collections of pieces
        public Move ParseTurn(Board board, string turn)
        {

            Move move = new Move();
            //TODO could merge these regexs
            //right now this takes an input of a single player's turn
            string stoneType;
            string boardSquare;
            List<char> alphabet = Enumerable.Range(97, 26).Select(x => (char)x).ToList();
            int sideLength = board.SideLength;
            char maxAlpha = alphabet[sideLength - 1];
            int maxNum = sideLength;
            string stoneLocation = $@"[a-{maxAlpha}]{{1}}[1-{maxNum}]{{1}}";

            Regex placeStoneRegex = new Regex($@"^[FSC]{{0,1}}{stoneLocation}$");
            if (placeStoneRegex.IsMatch(turn))
            {
                MatchCollection matches = placeStoneRegex.Matches(turn);
                stoneType = matches[0].Length != 3 ? "F" : matches[0].Groups[0].Value[0].ToString();
                boardSquare = matches[0].Groups[0].Value.Substring(1);
            }


            Regex moveStoneRegex = new Regex($"([0-9]*)({stoneLocation})([><+-]{{1}})([1-9]*)([FSC]*)");
            if (moveStoneRegex.IsMatch(turn))
            {
                MatchCollection matches = moveStoneRegex.Matches(turn);

                boardSquare = matches[0].Groups[2].ToString();
                //TODO this seems totally unnecessary
                stoneType = matches[0].Groups[5].ToString();

                Direction direction = DirectionDict[matches[0].Groups[3].ToString()];
                Stack<int> dropStack = new Stack<int>();
                int count = int.Parse(matches[0].Groups[1].ToString());
                string dropCounts = matches[0].Groups[4].ToString();
                
                foreach (char dropCount in dropCounts)
                {
                    dropStack.Push(int.Parse(dropCount.ToString()));
                }
                
            }
            return move;

        }

    }



}