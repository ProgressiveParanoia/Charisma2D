using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using ParanoidGames.Charisma2D.Utilities;

namespace ParanoidGames.Charisma2D
{
    class RigidBody : GameObject
    {
        private GameObject previousCollidedObject;
        private Vector2 potentialVelocity;
        private Vector2 velocity;

        public Vector2 Velocity
        {
            get { return this.velocity; }
        }

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

                    this.previousCollidedObject = collidingBlock;

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
                else
                    this.previousCollidedObject = null;
            }else
            {
                if (collidingObject is Block)
                {
                    Block collidingBlock = collidingObject as Block;

                    if (RectangleCollider.TouchTopOf(this.Rectangle, collidingBlock.Rectangle) == false)
                    {
                        if(previousCollidedObject is Block)
                            velocity = Vector2.Zero;

                       // this.velocity.Y -= 1f;
                        Fall = true;
                    }
                }
            }

            this.PhysicsUpdate(gameTime);
        }

        private void PhysicsUpdate(GameTime gameTime)
        {
            if (Fall)
            {
                velocity.Y += 0.1f;

                this.Move((int)(this.Position.X + velocity.X), (int)(this.Position.Y + velocity.Y));
            }
        }

        private void PlatformerBlockCollision(Block collidingBlock)
        {
            if(this.velocity != Vector2.Zero)
            {
                this.potentialVelocity.Y = this.velocity.Y / 4;
            }

            if (Math.Abs(this.Position.Y - collidingBlock.Position.Y) < this.Rectangle.Height)
            {
                this.Move((int)(this.Position.X), (int)(this.Position.Y - potentialVelocity.Y));
            }

            if(Math.Abs(this.Position.Y - collidingBlock.Position.Y) >= this.Rectangle.Height)
            {
                velocity.Y = 0;
            }

            if (RectangleCollider.TouchTopOf(this.Rectangle, collidingBlock.Rectangle))
            {
                //this.velocity.Y = 1;
                Fall = false;
            }

            Console.WriteLine("Collision values:"+ (collidingBlock.Position.Y - (this.Position.Y - collidingBlock.Position.Y)));
            Console.WriteLine("Velocity:" + this.velocity);
        }

        private void GenericBlockCollision(Block collidingBlock)
        {

        }

        private bool StuckOnTop(GameObject collidingObject)
        {
            return Math.Abs(this.Position.Y - collidingObject.Position.Y) >= this.Rectangle.Height;
        }
    }
}
