namespace LampedChess.Utils
{
    using LampedChess.Enumerations;
    public class PromotionHelper
    {
        public static Piece GetPromotionPiece(string move, PlayingAs colour)
        {
            Piece result = Piece.EMPTY;
            if (move.Length == 5)
            {
                if (colour == PlayingAs.WHITE)
                {
                    switch (move[4])
                    {
                        case 'q':
                            result = Piece.WHITE_QUEEN;
                            break;
                        case 'r':
                            result = Piece.WHITE_ROOK;
                            break;
                        case 'b':
                            result = Piece.WHITE_BISHOP;
                            break;
                        case 'n':
                            result = Piece.WHITE_KNIGHT;
                            break;
                    }
                }
                else
                {
                    switch (move[4])
                    {
                        case 'q':
                            result = Piece.BLACK_QUEEN;
                            break;
                        case 'r':
                            result = Piece.BLACK_ROOK;
                            break;
                        case 'b':
                            result = Piece.BLACK_BISHOP;
                            break;
                        case 'n':
                            result = Piece.BLACK_KNIGHT;
                            break;
                    }
                }
            }
            return result;
        }
    }
}
