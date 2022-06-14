using System.Collections;
using System;
using UnityEngine;

public class Character : MonoBehaviour
{
    public EntityTypeSO Stats => stats;
    public Movement Movement { get; private set; }
    public Shooter.BulletShooter Shooter { get; private set; }
    public Item.ItemPickUpper ItemPickUpper { get; private set; }

    [SerializeField] private EntityTypeSO stats;

    private void Awake()
    {
        Movement = GetComponent<Movement>();
        Shooter = GetComponent<Shooter.BulletShooter>();
        ItemPickUpper = GetComponent<Item.ItemPickUpper>();


        Movement.SetValues(Stats.WalkSpeed, Stats.RunSpeed, Stats.RotationSpeed);
    }

    public void DoWalking(Vector2 direction, bool isRunning)
    {
        if (!Movement)
        {
            Debug.LogWarning("Movement Null");
            return;
        }

        Movement.Walking(direction, isRunning);
    }

    public void DoRotation(Vector2 rotation)
    {
        if (!Movement)
        {
            Debug.LogWarning("Movement Null");
            return;
        }

        Movement.Rotation(rotation);
    }

    public void DoShoot()
    {
        if (!Shooter)
        {
            Debug.LogWarning("Shooter Null");
            return;
        }

        Shooter.DoShoot();
    }

    public void DoReload()
    {
        if (!Shooter)
        {
            Debug.LogWarning("Shooter Null");
            return;
        }

        Shooter.DoReloadAmmo();
    }

    public void DoPickUpItem()
    {
        if (!ItemPickUpper)
        {
            Debug.LogWarning("ItemPickUpper Null");
            return;
        }

        ItemPickUpper.PickUpItem();
    }

    public void ShowItems()
    {
        if (!ItemPickUpper)
        {
            Debug.LogWarning("ItemPickUpper Null");
            return;
        }

        ItemPickUpper.ShowItems();
    }
}