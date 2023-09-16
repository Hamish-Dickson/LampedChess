using LampedChess.Enumerations;
using LampedChess.Utils;
using System.Diagnostics;

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

        private PlayingAs playingAs;
        public Board(string currentState, PlayingAs playingAs)
        {
            this.playingAs = playingAs;
            this.RefreshBoards();
        }

        public string Update(string move, bool opponentMove)
        {
            (Move, Piece) parsedMove = ParseMove(move);
            ProcessCapture(parsedMove);
            ProcessMove(parsedMove);
            this.RefreshBoards();
            if (opponentMove)
            {
                string ourResponse = GenerateMove(1000).ToUCI();

                Console.WriteLine("Opponent plays: " + move + " We play: " + ourResponse);
                this.Update(ourResponse, false);
                return ourResponse;
            }
            else
            {
                return"";
            }
        }

        private void ProcessCapture((Move, Piece) value)
        {
            Move madeMove = value.Item1;
            Piece piece = value.Item2;
            Piece capturedPiece = FindPieceEnemyCaptured(madeMove.to);

            //now we need to execute the move and update the bitboard to delete the captured piece.
            if (capturedPiece != Piece.EMPTY)
            {
                switch (capturedPiece)
                {
                    case Piece.BLACK_PAWN:
                        bpBitBoard &= ~madeMove.to;
                        break;
                    case Piece.BLACK_KNIGHT:
                        bnBitBoard &= ~madeMove.to;
                        break;
                    case Piece.BLACK_BISHOP:
                        bbBitBoard &= ~madeMove.to;
                        break;
                    case Piece.BLACK_ROOK:
                        brBitBoard &= ~madeMove.to;
                        break;
                    case Piece.BLACK_QUEEN:
                        bqBitBoard &= ~madeMove.to;
                        break;
                    case Piece.BLACK_KING:
                        bkBitBoard &= ~madeMove.to;
                        break;
                    case Piece.WHITE_PAWN:
                        wpBitBoard &= ~madeMove.to;
                        break;
                    case Piece.WHITE_KNIGHT:
                        wnBitBoard &= ~madeMove.to;
                        break;
                    case Piece.WHITE_BISHOP:
                        wbBitBoard &= ~madeMove.to;
                        break;
                    case Piece.WHITE_ROOK:
                        wrBitBoard &= ~madeMove.to;
                        break;
                    case Piece.WHITE_QUEEN:
                        wqBitBoard &= ~madeMove.to;
                        break;
                    case Piece.WHITE_KING:
                        wkBitBoard &= ~madeMove.to;
                        break;
                }
            }
        }

        private Piece FindPieceEnemyCaptured(ulong to)
        {
            //we have a ulong which represents the bitboard of the move just made, we need to see if any of our pieces occupy it.
            //if they do, we need to return the piece type.
            //if they don't, we need to return Piece.EMPTY
            //we can use the playingAs property to determine if we are looking for white or black pieces.
            //we can use the allPiecesBitBoard to see if any of our pieces occupy the square.
            if (playingAs == PlayingAs.BLACK)
            {
                if ((to & bpBitBoard) != 0)
                {
                    return Piece.BLACK_PAWN;
                }
                else if ((to & bnBitBoard) != 0)
                {
                    return Piece.BLACK_KNIGHT;
                }
                else if ((to & bbBitBoard) != 0)
                {
                    return Piece.BLACK_BISHOP;
                }
                else if ((to & brBitBoard) != 0)
                {
                    return Piece.BLACK_ROOK;
                }
                else if ((to & bqBitBoard) != 0)
                {
                    return Piece.BLACK_QUEEN;
                }
                else if ((to & bkBitBoard) != 0)
                {
                    return Piece.BLACK_KING;
                }
                else
                {
                    return Piece.EMPTY;
                }
            }
            else
            {
                if ((to & wpBitBoard) != 0)
                {
                    return Piece.WHITE_PAWN;
                }
                else if ((to & wnBitBoard) != 0)
                {
                    return Piece.WHITE_KNIGHT;
                }
                else if ((to & wbBitBoard) != 0)
                {
                    return Piece.WHITE_BISHOP;
                }
                else if ((to & wrBitBoard) != 0)
                {
                    return Piece.WHITE_ROOK;
                }
                else if ((to & wqBitBoard) != 0)
                {
                    return Piece.WHITE_QUEEN;
                }
                else if ((to & wkBitBoard) != 0)
                {
                    return Piece.WHITE_KING;
                }
                else
                {
                    return Piece.EMPTY;
                }
            }


        }

        private void ProcessMove((Move, Piece) value)
        {
            Move madeMove = value.Item1;
            Piece piece = value.Item2;
            switch (piece)
            {
                case Piece.WHITE_PAWN:
                    wpBitBoard ^= madeMove.from;
                    wpBitBoard ^= madeMove.to;
                    break;
                case Piece.WHITE_KNIGHT:
                    wnBitBoard ^= madeMove.from;
                    wnBitBoard ^= madeMove.to;
                    break;
                case Piece.WHITE_BISHOP:
                    wbBitBoard ^= madeMove.from;
                    wbBitBoard ^= madeMove.to;
                    break;
                case Piece.WHITE_ROOK:
                    wrBitBoard ^= madeMove.from;
                    wrBitBoard ^= madeMove.to;
                    break;
                case Piece.WHITE_QUEEN:
                    wqBitBoard ^= madeMove.from;
                    wqBitBoard ^= madeMove.to;
                    break;
                case Piece.WHITE_KING:
                    wkBitBoard ^= madeMove.from;
                    wkBitBoard ^= madeMove.to;
                    break;
                case Piece.BLACK_PAWN:
                    bpBitBoard ^= madeMove.from;
                    bpBitBoard ^= madeMove.to;
                    break;
                case Piece.BLACK_KNIGHT:
                    bnBitBoard ^= madeMove.from;
                    bnBitBoard ^= madeMove.to;
                    break;
                case Piece.BLACK_BISHOP:
                    bbBitBoard ^= madeMove.from;
                    bbBitBoard ^= madeMove.to;
                    break;
                case Piece.BLACK_ROOK:
                    brBitBoard ^= madeMove.from;
                    brBitBoard ^= madeMove.to;
                    break;
                case Piece.BLACK_QUEEN:
                    bqBitBoard ^= madeMove.from;
                    bqBitBoard ^= madeMove.to;
                    break;
                case Piece.BLACK_KING:
                    bkBitBoard ^= madeMove.from;
                    bkBitBoard ^= madeMove.to;
                    break;
            }
        }

        private (Move, Piece) ParseMove(string move)
        {
            string fromSquare = move.Substring(0, 2);
            string toSquare = move.Substring(2, 2);

            Piece movedPiece = FindPiece(fromSquare);
            Piece promotion = PromotionHelper.GetPromotionPiece(move, this.playingAs);

            Move madeMove = new Move()
            {
                from = PositionBitBoards.GetBitBoard(fromSquare),
                to = PositionBitBoards.GetBitBoard(toSquare),
                promotionPiece = promotion
            };

            return (madeMove, movedPiece);
        }

        private Move GenerateMove(long millisecondsToThink)
        {            
            Move bestMove = null;
            int bestScore = -100000;
            Stopwatch sw = new();
            sw.Start();
            while (sw.ElapsedMilliseconds < millisecondsToThink)
            {
                Move randomMove = GenerateRandomMove();
                int score = Evaluate.EvaluateMove(randomMove);
                if (score > bestScore)
                {
                    bestScore = score;
                    bestMove = randomMove;
                }
            }
            return bestMove;
        }//make a while loop that runs until the time is up.


        private List<Move> GenerateAllLegalMoves()
        {
            List<Move> move = new List<Move>();
            move.AddRange(GenerateAllPawnMoves());
            move.AddRange(GenerateAllKnightMoves());
            move.AddRange(GenerateAllBishopMoves());
            move.AddRange(GenerateAllRookMoves());
            move.AddRange(GenerateAllQueenMoves());
            move.AddRange(GenerateAllKingMoves());


            return move;
        }

        private IEnumerable<Move> GenerateAllKingMoves()
        {
            // generate all king moves
            // check if any of them are legal
            // if they are, add them to the list
            // return the list
            throw new NotImplementedException();

        }

        private IEnumerable<Move> GenerateAllQueenMoves()
        {
            // generate all queen moves
            // check if any of them are legal
            // if they are, add them to the list
            // return the list

            throw new NotImplementedException();
        }

        private IEnumerable<Move> GenerateAllRookMoves()
        {
            // generate all rook moves
            // check if any of them are legal
            // if they are, add them to the list
            // return the list

            throw new NotImplementedException();
        }

        private IEnumerable<Move> GenerateAllBishopMoves()
        {
            // generate all bishop moves
            // check if any of them are legal
            // if they are, add them to the list
            // return the list
            throw new NotImplementedException();
        }

        private IEnumerable<Move> GenerateAllKnightMoves()
        {
            // generate all knight moves
            // check if any of them are legal
            // if they are, add them to the list
            // return the list

            throw new NotImplementedException();
        }

        private IEnumerable<Move> GenerateAllPawnMoves()
        {
            // generate all pawn moves
            // check if any of them are legal
            // if they are, add them to the list
            // return the list

            throw new NotImplementedException();
        }

        private Move GenerateRandomMove()
        {
            // "opponent plays e2e4, we play c2b6" outstanding move
            var files = new[] {"a", "b", "c", "d", "e", "f", "g", "h"};
            var ranks = new[] {"1", "2", "3", "4", "5", "6", "7", "8"};

            Random random = new();
            var fromFile = files[random.Next(0, 8)];
            var fromRank = ranks[random.Next(0, 8)];
            var toFile = files[random.Next(0, 8)];
            var toRank = ranks[random.Next(0, 8)];

            return ParseMove(fromFile + fromRank + toFile + toRank).Item1;
        }


        public ulong GetKnightMoves(string square)
        {
            List<ulong> possibleMoves = new List<ulong>();
            ulong currentBitBoard = PositionBitBoards.GetBitBoard(square);

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

        public Piece FindPiece(string square)
        {
            // given a square, we need to find what piece occupies it.
            ulong currentBitBoard = Utils.PositionBitBoards.GetBitBoard(square);
            if ((wpBitBoard & currentBitBoard) != 0) return Piece.WHITE_PAWN;
            if ((wnBitBoard & currentBitBoard) != 0) return Piece.WHITE_KNIGHT;
            if ((wbBitBoard & currentBitBoard) != 0) return Piece.WHITE_BISHOP;
            if ((wrBitBoard & currentBitBoard) != 0) return Piece.WHITE_ROOK;
            if ((wqBitBoard & currentBitBoard) != 0) return Piece.WHITE_QUEEN;
            if ((wkBitBoard & currentBitBoard) != 0) return Piece.WHITE_KING;
            if ((bpBitBoard & currentBitBoard) != 0) return Piece.BLACK_PAWN;
            if ((bnBitBoard & currentBitBoard) != 0) return Piece.BLACK_KNIGHT;
            if ((bbBitBoard & currentBitBoard) != 0) return Piece.BLACK_BISHOP;
            if ((brBitBoard & currentBitBoard) != 0) return Piece.BLACK_ROOK;
            if ((bqBitBoard & currentBitBoard) != 0) return Piece.BLACK_QUEEN;
            if ((bkBitBoard & currentBitBoard) != 0) return Piece.BLACK_KING;
            //Console.WriteLine("No piece found at " + square + "🤔");
            return Piece.EMPTY;

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


        public override string ToString()
        {
            // i need to display all of my pieces in the console. We need to write backwards to show white at the bottom

            string board = "";
            for (int i = 7; i >= 0; i--)
            {
                for (int j = 0; j < 8; j++)
                {
                    ulong currentBitBoard = 0b1;
                    currentBitBoard = currentBitBoard << ((i * 8) + j);
                    if ((wpBitBoard & currentBitBoard) != 0) board += "P";
                    else if ((wnBitBoard & currentBitBoard) != 0) board += "N";
                    else if ((wbBitBoard & currentBitBoard) != 0) board += "B";
                    else if ((wrBitBoard & currentBitBoard) != 0) board += "R";
                    else if ((wqBitBoard & currentBitBoard) != 0) board += "Q";
                    else if ((wkBitBoard & currentBitBoard) != 0) board += "K";
                    else if ((bpBitBoard & currentBitBoard) != 0) board += "p";
                    else if ((bnBitBoard & currentBitBoard) != 0) board += "n";
                    else if ((bbBitBoard & currentBitBoard) != 0) board += "b";
                    else if ((brBitBoard & currentBitBoard) != 0) board += "r";
                    else if ((bqBitBoard & currentBitBoard) != 0) board += "q";
                    else if ((bkBitBoard & currentBitBoard) != 0) board += "k";
                    else board += ".";
                }
                board += "\n";
            }
            return board;

        }
    }
}
