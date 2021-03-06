﻿using System;
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
            get { return _player ?? (_player = Environment.ObjectManager.Objects.First(a => a is Player)); }
        }

        public override void Update()
        {
            Velocity = new Vector2f();
            var delta = Player.Position - Position;
            var dsm = delta.SquaredMagnitude();
            if (dsm < .75)
                return;
            Velocity = delta.Normalize() * Speed;
            Position += Velocity * Environment.FrameDelta;




            base.Update();
        }

        public float Speed => 155.0f;
    }
}