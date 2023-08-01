using LampedChess.Enumerations;
using LampedChess.Utils;

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

        public void Update(string move, bool opponentMove)
        {
            (Move madeMove, Piece piece) = ParseMove(move);

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
            this.RefreshBoards();
            if (opponentMove)
            {
                string ourResponse = GenerateMove().ToUCI();

                Console.WriteLine("Opponent plays: " + move + " We play: " + ourResponse);
                Console.WriteLine("State before response:");
                Console.WriteLine(this.ToString());
                this.Update(ourResponse, false);
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
                from = Utils.PositionBitBoards.GetBitBoard(fromSquare),
                to = Utils.PositionBitBoards.GetBitBoard(toSquare),
                promotionPiece = promotion
            };

            return (madeMove, movedPiece);
        }

        private Move GenerateMove()
        {
            return new Move()
            {
                from = PositionBitBoards.GetBitBoard("b8"),
                to = PositionBitBoards.GetBitBoard("c6"),
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
            Console.WriteLine("No piece found at " + square + "🤔");
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
