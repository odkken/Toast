using System.Collections;
using System.Collections.Generic;
using SFML.System;

namespace Toast
{
    public interface IGameObjectManager
    {
        T Spawn<T>() where T : GameObjectBase, new();
        void Destroy(GameObjectBase gameObject);
        IEnumerable<GameObjectBase> Objects { get;}
        IEnumerable<GameObjectBase> GetCollidingEntities(GameObjectBase gameObject);
    }
}