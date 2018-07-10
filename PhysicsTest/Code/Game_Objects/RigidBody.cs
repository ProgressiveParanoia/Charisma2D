using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using ParanoidGames.Charisma2D.Utilities;

namespace ParanoidGames.Charisma2D
{
    class RigidBody : GameObject
    {
        private Point velocity;
        private bool Fall = true;

        public RigidBody(Rectangle rect, string name) : base(rect, name)
        {
            CollisionManager.Instance.OnCollisionDetected += this.CollisionUpdate;
        }

        protected virtual void CollisionUpdate(GameTime gameTime, GameObject collidingObject)
        {
            if (this.Rectangle.Intersects(collidingObject.Rectangle))
            {
                //if (collidingObject is RigidBody == false || collidingObject == null || collidingObject == this || collidingObject is Block == false)
                //    return;
                
                if (collidingObject is Block)
                {
                    Block collidingBlock = collidingObject as Block;

                    if (collidingBlock.PlatformerBlockType != PlatformerBlockType.Unidentified)
                    {
                        this.PlatformerBlockCollision(collidingBlock);
                    }
                    else
                            if (collidingBlock.GenericBlockType != GenericBlockType.Unidentified)
                    {
                        this.GenericBlockCollision(collidingBlock);
                    }
                }
            }

            this.PhysicsUpdate(gameTime);
        }

        private void PhysicsUpdate(GameTime gameTime)
        {
            if (Fall)
            {
                velocity.Y += 1;


                this.Move(this.Position.X + velocity.X, this.Position.Y + 1);
            }
        }

        private void PlatformerBlockCollision(Block collidingBlock)
        {
            this.velocity.Y = 0;

            //if (Math.Abs(this.Position.Y - collidingBlock.Position.Y) < this.Rectangle.Height)
            //{
            //    this.Move(this.Position.X, this.Position.Y - 1);
            //}
            //if (RectangleCollider.TouchTopOf(this.Rectangle, collidingBlock.Rectangle))
            //{
            //    this.velocity.Y = 0;

            //    if(Math.Abs(this.Position.Y - collidingBlock.Position.Y) < this.Rectangle.Height)
            //    {
            //        this.Move(this.Position.X, this.Position.Y - 1);
            //    }

            //}
            Fall = false;
        }

        private void GenericBlockCollision(Block collidingBlock)
        {

        }
    }
}
