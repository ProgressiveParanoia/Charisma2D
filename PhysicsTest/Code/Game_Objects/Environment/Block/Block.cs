using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace ParanoidGames.Charisma2D
{

    public enum PlatformerBlockType
    {
        Unidentified,
        Regular,
        Slippery,
        Damage,
        Water
    }

    /// <summary>
    /// Generic types for other 2D games
    /// </summary>
    public enum GenericBlockType
    {
        Unidentified,
        EmptySpace,
        OccupiedSpace
    }

    class Block : Environment
    {
        public PlatformerBlockType PlatformerBlockType
        {
            private set;
            get;
        }

        public GenericBlockType GenericBlockType
        {
            private set;
            get;
        }

        /// <summary>
        /// Constructor used for platformer games
        /// </summary>
        /// <param name="rect">Bounding rectangle of the object</param>
        /// <param name="name">Name key used for texture loading and as a unique identifier</param>
        /// <param name="platformerBlockType">Type of block to be used</param>
        public Block(Rectangle rect, string name, PlatformerBlockType platformerBlockType) : base(rect, name)
        {
            this.PlatformerBlockType = platformerBlockType;
        }

        /// <summary>
        /// Constructor used for other types of 2D games
        /// </summary>
        /// <param name="rect">Bounding rectangle of the object</param>
        /// <param name="name">Name key used for texture loading and as a unique identifier</param>
        /// <param name="genericBlockType">Type of block to be used</param>
        public Block(Rectangle rect, string name, GenericBlockType genericBlockType) : base(rect, name)
        {
            this.GenericBlockType = genericBlockType;
        }
    }
}
