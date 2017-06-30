using System;
using System.Collections.Generic;
using SFML.System;

namespace Toast
{
    public class GameObjectManager : IGameObjectManager
    {
        private readonly HashSet<GameObjectBase> _objects = new HashSet<GameObjectBase>();
        private readonly HashSet<GameObjectBase> _toDestroy = new HashSet<GameObjectBase>();

        public IEnumerable<GameObjectBase> Objects => _objects;
        Func<GameObjectBase, IEnumerable<GameObjectBase>> _getColliding;

        public GameObjectManager(Func<GameObjectBase, IEnumerable<GameObjectBase>> getColliding)
        {
            _getColliding = getColliding;
        }

        public IEnumerable<GameObjectBase> GetCollidingEntities(GameObjectBase gameObject)
        {
            return _getColliding(gameObject);
        }

        public T Spawn<T>() where T : GameObjectBase, new()
        {
            var o = new T();
            _objects.Add(o);
            return o;
        }

        public void Destroy(GameObjectBase gameObject)
        {
            _toDestroy.Add(gameObject);
            gameObject.IsDestroyed = true;
        }

        public void DestroyAll()
        {
            foreach (var go in _toDestroy)
            {
                _objects.Remove(go);
            }
            _toDestroy.Clear();
        }
    }
}