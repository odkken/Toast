using System.Collections.Generic;

namespace Toast
{
    public interface IEnvironment
    {
        float FrameDelta { get; }
        float FrameRemainder { get; }

        List<GameObjectBase> GameObjects { get; }
    }
}