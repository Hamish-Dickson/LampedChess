using LampedChess.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LampedChess
{
    internal class Board
    {
        //bitboards for the pieces. these are starting values.
        ulong emptyBitBoard = ulong.MinValue;
        ulong wpBitBoard = 0b1111111100000000;
        ulong wnBitBoard = 0b1000010;
        ulong wbBitBoard = 0b100100;
        ulong wrBitBoard = 0b10000001;
        ulong wqBitBoard = 0b1000;
        ulong wkBitBoard = 0b10000;
        ulong bpBitBoard = 0b11111111000000000000000000000000000000000000000000000000;
        ulong bnBitBoard = 0b100001000000000000000000000000000000000000000000000000000000000;
        ulong bbBitBoard = 0b10010000000000000000000000000000000000000000000000000000000000;
        ulong brBitBoard = 0b1000000100000000000000000000000000000000000000000000000000000000;
        ulong bqBitBoard = 0b100000000000000000000000000000000000000000000000000000000000;
        ulong bkBitBoard = 0b1000000000000000000000000000000000000000000000000000000000000;
        ulong wPiecesBitBoard;
        ulong bPiecesBitBoard;
        ulong allPiecesBitBoard;
        public Board(string currentState)
        {
            this.RefreshBoards();
        }

        public void Update(string move, bool opponentMove)
        {
            (ulong moveFrom, ulong moveTo, Pieces piece) = ParseMove(move);

            switch (piece)
            {
                case Pieces.WHITE_PAWN:
                    wpBitBoard ^= moveFrom;
                    wpBitBoard ^= moveTo;
                    break;
                case Pieces.WHITE_KNIGHT:
                    wnBitBoard ^= moveFrom;
                    wnBitBoard ^= moveTo;
                    break;
                case Pieces.WHITE_BISHOP:
                    wbBitBoard ^= moveFrom;
                    wbBitBoard ^= moveTo;
                    break;
                case Pieces.WHITE_ROOK:
                    wrBitBoard ^= moveFrom;
                    wrBitBoard ^= moveTo;
                    break;
                case Pieces.WHITE_QUEEN:
                    wqBitBoard ^= moveFrom;
                    wqBitBoard ^= moveTo;
                    break;
                case Pieces.WHITE_KING:
                    wkBitBoard ^= moveFrom;
                    wkBitBoard ^= moveTo;
                    break;
                case Pieces.BLACK_PAWN:
                    bpBitBoard ^= moveFrom;
                    bpBitBoard ^= moveTo;
                    break;
                case Pieces.BLACK_KNIGHT:
                    bnBitBoard ^= moveFrom;
                    bnBitBoard ^= moveTo;
                    break;
                case Pieces.BLACK_BISHOP:
                    bbBitBoard ^= moveFrom;
                    bbBitBoard ^= moveTo;
                    break;
                case Pieces.BLACK_ROOK:
                    brBitBoard ^= moveFrom;
                    brBitBoard ^= moveTo;
                    break;
                case Pieces.BLACK_QUEEN:
                    bqBitBoard ^= moveFrom;
                    bqBitBoard ^= moveTo;
                    break;
                case Pieces.BLACK_KING:
                    bkBitBoard ^= moveFrom;
                    bkBitBoard ^= moveTo;
                    break;
            }
            this.RefreshBoards();
            if(opponentMove)
            {
                string ourResponse = GenerateMove().ToUCI();

                Console.WriteLine("Opponent plays: " + move + " We play: " + ourResponse);
                this.Update(ourResponse, false);
            }
        }
        
        private (ulong, ulong, Pieces) ParseMove(string move)
        {
            string fromSquare = move.Substring(0, 2);
            string toSquare = move.Substring(2, 2);
            return (Utils.PositionBitBoards.GetBitBoard(fromSquare), Utils.PositionBitBoards.GetBitBoard(toSquare), Pieces.WHITE_PAWN);
        }

        private Move GenerateMove()
        {
            return new Move()
            {
                from = Utils.PositionBitBoards.GetBitBoard("e7"),
                to = Utils.PositionBitBoards.GetBitBoard("e5"),
            };
        }

        
        public ulong GetKnightMoves(string square)
        {
            List<ulong> possibleMoves = new List<ulong>();
            ulong currentBitBoard = Utils.PositionBitBoards.GetBitBoard(square);

            possibleMoves.Add(currentBitBoard << 17);
            possibleMoves.Add(currentBitBoard << 15);
            possibleMoves.Add(currentBitBoard << 10);
            possibleMoves.Add(currentBitBoard << 6);
            possibleMoves.Add(currentBitBoard >> 6);
            possibleMoves.Add(currentBitBoard >> 10);
            possibleMoves.Add(currentBitBoard >> 15);
            possibleMoves.Add(currentBitBoard >> 17);

            foreach (ulong attack in possibleMoves)
            {
                currentBitBoard |= attack;
            }
            return currentBitBoard;
        }

        public ulong GetRookMoves(string square)
        {
            List<ulong> possibleMoves = new List<ulong>();
            ulong currentBitBoard = Utils.PositionBitBoards.GetBitBoard(square);

            return currentBitBoard;
        }

        private void RefreshBoards()
        {
            wPiecesBitBoard = 0b0;
            wPiecesBitBoard = wpBitBoard | wnBitBoard | wbBitBoard | wrBitBoard | wqBitBoard | wkBitBoard;
            bPiecesBitBoard = 0b0;
            bPiecesBitBoard = bpBitBoard | bnBitBoard | bbBitBoard | brBitBoard | bqBitBoard | bkBitBoard;
            allPiecesBitBoard = 0b0;
            allPiecesBitBoard = wPiecesBitBoard | bPiecesBitBoard;
        }
        public ulong GetWhitePieces()
        {
            return wPiecesBitBoard;
        }

        public ulong GetBlackPieces()
        {
            return bPiecesBitBoard;
        }

        public ulong GetAllPieces()
        {
            return allPiecesBitBoard;
        }
    }
}
