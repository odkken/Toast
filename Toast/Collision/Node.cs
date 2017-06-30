using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;

namespace Toast.Collision
{
    public interface ICollisionDetector
    {
        List<GameObjectBase> GetCollisions(FloatRect bounds);
    }

    public class BSP : ICollisionDetector, Drawable
    {
        private readonly HashSet<GameObjectBase> _objects;
        private readonly BSP[] _children;
        private FloatRect _bounds;
        private readonly Shape _shape;
        public BSP(HashSet<GameObjectBase> objects, FloatRect bounds, bool horizontal = true)
        {
            _bounds = bounds;
            var half = new Vector2f(bounds.Left + bounds.Width / 2, bounds.Top + bounds.Height / 2);
            _shape = new RectangleShape(new Vector2f(bounds.Width, bounds.Height)) { Position = new Vector2f(bounds.Left, bounds.Top), FillColor = Color.Transparent, OutlineColor = Color.White, OutlineThickness = 2f };
            if (objects.Count < 16 || _bounds.Width < 1.0 || _bounds.Height < 1.0)
            {
                _children = new BSP[2];
                _objects = objects;
                return;
            }

            var childSize = new Vector2f(horizontal ? bounds.Width : bounds.Width / 2, horizontal ? bounds.Height / 2 : bounds.Height);
            var childBounds = new List<FloatRect>
            {
                new FloatRect(bounds.Left, bounds.Top, childSize.X, childSize.Y),
                new FloatRect(horizontal ? bounds.Left : half.X, horizontal ? half.Y : bounds.Top, childSize.X, childSize.Y)
            };
            _children = new BSP[2];
            var i = 0;
            foreach (var childRegion in childBounds)
            {
                var region = childRegion;
                var objectsForChild = new HashSet<GameObjectBase>(objects.Where(a => region.Contains(a.Bounds.Left, a.Bounds.Top) &&
                                                         region.Contains(a.Bounds.Left + a.Bounds.Width,
                                                             a.Bounds.Top + a.Bounds.Height)));
                foreach (var gob in objectsForChild)
                {
                    objects.Remove(gob);
                }
                _children[i++] = new BSP(objectsForChild, childRegion, !horizontal);
            }
            _objects = new HashSet<GameObjectBase>(objects);
        }

        public List<GameObjectBase> GetCollisions(FloatRect bounds)
        {
            return _objects.Where(a => a.Bounds.Intersects(bounds)).Concat(_children.Where(a => a != null && a._bounds.Intersects(bounds)).SelectMany(a => a.GetCollisions(bounds))).ToList();
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            _shape.Draw(target, states);
            foreach (var child in _children)
            {
                child?.Draw(target, states);
            }
        }
    }
}
