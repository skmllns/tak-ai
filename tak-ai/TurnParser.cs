using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TakAI.Models;

namespace TakAI
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

        public void ParseTurn()
        {
            string turnNotation = "asdf";
            ValidateSyntax(turnNotation);
           

        }

        public bool ValidateSemantics(string turnNotation, out Turn turn)
        {
            turn = null;
            return false;

        }
        //TODO this just returns an empty move, have to hook this up with collections of pieces
        public void ValidateSyntax(string turnNotation)
        {
            //move move
            //move place
            //place move
            //place place
            //move (SCORE) 
            //place (SCORE)
            //SCORE: R-0, 0-R, F-0, 0-F, 1-0, 0-1, 1/2-1/2
            //right now this takes an input of a single player's turn

            //string boardSquare;
            //List<char> alphabet = Enumerable.Range(97, 26).Select(x => (char)x).ToList();
            //int sideLength = boardSideLength;
            //char maxAlpha = alphabet[sideLength - 1];
            //int maxNum = sideLength;
           
            //split by space
            List<string> turnMoves = turnNotation.Split(' ').ToList(); 

            List<string> validEndGame = new List<string>
            {
                "R-0", "0-R", "R-0", "0-F", "1-0", "0-1", "1/2-1/2"
            };
            string stoneType;
            string stoneLocation = @"[a-z]{{1}}[1-9]{{1}}";

            //for placing turns
            Regex placeStoneRegex = new Regex($@"^[FSC]{{0,1}}{stoneLocation}$");
            if (placeStoneRegex.IsMatch(turnNotation))
            {
                MatchCollection matches = placeStoneRegex.Matches(turnNotation);
                PlaceTurn placeTurn = new PlaceTurn
                {
                    StoneType = matches[0].Length != 3 ? "F" : matches[0].Groups[0].Value[0].ToString(),
                    TurnNotation = matches[0].Groups[0].Value,
                    TargetLocation = matches[0].Groups[0].Value.Substring(1)
                };
                //return true;
            }

            //for moving turns
            Regex moveStoneRegex = new Regex($"([0-9]*)({stoneLocation})([><+-]{{1}})([1-9]*)([FSC]*)");
            if (moveStoneRegex.IsMatch(turnNotation))
            {
                MatchCollection matches = moveStoneRegex.Matches(turnNotation);

                Stack<int> dropStack = new Stack<int>();
                string dropCounts = matches[0].Groups[4].ToString();
                foreach (char dropCount in dropCounts)
                {
                    dropStack.Push(int.Parse(dropCount.ToString()));
                }

                MoveTurn moveTurn = new MoveTurn
                {
                    TurnNotation = matches[0].Groups[0].Value,
                    TargetLocation = matches[0].Groups[2].ToString(),
                    MoveDirection = DirectionDict[matches[0].Groups[3].ToString()],
                    StonesToMove = int.Parse(matches[0].Groups[1].ToString()),
                    DropStack = dropStack
                };
                //return moveTurn;
            }


        }
    }


}