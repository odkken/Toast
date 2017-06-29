using System.Collections.Generic;
using System.Runtime.InteropServices;
using SFML.System;
using SFML.Window;

namespace Toast
{
    internal class SimpleEnvironment : IEnvironment
    {
        private readonly Window _window;

        public SimpleEnvironment(Window window, IGameObjectManager objectManager)
        {
            _window = window;
            ObjectManager = objectManager;
        }

        public float FrameDelta => 0.01f;
        public float FrameRemainder { get; set; }
        public Queue<string> DebugText { get; set; } = new Queue<string>();
        public Vector2f MousePosition => Mouse.GetPosition(_window).ToFloat();
        public IGameObjectManager ObjectManager { get; private set; }

        public void LogText(string s)
        {
            DebugText.Enqueue(s);
        }
    }
}