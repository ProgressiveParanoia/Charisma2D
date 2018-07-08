using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ParanoidGames.Charisma2D
{
    interface IGameObject
    {
        void Update(GameTime gameTime);
        void Draw(GameTime gameTime, SpriteBatch spriteBatch);
    }
}
