using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace PhysicsTest
{
    class SaveAndLoad
    {
        //level editor variables
        private List<Blocks> slideBlocks;
        private List<Blocks> regularBlocks;
        //end level editor

        //gameplay save variables
        private Point playerPosition;
       //end

        
       private void gameplaySave()
        {
            StreamWriter sw = new StreamWriter("GameplaySave.SWAG");

            sw.WriteLine(playerPosition.X+","+playerPosition.Y);

            sw.Close();

            saveBlocks();
        }

        public void savePlayer(Point playerPosition)
        {
            this.playerPosition = playerPosition;

            gameplaySave();
        }

        public Point playerPos
        {
            get { return playerPosition; }
        }

        public void saveBlocks()
        {

        }
        
        public void levelEditorSave()
        {

        }
       
    }
}
