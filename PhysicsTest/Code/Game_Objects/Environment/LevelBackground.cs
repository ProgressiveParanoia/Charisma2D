using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ParanoidGames.Charisma2D
{
    class LevelBackground : GameObject
    {

        public override void Initialize()
        {
            base.Initialize();

            this.textureName = "GameBackground";
        }
    }
}
