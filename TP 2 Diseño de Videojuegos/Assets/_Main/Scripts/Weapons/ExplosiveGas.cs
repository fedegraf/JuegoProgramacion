using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveGas : MonoBehaviour
{
    [SerializeField] private GameObject explosionObject;
    [SerializeField] private Weapons.AmmoTypeSO explosiveType;

    private void OnCollisionEnter(Collision collision)
    {

        Debug.Log("here");
        if (collision.gameObject.layer != LayerMask.NameToLayer("Bullet")) return;

        SetExplosive();

        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != LayerMask.NameToLayer("Bullet")) return;

        SetExplosive();

        gameObject.SetActive(false);
    }

    private void SetExplosive()
    {
        GameObject grenade = Instantiate(explosionObject, transform.position, transform.rotation);
        grenade.GetComponent<IProduct<Weapons.AmmoTypeSO>>().SetData(explosiveType);
        grenade.GetComponent<Weapons.GrenadeScript>().ToggleCanThrow();
        grenade.GetComponent<Weapons.GrenadeScript>().Explode();

    }
}
