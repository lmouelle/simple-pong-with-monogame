using Microsoft.Xna.Framework;

namespace project
{
    public interface IBallManager
    {
        Rectangle GetLocation();

        void Collide(PlayerKind playerKind);
    }
}
