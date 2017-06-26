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
        protected GameObjectBase(Shape shape, IEnvironment environment)
        {
            _renderer = new Renderer(environment, shape, () => Velocity, () => Position, () => 0f);
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
