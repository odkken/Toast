using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Timers;
using SFML.Graphics;
using SFML.System;

namespace Toast
{
    public class Projectile : GameObjectBase
    {
        private Stopwatch _watch;

        public Projectile()
        {
            _watch = new Stopwatch();
            _watch.Start();
        }

        public void SetVelocity(Vector2f vel)
        {
            Velocity = vel;
        }

        public override void Update()
        {
            if(IsDestroyed)
                return;
            Position += Velocity * Environment.FrameDelta;


            var collisions = Environment.ObjectManager.GetCollidingEntities(this).Where(a=> !a.IsDestroyed && a is Enemy).ToList();

            if (collisions.Count > 0)
            {
                Environment.ObjectManager.Destroy(collisions.First());
                Environment.ObjectManager.Destroy(this);
            }
            
            if (_watch.Elapsed.TotalSeconds > 2.0)
            {
                _watch.Stop();
                Environment.ObjectManager.Destroy(this);
            }

            base.Update();
        }
    }
}