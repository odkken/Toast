using System;
using System.Collections.Generic;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Toast
{
    public class Player : GameObjectBase
    {
        private readonly Dictionary<Keyboard.Key, Action> _keyBindings;
        private const float Speed = 190;

        public Player()
        {
            _keyBindings = new Dictionary<Keyboard.Key, Action>
            {
                {Keyboard.Key.W, () => Velocity.Y = -Speed},
                {Keyboard.Key.A, () => Velocity.X = -Speed},
                {Keyboard.Key.S, () => Velocity.Y = Speed},
                {Keyboard.Key.D, () => Velocity.X = Speed}
            };

        }

        public override void Update()
        {
            Velocity = new Vector2f();
            foreach (var keyBinding in _keyBindings)
            {
                if (Keyboard.IsKeyPressed(keyBinding.Key))
                    keyBinding.Value();
            }
            Velocity = Velocity.Normalize() * Speed;
            Position += Velocity * Environment.FrameDelta;

            if (Mouse.IsButtonPressed(Mouse.Button.Left))
            {

                var p = Environment.ObjectManager.Spawn<Projectile>();
                p.Position = Position;
                p.Initialize(new RectangleShape(new Vector2f(5f, 5f)), Environment, Environment.ObjectManager.Destroy);
                p.SetVelocity(Orientation.Normalize() * 500f);
            }
            Orientation = Environment.MousePosition - Position;
            //Environment.LogText($"orientation: {Orientation.X}, {Orientation.Y}");

        }
    }
}
