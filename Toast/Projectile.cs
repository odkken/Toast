using System;
using SFML.Graphics;
using SFML.System;

namespace Toast
{
    public class Projectile : GameObjectBase
    {

        public void SetVelocity(Vector2f vel)
        {
            Velocity = vel;
        }

        public override void Update()
        {
            Position += Velocity * Environment.FrameDelta;
        }
    }
}