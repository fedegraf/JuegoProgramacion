using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons
{
    public interface IFactory<T, S>
    where T : IProduct<S>
    where S : ScriptableObject
    {
        T Product { get; }
        T CreateBullet(AmmoTypeSO stats);
    }

}
