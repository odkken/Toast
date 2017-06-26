﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Toast
{
    static class Program
    {
        private static RenderWindow _window;
        public static void Main(string[] args)
        {
            _window = new RenderWindow(new VideoMode(), "Toast", Styles.Fullscreen, new ContextSettings { AntialiasingLevel = 8});
            _window.SetVerticalSyncEnabled(true);
            //_window.SetFramerateLimit(60);

            var screenCenter = _window.Size / 2;

            _window.SetVisible(true);
            _window.Closed += OnClosed;
            _window.KeyPressed += (sender, eventArgs) =>
            {
                if (eventArgs.Code == Keyboard.Key.Escape)
                    _window.Close();
            };
            var env = new SimpleEnvironment(_window);
            var objects = new List<GameObjectBase>
            {
                new Player(new RectangleShape(new Vector2f(50f, 50f)), env){Position = new Vector2f(screenCenter.X, screenCenter.Y)},
                new Enemy(env)
            };
            env.GameObjects = objects;
            var showFps = false;
            _window.KeyPressed += (sender, eventArgs) =>
            {
                if (eventArgs.Code == Keyboard.Key.Tilde)
                    showFps = !showFps;
            };
            var font = new Font(@"c:\windows\fonts\ariblk.ttf");
            var watch = new Stopwatch();
            watch.Start();
            var previous = (float)watch.Elapsed.TotalSeconds;
            var lag = 0f;
            var fpsBuffer = new Queue<float>();
            while (_window.IsOpen)
            {
                env.DebugText.Clear();
                var time = (float)watch.Elapsed.TotalSeconds;
                var dt = time - previous;
                previous = time;
                lag += dt;

                _window.DispatchEvents();
                _window.Clear();

                while (lag > env.FrameDelta)
                {
                    objects.ForEach(a => a.Update());
                    lag -= env.FrameDelta;
                }
                env.FrameRemainder = lag;
                objects.ForEach(_window.Draw);
                if (showFps)
                {
                    fpsBuffer.Enqueue(1 / dt);
                    while (fpsBuffer.Count > 10)
                    {
                        fpsBuffer.Dequeue();
                    }
                    env.LogText($"fps: {fpsBuffer.Average():F1}");
                }
                _window.Draw(new Text(string.Join("\n", env.DebugText), font));
                _window.Display();
            }
        }

        private static void OnClosed(object sender, EventArgs e)
        {
            _window.Close();
        }
    }
}
