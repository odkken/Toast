using System;
using SFML.Graphics;
using SFML.System;

namespace Toast
{
    public class Renderer
    {
        private readonly IEnvironment _environment;
        private readonly Shape _shape;
        private readonly Func<Vector2f> _getVelocity;
        private readonly Func<Vector2f> _getPosition;
        private readonly Func<float> _getRotation;

        public Renderer(IEnvironment environment, Shape shape, Func<Vector2f> getVelocity, Func<Vector2f> getPosition, Func<float> getRotation)
        {
            _environment = environment;
            _shape = shape;
            _getVelocity = getVelocity;
            _getPosition = getPosition;
            _getRotation = getRotation;
        }

        public void Render(RenderTarget target, RenderStates states)
        {
            _shape.Position = _getPosition() + _getVelocity() * _environment.FrameRemainder;
            _shape.Rotation = _getRotation();
            _shape.Draw(target, states);
        }
    }
}