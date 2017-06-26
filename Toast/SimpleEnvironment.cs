using System.Collections.Generic;
using System.Runtime.InteropServices;
using SFML.System;
using SFML.Window;

namespace Toast
{
    internal class SimpleEnvironment : IEnvironment
    {
        private readonly Window _window;

        public SimpleEnvironment(Window window)
        {
            _window = window;
        }

        public float FrameDelta => 0.01f;
        public float FrameRemainder { get; set; }
        public List<GameObjectBase> GameObjects { get; set; }
        public Queue<string> DebugText { get; set; } = new Queue<string>();
        public Vector2f MousePosition => Mouse.GetPosition(_window).ToFloat();
        public void LogText(string s)
        {
            DebugText.Enqueue(s);
        }
    }
}