using System;
using SFML.Graphics;
using SFML.System;

namespace Toast
{
    public class Renderer
    {
        private readonly IEnvironment _environment;
        public Shape Shape { get; }
        private readonly Func<Vector2f> _getVelocity;
        private readonly Func<Vector2f> _getPosition;
        private readonly Func<float> _getRotation;
        private readonly Vector2f _originalScale;

        public Renderer(IEnvironment environment, Shape shape, Func<Vector2f> getVelocity, Func<Vector2f> getPosition, Func<float> getRotation)
        {
            _environment = environment;
            Shape = shape;
            _originalScale = Shape.Scale;
            _getVelocity = getVelocity;
            _getPosition = getPosition;
            _getRotation = getRotation;
        }

        public void Render(RenderTarget target, RenderStates states)
        {
            Shape.Position = _getPosition() + _getVelocity() * _environment.FrameRemainder;
            Shape.Scale = new Vector2f(_originalScale.X * (float)Math.Cos(_getRotation() * 2 * Math.PI/360f), _originalScale.Y);
            Shape.Draw(target, states);
        }
    }
}