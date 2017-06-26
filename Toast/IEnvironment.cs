using System.Collections.Generic;
using SFML.System;

namespace Toast
{
    public interface IEnvironment
    {
        float FrameDelta { get; }
        float FrameRemainder { get; }

        List<GameObjectBase> GameObjects { get; }
        Vector2f MousePosition { get; }
        void LogText(string s);
    }
}