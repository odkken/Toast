using System;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Toast
{
    public class Bounds
    {
        private readonly Func<FloatRect> _getBounds;

        public Bounds(Func<FloatRect> getBounds)
        {
            _getBounds = getBounds;
        }

        public Vector2f TopLeft
        {
            get
            {
                var bounds = _getBounds();
                return new Vector2f(bounds.Left, bounds.Top);
            }
        }
        public Vector2f BottomRight
        {
            get
            {
                var bounds = _getBounds();
                return new Vector2f(bounds.Left + bounds.Width, bounds.Top + bounds.Height);
            }
        }
    }

    public abstract class GameObjectBase : Drawable
    {
        protected IEnvironment Environment;
        private Renderer _renderer;

        public Vector2f Position
        {
            get => _position;
            set
            {
                _position = value;
                Bounds = _renderer.Shape.GetGlobalBounds();

            }
        }

        protected Vector2f Velocity;

        public Vector2f Orientation;
        private Vector2f _position;
        public bool IsDestroyed { get; set; }
        public Vector2f Size { get; private set; }
        public FloatRect Bounds { get; private set; }


        public virtual void Initialize(Shape shape, IEnvironment environment, Window window)
        {
            Size = shape.Size();
            shape.Origin = Size / 2f;
            _renderer = new Renderer(environment, shape, () => Velocity, () => Position, () => Orientation.Rotation());
            Environment = environment;
            Bounds = _renderer.Shape.GetGlobalBounds();
        }
        public virtual void Update()
        {
            Bounds = _renderer.Shape.GetGlobalBounds();
        }
        public int TimesDrawn { get; private set; }
        public virtual void Draw(RenderTarget target, RenderStates states)
        {
            TimesDrawn++;
            _renderer.Render(target, states);
        }
    }
}
