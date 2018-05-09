using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhysicsTest
{
    class ScoreTracker
    {
        private String name;
        private int score;
        public ScoreTracker(String name, int score)
        {
            this.name = name;
            this.score = score;
        }

        public string Name
        {
            get { return name; }
        }

        public int Score
        {
            get { return score; }
        }
    }
}
