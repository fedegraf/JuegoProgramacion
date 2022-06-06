using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFactory<T, S>
    where T : IProduct<S>
    where S : ScriptableObject
{
    T Product { get; }
    T CreateBullet(BulletTypeSO stats);
}

