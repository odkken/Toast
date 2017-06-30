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

        public override void Initialize(Shape shape, IEnvironment environment, Window window)
        {
            base.Initialize(shape, environment, window);

            window.KeyReleased += (sender, args) =>
            {
                if (args.Code == Keyboard.Key.Space)
                    _canSprintAgain = true;
            };
        }

        public Player()
        {
            _keyBindings = new Dictionary<Keyboard.Key, Action>
            {
                {Keyboard.Key.W, () => Velocity.Y = -Speed},
                {Keyboard.Key.A, () => Velocity.X = -Speed},
                {Keyboard.Key.S, () => Velocity.Y = Speed},
                {Keyboard.Key.D, () => Velocity.X = Speed}
            };
            _sprintTicks = SprintDuration;
        }

        private bool _sprinting = false;
        private const int SprintDuration = 30;
        private int _sprintTicks;
        private bool _canSprintAgain = true;
        private float lastShotTime = 0;
        public override void Update()
        {
            
            if (_sprinting && _sprintTicks > 0)
            {
                _sprintTicks--;
                Position += Velocity * Environment.FrameDelta;
            }
            else
            {

                Velocity = new Vector2f();
                foreach (var keyBinding in _keyBindings)
                {
                    if (Keyboard.IsKeyPressed(keyBinding.Key))
                        keyBinding.Value();
                }
                Velocity = Velocity.Normalize() * Speed;
                if (Keyboard.IsKeyPressed(Keyboard.Key.Space) && _canSprintAgain)
                {

                    _sprinting = true;
                    _sprintTicks = SprintDuration;
                    _canSprintAgain = false;
                    Velocity = Velocity.Normalize() * Speed * 5;
                    base.Update();
                    return;
                }


                Position += Velocity * Environment.FrameDelta;

                if (Mouse.IsButtonPressed(Mouse.Button.Left) && Environment.Time - lastShotTime > 0.2)
                {
                    lastShotTime = Environment.Time;
                    var normalOrient = Orientation.Normalize();
                    for (int i = 0; i <20; i++)
                    {
                        var p = Environment.ObjectManager.Spawn<Projectile>();
                        p.Initialize(new CircleShape(5f) { FillColor = Color.Red, OutlineColor = Color.Yellow, OutlineThickness = 2.0f }, Environment, null);
                        p.Position = Position;
                        p.SetVelocity((normalOrient + new Vector2f(MathUtil.RandomGaussian(0,.15f), MathUtil.RandomGaussian(0,.15f))) * 500f);
                    }
                    
                }
                Orientation = Environment.MousePosition - Position;
                base.Update();
            }





        }
    }
}
