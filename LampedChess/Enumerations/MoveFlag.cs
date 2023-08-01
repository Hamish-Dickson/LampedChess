namespace LampedChess.Enumerations
{
    public class MoveFlag
    {
        public const int NONE = 0;
        public const int PAWN_START = 1;
        public const int PAWN_DOUBLE = 2;
        public const int EN_PASSANT = 4;
        public const int CASTLE = 8;
        public const int PROMOTION = 16;
        public const int CAPTURE = 32;
    }
}
