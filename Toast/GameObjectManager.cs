using System.Collections.Generic;
using SFML.System;

namespace Toast
{
    public class GameObjectManager : IGameObjectManager
    {
        private readonly HashSet<GameObjectBase> _objects = new HashSet<GameObjectBase>();

        public IEnumerable<GameObjectBase> Objects => _objects;

        public T Spawn<T>() where T : GameObjectBase, new()
        {
            var o = new T();
            _objects.Add(o);
            return o;
        }

        public T Spawn<T>(Vector2f position) where T : GameObjectBase
        {
            throw new System.NotImplementedException();
        }

        public void Destroy(GameObjectBase gameObject)
        {
            throw new System.NotImplementedException();
        }
    }
}