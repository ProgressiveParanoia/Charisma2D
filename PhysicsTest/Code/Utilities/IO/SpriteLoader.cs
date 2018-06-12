using System;
using System.Collections.Generic;

namespace ParanoidGames.Utilities.IO
{
    class SpriteLoader : Loader
    {
        public void CheckDirectories()
        {
            string[] test = this.RecurseGetFilePaths(@"Content\Sprites\Environment");

            foreach(string t in test)
            {
                Console.WriteLine(t);
            }
        }
    }
}
