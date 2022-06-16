using System.Collections;
using System;
using UnityEngine;

public class Character : MonoBehaviour
{
    public EntityTypeSO Stats => stats;
    public Movement Movement { get; private set; }
    public Shooter.BulletShooter Shooter { get; private set; }
    public Items.ItemPickUp ItemPickUp { get; private set; }

    [SerializeField] private EntityTypeSO stats;

    private void Awake()
    {
        Movement = GetComponent<Movement>();
        Shooter = GetComponent<Shooter.BulletShooter>();
        ItemPickUp = GetComponent<Items.ItemPickUp>();


        Movement.SetValues(Stats.WalkSpeed, Stats.RunSpeed, Stats.RotationSpeed);
    }

    private bool IsMovementNull()
    {
        var isNull = Movement == null;
        if (isNull)
        {
            Debug.LogWarning("Shooter is Null");
            return true;
        }

        else return false;
    }

    private bool IsShooterNull()
    {
        var isNull = Shooter == null;
        if (isNull)
        {
            Debug.LogWarning("Shooter is Null");
            return true;
        }

        else return false;
    }

    private bool IsItemPickUpNull()
    {
        var isNull = ItemPickUp == null;
        if (isNull)
        {
            Debug.LogWarning("ItemPickup is Null");
            return true;
        }

        else return false;
    }

    public void DoWalking(Vector2 direction, bool isRunning)
    {
        if (IsMovementNull()) return;

        Movement.Walking(direction, isRunning);
    }

    public void DoRotation(Vector2 rotation)
    {
        if (IsMovementNull()) return;

        Movement.Rotation(rotation);
    }

    public void DoShoot()
    {
        if (IsShooterNull()) return;

        Shooter.DoShoot();
    }

    public void DoReload()
    {
        if (IsShooterNull()) return;

        Shooter.DoReload();
    }

    public void DoCycleWeapons()
    {
        if (IsShooterNull()) return;

        Shooter.DoCycleWeapons();
    }

    public void DoPickUp()
    {
        if (IsItemPickUpNull()) return;

        ItemPickUp.PickUp();
    }

    public void DoItemInGroundUse()
    {
        if (IsItemPickUpNull()) return;

        ItemPickUp.UseItemInGround();
    }
}