using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using SFML.System;
using SFML.Window;

namespace Toast
{
    internal class SimpleEnvironment : IEnvironment
    {
        private readonly Window _window;
        private Stopwatch watch;
        public SimpleEnvironment(Window window, IGameObjectManager objectManager)
        {
            watch = new Stopwatch();
            watch.Start();
            _window = window;
            ObjectManager = objectManager;
        }

        public float FrameDelta => 0.01f;
        public float FrameRemainder { get; set; }
        public Queue<string> DebugText { get; set; } = new Queue<string>();
        public Vector2f MousePosition => Mouse.GetPosition(_window).ToFloat();
        public IGameObjectManager ObjectManager { get; private set; }
        public float Time => (float) watch.Elapsed.TotalSeconds;

        public void LogText(string s)
        {
            DebugText.Enqueue(s);
        }
    }
}