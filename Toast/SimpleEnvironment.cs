using System.Collections.Generic;

namespace Toast
{
    internal class SimpleEnvironment : IEnvironment
    {
        public float FrameDelta => 0.02f;
        public float FrameRemainder { get; set; }
        public List<GameObjectBase> GameObjects { get; set; }
    }
}