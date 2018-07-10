using Microsoft.Xna.Framework;

namespace ParanoidGames.Charisma2D.Utilities
{
    public static class RectangleCollider
    {
        public static bool TouchTopOf(Rectangle r1, Rectangle r2)
        {
            return (r1.Bottom >= r2.Top - 1 &&
                    r1.Bottom <= r2.Top + (r2.Height / 2) &&
                    r1.Right >= r2.Left + r2.Width / 5 &&
                    r1.Left <= r2.Right - r2.Width / 5);
        }

        public static bool TouchBottomOf(Rectangle r1, Rectangle r2)
        {
            return (r1.Top <= r2.Bottom + (r2.Height / 5) &&
                    r1.Top >= r2.Bottom - 1 &&
                    r1.Right >= r2.Left + (r2.Width / 5) &&
                    r1.Left <= r2.Right - (r2.Width / 5));
        }

        public static bool TouchLeftOf(Rectangle r1, Rectangle r2)
        {
            return (r1.Right <= r2.Right &&
                    r1.Right >= r2.Right - 5 &&
                    r1.Top <= r2.Bottom - (r2.Width / 4) &&
                    r1.Bottom >= r2.Top + (r2.Width / 4));
        }

        public static bool TouchRightOf(Rectangle r1, Rectangle r2)
        {
            return (r1.Left <= r2.Left - 5 &&
                    r1.Left >= r2.Left - 64 &&
                    r1.Top <= r2.Bottom - (r2.Width / 4) &&
                    r1.Bottom >= r2.Top + (r2.Width / 4));
        }
    }
}
