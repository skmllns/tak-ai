using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using TakAI.Models;

namespace TakAI.Services
{
    public static class TurnService
    {
        public static List<string> GetTurnFile()
        {
            List<string> movesList = new List<string>();

            Console.WriteLine("file name?");
            string fileName = Console.ReadLine();
            if (fileName != null)
            {
                string ptnFile = string.Join(@"\n", File.ReadAllLines(fileName,
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
                Regex movesRegex = new Regex(@"(?<=1\.).*?");

                //TODO null handling w/poorly formed PTN file
                date = dateRegex.Matches(ptnFile)[0].Groups[0].ToString();
                playerOne = playerOneRegex.Matches(ptnFile)[0].Groups[0].ToString();
                playerTwo = playerTwoRegex.Matches(ptnFile)[0].Groups[0].ToString();
                boardSize = int.Parse(boardSizeRegex.Matches(ptnFile)[0].Groups[0].ToString());
                movesString = movesRegex.Matches(ptnFile)[0].Groups[0].ToString();

                movesList = Regex.Split(movesString, @"\\n").ToList();
            }


            return movesList;

        }

        public static void GetTurnInteractive()
        {

        }


    }
}