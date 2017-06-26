using System;
using SFML.Graphics;
using SFML.System;

namespace Toast
{
    public abstract class GameObjectBase : Drawable
    {
        protected readonly IEnvironment Environment;
        private readonly Renderer _renderer;
        public Vector2f Position;
        protected Vector2f Velocity;

        public Vector2f Orientation;
        protected GameObjectBase(Shape shape, IEnvironment environment)
        {
            shape.Origin = shape.Size() / 2;
            _renderer = new Renderer(environment, shape, () => Velocity, () => Position, () =>Orientation.Rotation());
            Environment = environment;
        }

        public virtual void Update()
        {
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            _renderer.Render(target, states);
        }
    }
}
