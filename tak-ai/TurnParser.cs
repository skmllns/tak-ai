using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TakAI.Models;

namespace TakAI.Services
{
    public class TurnParser
    {
        //TODO: this only works for placing stones!
      
        public void ParseTurn(Board board, string turn)
        {
            List<char> alphabet = Enumerable.Range(97, 26).Select(x => (char)x).ToList();
            int sideLength = board.SideLength;
            char maxAlpha = alphabet[sideLength - 1];
            int maxNum = sideLength;

            // Placing stones: (stone*)(square*) [default is F]  
            Regex placeStoneRegex = new Regex($@"[FSC]{{0,1}}[a-{maxAlpha}]{{1}}[1-{maxNum}]{{1}}");
            if (placeStoneRegex.IsMatch(turn))
            {
                MatchCollection matches = placeStoneRegex.Matches(turn);

            }

            // Moving stones: (count)(square*)(direction*)(drop counts**)(stone) 

        }

    }



}