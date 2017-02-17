using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TakAI;
using TakAI.Models;

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
            string stoneType;

            List<char> directionList = new List<char>{'>', '<', '+', '-'}; 
            Board board = new Board(4);
            char[] alphabet = Enumerable.Range(97, 26).Select(x => (char)x).ToArray();
            int sideLength = board.SideLength;
            char maxAlpha = alphabet[sideLength - 1];
            int maxNum = sideLength;
            string turn = "3c4>12";



            Regex moveStoneRegex = new Regex($"([0-9]*)([a-{maxAlpha}]{{1}}[1-{maxNum}]{{1}})([><+-]{{1}})([1-9]*)([FSC]*)");
            Assert.IsTrue(moveStoneRegex.IsMatch(turn));
            if (moveStoneRegex.IsMatch(turn))
            {
                MatchCollection matches = moveStoneRegex.Matches(turn);
                count = Int32.Parse(matches[0].Groups[1].ToString());
                boardSquare = matches[0].Groups[2].ToString();
                direction = matches[0].Groups[3].ToString();
                dropCounts = matches[0].Groups[4].ToString();
                stoneType = matches[0].Groups[5].ToString();
            }

            int p = 4;
        }


    }

}
