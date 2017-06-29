using System;
using SFML.Graphics;
using SFML.System;

namespace Toast
{
    public abstract class GameObjectBase : Drawable
    {
        protected IEnvironment Environment;
        private Action<GameObjectBase> _destroyAction;
        private Renderer _renderer;
        public Vector2f Position;
        protected Vector2f Velocity;

        public Vector2f Orientation;

        public void Initialize(Shape shape, IEnvironment environment, Action<GameObjectBase> destroyAction)
        {

            shape.Origin = shape.Size() / 2;
            _renderer = new Renderer(environment, shape, () => Velocity, () => Position, () => Orientation.Rotation());
            Environment = environment;
            _destroyAction = destroyAction;
        }
        public virtual void Update()
        {
        }

        protected void Destroy()
        {
            _destroyAction(this);
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            _renderer.Render(target, states);
        }
    }
}
