using System.Collections;

namespace LampedChess.Utils
{
    /// <summary>
    /// This class is disgusting, I'm sorry.
    /// </summary>
    public static class PositionBitBoards
    {
        private static readonly ulong a1 = 0b1;
        private static readonly ulong b1 = a1 << 1;
        private static readonly ulong c1 = b1 << 1;
        private static readonly ulong d1 = c1 << 1;
        private static readonly ulong e1 = d1 << 1;
        private static readonly ulong f1 = e1 << 1;
        private static readonly ulong g1 = f1 << 1;
        private static readonly ulong h1 = g1 << 1;

        private static readonly ulong a2 = h1 << 1;
        private static readonly ulong b2 = a2 << 1;
        private static readonly ulong c2 = b2 << 1;
        private static readonly ulong d2 = c2 << 1;
        private static readonly ulong e2 = d2 << 1;
        private static readonly ulong f2 = e2 << 1;
        private static readonly ulong g2 = f2 << 1;
        private static readonly ulong h2 = g2 << 1;

        private static readonly ulong a3 = h2 << 1;
        private static readonly ulong b3 = a3 << 1;
        private static readonly ulong c3 = b3 << 1;
        private static readonly ulong d3 = c3 << 1;
        private static readonly ulong e3 = d3 << 1;
        private static readonly ulong f3 = e3 << 1;
        private static readonly ulong g3 = f3 << 1;
        private static readonly ulong h3 = g3 << 1;

        private static readonly ulong a4 = h3 << 1;
        private static readonly ulong b4 = a4 << 1;
        private static readonly ulong c4 = b4 << 1;
        private static readonly ulong d4 = c4 << 1;
        private static readonly ulong e4 = d4 << 1;
        private static readonly ulong f4 = e4 << 1;
        private static readonly ulong g4 = f4 << 1;
        private static readonly ulong h4 = g4 << 1;

        private static readonly ulong a5 = h4 << 1;
        private static readonly ulong b5 = a5 << 1;
        private static readonly ulong c5 = b5 << 1;
        private static readonly ulong d5 = c5 << 1;
        private static readonly ulong e5 = d5 << 1;
        private static readonly ulong f5 = e5 << 1;
        private static readonly ulong g5 = f5 << 1;
        private static readonly ulong h5 = g5 << 1;

        private static readonly ulong a6 = h5 << 1;
        private static readonly ulong b6 = a6 << 1;
        private static readonly ulong c6 = b6 << 1;
        private static readonly ulong d6 = c6 << 1;
        private static readonly ulong e6 = d6 << 1;
        private static readonly ulong f6 = e6 << 1;
        private static readonly ulong g6 = f6 << 1;
        private static readonly ulong h6 = g6 << 1;

        private static readonly ulong a7 = h6 << 1;
        private static readonly ulong b7 = a7 << 1;
        private static readonly ulong c7 = b7 << 1;
        private static readonly ulong d7 = c7 << 1;
        private static readonly ulong e7 = d7 << 1;
        private static readonly ulong f7 = e7 << 1;
        private static readonly ulong g7 = f7 << 1;
        private static readonly ulong h7 = g7 << 1;
        
        private static readonly ulong a8 = h7 << 1;
        private static readonly ulong b8 = a8 << 1;
        private static readonly ulong c8 = b8 << 1;
        private static readonly ulong d8 = c8 << 1;
        private static readonly ulong e8 = d8 << 1;
        private static readonly ulong f8 = e8 << 1;
        private static readonly ulong g8 = f8 << 1;
        private static readonly ulong h8 = g8 << 1;

        public static ulong GetBitBoard(string square)
        {
            switch (square)
            {
                case "a1": return a1;
                case "b1": return b1;
                case "c1": return c1;
                case "d1": return d1;
                case "e1": return e1;
                case "f1": return f1;
                case "g1": return g1;
                case "h1": return h1;

                case "a2": return a2;
                case "b2": return b2;
                case "c2": return c2;
                case "d2": return d2;
                case "e2": return e2;
                case "f2": return f2;
                case "g2": return g2;
                case "h2": return h2;

                case "a3": return a3;
                case "b3": return b3;
                case "c3": return c3;
                case "d3": return d3;
                case "e3": return e3;
                case "f3": return f3;
                case "g3": return g3;
                case "h3": return h3;

                case "a4": return a4;
                case "b4": return b4;
                case "c4": return c4;
                case "d4": return d4;
                case "e4": return e4;
                case "f4": return f4;
                case "g4": return g4;
                case "h4": return h4;

                case "a5": return a5;
                case "b5": return b5;
                case "c5": return c5;
                case "d5": return d5;
                case "e5": return e5;
                case "f5": return f5;
                case "g5": return g5;
                case "h5": return h5;

                case "a6": return a6;
                case "b6": return b6;
                case "c6": return c6;
                case "d6": return d6;
                case "e6": return e6;
                case "f6": return f6;
                case "g6": return g6;
                case "h6": return h6;

                case "a7": return a7;
                case "b7": return b7;
                case "c7": return c7;
                case "d7": return d7;
                case "e7": return e7;
                case "f7": return f7;
                case "g7": return g7;
                case "h7": return h7;
                
                case "a8": return a8;
                case "b8": return b8;
                case "c8": return c8;
                case "d8": return d8;
                case "e8": return e8;
                case "f8": return f8;
                case "g8": return g8;
                case "h8": return h8;
                default: return 0;
            }
        }

        public static List<string> GetSquareNamesFromBitBoard(ulong board)
        {
            // There is 0 shot this method is fast, but i cant think of a better way????
            List<string> result = new List<string>();
            if ((board & a1) != 0) result.Add("a1");
            if ((board & b1) != 0) result.Add("b1");
            if ((board & c1) != 0) result.Add("c1");
            if ((board & d1) != 0) result.Add("d1");
            if ((board & e1) != 0) result.Add("e1");
            if ((board & f1) != 0) result.Add("f1");
            if ((board & g1) != 0) result.Add("g1");
            if ((board & h1) != 0) result.Add("h1");

            if ((board & a2) != 0) result.Add("a2");
            if ((board & b2) != 0) result.Add("b2");
            if ((board & c2) != 0) result.Add("c2");
            if ((board & d2) != 0) result.Add("d2");
            if ((board & e2) != 0) result.Add("e2");
            if ((board & f2) != 0) result.Add("f2");
            if ((board & g2) != 0) result.Add("g2");
            if ((board & h2) != 0) result.Add("h2");

            if ((board & a3) != 0) result.Add("a3");
            if ((board & b3) != 0) result.Add("b3");
            if ((board & c3) != 0) result.Add("c3");
            if ((board & d3) != 0) result.Add("d3");
            if ((board & e3) != 0) result.Add("e3");
            if ((board & f3) != 0) result.Add("f3");
            if ((board & g3) != 0) result.Add("g3");
            if ((board & h3) != 0) result.Add("h3");

            if ((board & a4) != 0) result.Add("a4");
            if ((board & b4) != 0) result.Add("b4");
            if ((board & c4) != 0) result.Add("c4");
            if ((board & d4) != 0) result.Add("d4");
            if ((board & e4) != 0) result.Add("e4");
            if ((board & f4) != 0) result.Add("f4");
            if ((board & g4) != 0) result.Add("g4");
            if ((board & h4) != 0) result.Add("h4");

            if ((board & a5) != 0) result.Add("a5");
            if ((board & b5) != 0) result.Add("b5");
            if ((board & c5) != 0) result.Add("c5");
            if ((board & d5) != 0) result.Add("d5");
            if ((board & e5) != 0) result.Add("e5");
            if ((board & f5) != 0) result.Add("f5");
            if ((board & g5) != 0) result.Add("g5");
            if ((board & h5) != 0) result.Add("h5");

            if ((board & a6) != 0) result.Add("a6");
            if ((board & b6) != 0) result.Add("b6");
            if ((board & c6) != 0) result.Add("c6");
            if ((board & d6) != 0) result.Add("d6");
            if ((board & e6) != 0) result.Add("e6");
            if ((board & f6) != 0) result.Add("f6");
            if ((board & g6) != 0) result.Add("g6");
            if ((board & h6) != 0) result.Add("h6");

            if ((board & a7) != 0) result.Add("a7");
            if ((board & b7) != 0) result.Add("b7");
            if ((board & c7) != 0) result.Add("c7");
            if ((board & d7) != 0) result.Add("d7");
            if ((board & e7) != 0) result.Add("e7");
            if ((board & f7) != 0) result.Add("f7");
            if ((board & g7) != 0) result.Add("g7");
            if ((board & h7) != 0) result.Add("h7");

            if ((board & a8) != 0) result.Add("a8");
            if ((board & b8) != 0) result.Add("b8");
            if ((board & c8) != 0) result.Add("c8");
            if ((board & d8) != 0) result.Add("d8");
            if ((board & e8) != 0) result.Add("e8");
            if ((board & f8) != 0) result.Add("f8");
            if ((board & g8) != 0) result.Add("g8");
            if ((board & h8) != 0) result.Add("h8");

            return result;
        }
    }
}
