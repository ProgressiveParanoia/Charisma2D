using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParanoidGames.Constants
{
    public static class FileDirectory
    {
        /// <summary>
        /// Contains the first subdirectory the sprite directory.
        /// </summary>
        private static List<string> spriteDirectories;
        public static void Initialize()
        {
            spriteDirectories = new List<string>();

            spriteDirectories.Add(Sprites_Environment);
            spriteDirectories.Add(Sprites_Enemy);
            spriteDirectories.Add(Sprites_Player);
            spriteDirectories.Add(Sprites_UI);
        }

        public static List<string> GetSpriteDirectories
        {
            get { return spriteDirectories; }
        }

        public const string Content = @"Content\";

        public const string Sprites_Environment = @"Sprites\Environment";
        public const string Sprites_Enemy = @"Sprites\Enemy";
        public const string Sprites_Player = @"Sprites\Player";
        public const string Sprites_UI = @"Sprites\UI";



        public const string Environment_Platform = @"Sprites\Environment\Platform\";
    }
}
