using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shooter
{
    [CreateAssetMenu(menuName = "New Weapon", fileName = "Weapon Type", order = 1)]
    public class WeaponTypeSO : ScriptableObject
    {
        [SerializeField] private int magazineSize;
        [Tooltip("Time Bewtween Bullets")]
        [SerializeField] private float cadence;
        [SerializeField] private float reloadTime;
        [SerializeField] BulletTypeSO ammoType;
        [SerializeField] Mesh mesh;
        public string WeaponName => name;
        public int MagazineSize => magazineSize;
        public float Cadence => cadence;
        public float ReloadTime => reloadTime;
        public BulletTypeSO BulletType => ammoType;
        public Mesh Mesh => mesh;
    }

}

