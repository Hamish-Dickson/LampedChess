using LampedChess.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LampedChess
{
    public class Game
    {
        private Board currentBoard;
        public PlayingAs playingAs;

        public Game(PlayingAs colour)
        {
            playingAs = colour;
            currentBoard = new Board("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR", playingAs); //starting position
            Console.WriteLine(currentBoard.ToString());

            currentBoard.Update("e2e4", true);
            Console.WriteLine(currentBoard.ToString());
        }
    }
}
