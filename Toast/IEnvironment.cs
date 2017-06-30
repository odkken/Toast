using System.Collections.Generic;
using SFML.System;

namespace Toast
{
    public interface IEnvironment
    {
        float FrameDelta { get; }
        float FrameRemainder { get; }
        Vector2f MousePosition { get; }
        IGameObjectManager ObjectManager { get; }
        float Time { get; }
        void LogText(string s);
    }
}