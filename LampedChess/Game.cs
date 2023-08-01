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
        private PlayingAs playingAs;

        public Game(PlayingAs colour)
        {
            playingAs = colour;
            currentBoard = new Board("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR"); //starting position
            Console.WriteLine("Current state: " + Convert.ToString((long)currentBoard.GetAllPieces(), 2));
            Console.WriteLine("White pieces: " + Convert.ToString((long)currentBoard.GetWhitePieces(), 2));
            Console.WriteLine("Black pieces: " + Convert.ToString((long)currentBoard.GetBlackPieces(), 2));

            currentBoard.Update("e2e4", true);
            Console.WriteLine("Current state: " + Convert.ToString((long)currentBoard.GetAllPieces(), 2));
            Console.WriteLine("White pieces: " + Convert.ToString((long)currentBoard.GetWhitePieces(), 2));
            Console.WriteLine("Black pieces: " + Convert.ToString((long)currentBoard.GetBlackPieces(), 2));
        }
    }
}
