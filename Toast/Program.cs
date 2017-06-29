using System;
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
            var watch = new Stopwatch();
            watch.Start();
            _window = new RenderWindow(new VideoMode(), "Toast", Styles.Fullscreen, new ContextSettings { AntialiasingLevel = 8 });
            var env = new SimpleEnvironment(_window, new GameObjectManager());
            var windowCreate = watch.Elapsed.TotalSeconds;
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
            var p = env.ObjectManager.Spawn<Player>();
            p.Initialize(new RectangleShape(new Vector2f(50f, 50f)) { Texture = new Texture(@"..\..\..\media\magic.png") { Smooth = true }, Scale = new Vector2f(4f, 4f) }, env, go => env.ObjectManager.Destroy(go));
            p.Position = new Vector2f(screenCenter.X, screenCenter.Y);

            for (int i = 0; i < 1000; i++)
            {
                var e = env.ObjectManager.Spawn<Enemy>();
                e.Initialize(new CircleShape(20f) { FillColor = Color.Cyan }, env, gob => { });
                e.Position = new Vector2f(MathUtil.RandomGaussian(screenCenter.X, 1000, _window.Size.X / 2.0f), MathUtil.RandomGaussian(screenCenter.Y, 490, _window.Size.Y / 2.0f));
            }

            var showFps = false;
            _window.KeyPressed += (sender, eventArgs) =>
            {
                if (eventArgs.Code == Keyboard.Key.Tilde)
                    showFps = !showFps;
            };
            var font = new Font(@"c:\windows\fonts\ariblk.ttf");
            var previous = (float)watch.Elapsed.TotalSeconds;
            var lag = 0f;
            var fpsBuffer = new Queue<float>();
            while (_window.IsOpen)
            {
                env.DebugText.Clear();
                //foreach (var gameObjectBase in env.ma)
                //{
                //    objects.Remove(gameObjectBase);
                //}
                //destroyed.Clear();
                var time = (float)watch.Elapsed.TotalSeconds;
                var dt = time - previous;
                previous = time;
                lag += dt;

                _window.DispatchEvents();
                _window.Clear();
                while (lag > env.FrameDelta)
                {
                    var objects = env.ObjectManager.Objects.ToList();
                    foreach (var gameObjectBase in objects)
                    {
                        gameObjectBase.Update();
                    }
                    lag -= env.FrameDelta;
                }
                env.FrameRemainder = lag;

                foreach (var gameObjectBase in env.ObjectManager.Objects)
                {
                    _window.Draw(gameObjectBase);
                }
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
