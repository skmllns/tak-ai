using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TakAI;
using TakAI.Models;
using TakAI.Services;

namespace UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void CreateBoardTest()
        {
            Board board = new Board(4);

        }

        [TestMethod]
        public void CreateStoneTest()
        {
            CapStone capStone = new CapStone(Color.Black);

        }

        [TestMethod]
        public void CharTest()
        {

            var y = Enumerable.Range(97, 26).Select(x => (char) x).ToList();

        }

        [TestMethod]
        public void RegExTest()
        {

            Regex turnRegex = new Regex(@"[FSC]{1}[a-z]{1}[0-9]");
            Assert.IsTrue(turnRegex.IsMatch("Fa9"));
        }

        public enum StoneType
        {
            CapStone,
            FlatStone,
            StandingStone
        }

        [TestMethod]
        public void ParsePlaceNotationTest()
        {
            //TODO could probably rewrite this using groups
            char stoneType;
            string boardSquare;
            Board board = new Board(4);
            List<char> alphabet = Enumerable.Range(97, 26).Select(x => (char) x).ToList();
            int sideLength = board.SideLength;
            char maxAlpha = alphabet[sideLength - 1];
            int maxNum = sideLength;
            string turn = "Sb2";

            // Placing stones: (stone*)(square*) [default is F]  
            Regex placeStoneRegex = new Regex($@"[FSC]{{0,1}}[a-{maxAlpha}]{{1}}[1-{maxNum}]{{1}}");
            if (placeStoneRegex.IsMatch(turn))
            {
                MatchCollection matches = placeStoneRegex.Matches(turn);
                stoneType = matches[0].Length != 3 ? 'F' : matches[0].Groups[0].Value[0];
                boardSquare = matches[0].Groups[0].Value.Substring(1);
                Console.WriteLine($"{stoneType}  {boardSquare}");
            }

        }


        [TestMethod]
        public void GeneralRegExTest()
        {
            string test = "1";
            Regex placeStoneRegex = new Regex($@"[0-9]*");
            Assert.IsTrue(placeStoneRegex.IsMatch(test));

        }


        //TODO combine move and place regex
        [TestMethod]
        public void ParseMoveNotationTest()
        {
            // Moving stones: (count)(square*)(direction*)(drop counts**)(stone) 
            int count;
            string boardSquare;
            string direction;
            //TODO: parse this into list
            string dropCounts;
            Stack<int> dropStack = new Stack<int>();
            string stoneType;

            List<char> directionList = new List<char> {'>', '<', '+', '-'};
            Board board = new Board(4);
            char[] alphabet = Enumerable.Range(97, 26).Select(x => (char) x).ToArray();
            int sideLength = board.SideLength;
            char maxAlpha = alphabet[sideLength - 1];
            int maxNum = sideLength;
            string turn = "3c4>12";

            Regex moveStoneRegex =
                new Regex($"([0-9]*)([a-{maxAlpha}]{{1}}[1-{maxNum}]{{1}})([><+-]{{1}})([1-9]*)([FSC]*)");
            Assert.IsTrue(moveStoneRegex.IsMatch(turn));
            if (moveStoneRegex.IsMatch(turn))
            {
                MatchCollection matches = moveStoneRegex.Matches(turn);
                count = int.Parse(matches[0].Groups[1].ToString());
                boardSquare = matches[0].Groups[2].ToString();
                direction = matches[0].Groups[3].ToString();
                dropCounts = matches[0].Groups[4].ToString();
                foreach (char dropCount in dropCounts)
                {
                    dropStack.Push(int.Parse(dropCount.ToString()));
                }
                stoneType = matches[0].Groups[5].ToString();
            }

        }

        [TestMethod]
        public void FileReadTest()
        {
            string ptnFile = string.Join(@"\n", File.ReadAllLines(
                @"C:\Users\smullins2\Documents\GitHub\tak-ai\tak-ai\ReferenceFiles\Test.ptn",
                Encoding.Default));

            string date;
            string playerOne;
            string playerTwo;
            int boardSize;
            string gameResult;
            string movesString;
            Regex dateRegex = new Regex(@"(?<=Date "").*?(?="")");
            Regex playerOneRegex = new Regex(@"(?<=Player1 "").*?(?="")");
            Regex playerTwoRegex = new Regex(@"(?<=Player2 "").*?(?="")");
            Regex boardSizeRegex = new Regex(@"(?<=Size "").*?(?="")");
            Regex gameResultRegex = new Regex(@"(?<=Result "").*?(?="")");
            Regex movesRegex = new Regex(@"(?<=1\.).*? (?=[RF(1/)])");

            //TODO null handling w/poorly formed PTN file
            date = dateRegex.Matches(ptnFile)[0].Groups[0].ToString();
            playerOne = playerOneRegex.Matches(ptnFile)[0].Groups[0].ToString();
            playerTwo = playerTwoRegex.Matches(ptnFile)[0].Groups[0].ToString();
            boardSize = int.Parse(boardSizeRegex.Matches(ptnFile)[0].Groups[0].ToString());
            gameResult = gameResultRegex.Matches(ptnFile)[0].Groups[0].ToString();
            movesString = movesRegex.Matches(ptnFile)[0].Groups[0].ToString();

            List<string> movesList = Regex.Split(movesString, @"\\n").ToList();

            Assert.IsNotNull(movesList);
        }

        [TestMethod]
        public void NewGameTest()
        {
            Game game = new Game(5);
            Assert.IsTrue(game.Boards.FirstOrDefault().SideLength == 5);
        }


        [TestMethod]
        public void ParseEachTurnTest()
        {
            //place move
            string pmNotation = "a4 b5+";
            //move place
            string mpNotation = "e3- c1";
            //place place
            string ppNotation = "a5 a4";
            //move move
            string mmNotation = "a1> d1<";

            //place move (SCORE)
            string pmsNotation = "a4 b5+ 0-F";
            //move place (SCORE)
            string mpsNotation = "e3- c1 0-F";
            //place place (SCORE)
            string ppsNotation = "a5 a4 0-F";
            //move move (SCORE)
            string mmsNotation = "a1> d1< 0-F";
            //move (SCORE) 
            string msNotation = "b5+ 0-F";
            //place (SCORE)
            string psNotation = "b5 0-F";

            //TESTING
            string stringToTest = pmsNotation;
            string endGameStatus;
            bool endGameTurn = false;

            string stoneLocation = @"[a-z]{{1}}[1-9]{{1}}";
            string placeRegex = $@"^[FSC]{{0,1}}{stoneLocation}$";
            string moveRegex = $"([0-9]*)({stoneLocation})([><+-]{{1}})([1-9]*)([FSC]*)";
            string endGameRegex = @".*(R-0)|(0-R)|(F-0)|(0-F)|(1-0)|(0-1)";
            string turnRegex = $"({placeRegex}|{moveRegex}) ({placeRegex}|{moveRegex})";

            Regex finalTurnRegex = new Regex($@"({placeRegex}|{moveRegex}) ({placeRegex}|{moveRegex})+ {endGameRegex}");

            //List<string> validEndGames = new List<string> { "R-0", "0-R", "F-0", "0-F", "1-0", "0-1", "1/2-1/2" };


            if (finalTurnRegex.IsMatch(stringToTest))
            {
                MatchCollection matches = finalTurnRegex.Matches(stringToTest);
                int y = 4;
            }
              

            //List<string> turnMoves = stringToTest.Split(' ').ToList();

            //if (endGameTurn)
            //{
            //    //make sure there's either one or two moves in final turn
            //    if (turnMoves.Count == 2 || turnMoves.Count == 3)
            //    { 
            //    }
            //}

            //string stoneLocation = @"[a-z]{{1}}[1-9]{{1}}";





        }
}

}
