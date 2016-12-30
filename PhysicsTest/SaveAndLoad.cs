using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PhysicsTest
{
    class SaveAndLoad
    {
        //level editor variables
        private List<Blocks> slideBlocks;
        private List<Blocks> regularBlocks;

        private List<Player> playerList;

        private Player _player;
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

            playerList = new List<Player>();

            if(playerList.Count == 0)
            {
                playerList.Add(_player);
            }

            gameplaySave();
        }

        public void loadPlayer()
        {
          
            foreach (Player p in playerList)
            {
                playerList.Remove(p);
               
                break;
            }
            
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
