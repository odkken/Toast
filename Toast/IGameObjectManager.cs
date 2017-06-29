using System.Collections;
using System.Collections.Generic;
using SFML.System;

namespace Toast
{
    public interface IGameObjectManager
    {
        T Spawn<T>() where T : GameObjectBase, new();
        T Spawn<T>(Vector2f position) where T : GameObjectBase;
        void Destroy(GameObjectBase gameObject);
        IEnumerable<GameObjectBase> Objects { get;}
    }
}