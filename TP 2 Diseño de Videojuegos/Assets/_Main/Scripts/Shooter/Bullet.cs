using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, IProduct<BulletTypeSO>
{
    public BulletTypeSO Data => _data;
    public float Damage => Data.Damage;

    private BulletTypeSO _data;
    private float _currentLifeTime;

    private void Update()
    {
        _currentLifeTime += Time.deltaTime;
        if (_currentLifeTime >= Data.LifeTime) DestroyBullet();
        else Movement();
    }

    public void SetData(BulletTypeSO newData)
    {
        _data = newData;
        GetComponent<IDamager>().SetDamage(Damage);
    }

    private void Movement()
    {
        float finalSpeed = Data.Speed * Time.deltaTime;
        transform.position += transform.forward * finalSpeed;
    }

    private void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
