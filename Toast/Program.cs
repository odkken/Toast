using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using Toast.Collision;

namespace Toast
{
    static class Program
    {
        private static RenderWindow _window;

        static void SpawnEnemy(IEnvironment env, Vector2f screenCenter)
        {
            var e = env.ObjectManager.Spawn<Enemy>();
            e.Initialize(new CircleShape(20f) { FillColor = Color.Cyan }, env, _window);
            e.Position = new Vector2f(MathUtil.RandomGaussian(screenCenter.X, _window.Size.X / 5f, _window.Size.X / 2.0f), MathUtil.RandomGaussian(screenCenter.Y, _window.Size.Y / 5f, _window.Size.Y / 2.0f));
        }
        public static void Main(string[] args)
        {
            var watch = new Stopwatch();
            watch.Start();
            _window = new RenderWindow(new VideoMode(), "Toast", Styles.Fullscreen, new ContextSettings { AntialiasingLevel = 2 });
            BSP bsp = null;
            var manager = new GameObjectManager(gob => bsp?.GetCollisions(gob.Bounds));
            var env = new SimpleEnvironment(_window, manager);
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
            p.Initialize(new RectangleShape(new Vector2f(50f, 50f)) { Texture = new Texture(@"media\magic.png") { Smooth = true }, Scale = new Vector2f(4f, 4f) }, env, _window);
            p.Position = new Vector2f(screenCenter.X, screenCenter.Y);
            var numEnemies = 500;
            for (int i = 0; i < 500; i++)
            {
                SpawnEnemy(env, new Vector2f(screenCenter.X, screenCenter.Y));
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
                var time = (float)watch.Elapsed.TotalSeconds;
                var dt = time - previous;
                previous = time;
                lag += dt;

                _window.DispatchEvents();
                _window.Clear();

                var preUpdate = watch.Elapsed.TotalSeconds;
                while (lag > env.FrameDelta)
                {
                    var objects = env.ObjectManager.Objects.ToList();
                    for (int i = 0; i < numEnemies - objects.Count(a => a is Enemy); i++)
                    {
                        SpawnEnemy(env, new Vector2f(screenCenter.X, screenCenter.Y));
                    }
                    objects = env.ObjectManager.Objects.ToList();
                    bsp = new BSP(new HashSet<GameObjectBase>(objects), new FloatRect(0, 0, _window.Size.X, _window.Size.Y));
                    foreach (var gameObjectBase in objects)
                    {
                        gameObjectBase.Update();
                    }
                    manager.DestroyAll();
                    lag -= env.FrameDelta;
                }
                var postUpdate = watch.Elapsed.TotalSeconds;
                env.LogText($"update %: {100 * (postUpdate - preUpdate) / dt:F1}");
                env.FrameRemainder = lag;

                foreach (var gameObjectBase in env.ObjectManager.Objects)
                {
                    _window.Draw(gameObjectBase);
                }
                if (bsp != null)
                {
                    //_window.Draw(bsp);
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
