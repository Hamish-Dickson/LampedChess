using LampedChess.Enumerations;

namespace LampedChess
{
    public class Move
    {
        public ulong from = 0;
        public ulong to = 0;
        public int flag = MoveFlag.NONE;
        public Piece promotionPiece = Piece.EMPTY;

        public string ToUCI()
        {
            string result = Utils.PositionBitBoards.GetSquareNameFromBitBoard(from) + Utils.PositionBitBoards.GetSquareNameFromBitBoard(to);
            return result;
        }
    }

    
}
