using System.Linq;
using SFML.Graphics;
using SFML.System;

namespace Toast
{
    public class Enemy : GameObjectBase
    {
        private GameObjectBase _player;
        private GameObjectBase Player
        {
            get { return _player ?? (_player = Environment.GameObjects.First(a => a is Player)); }
        }

        public Enemy(IEnvironment environment) : base(new CircleShape(15) { FillColor = Color.Magenta }, environment)
        {
        }

        public override void Update()
        {
            Velocity = new Vector2f();
            var delta = Player.Position - Position;
            if (delta.SquaredMagnitude() < .75)
                return;
            Velocity = delta.Normalize() * Speed;
            Position += Velocity * Environment.FrameDelta;
        }

        public float Speed => 55.0f;
    }
}