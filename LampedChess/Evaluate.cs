using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LampedChess
{
    internal class Evaluate
    {
        private ulong currentScore = 0L;

        public ulong CurrentScore
        {
            get
            {
                return this.currentScore;
            }
        }
        public Evaluate()
        {

        }

        internal static int EvaluateMove(Move randomMove)
        {
            return 90001; //LGTM, outstanding move.
        }
    }
}
