using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace ParanoidGames.Charisma2D.Utilities
{
    class CollisionManager : IManager
    {
        #region singleton implementation
        private static CollisionManager instance;
        public static CollisionManager Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new CollisionManager();
                }
                return instance;
            }
        }
        #endregion

        public event Action<GameTime, GameObject> OnCollisionDetected;

        public void Initialize(ContentManager content)
        {
            
        }

        public void Update(GameTime gameTime, GameObject collidingObject)
        {
            if (OnCollisionDetected == null)
            {
                return;
            }

            this.OnCollisionDetected(gameTime, collidingObject);
        }
    }
}
