using System.Collections;
using System;
using UnityEngine;

public class Character : MonoBehaviour
{
    public EntityTypeSO Stats => stats;
    public Movement Movement { get; private set; }
    public Shooter.BulletShooter Shooter { get; private set; }
    public Shooter.BulletShooterV2 ShooterV2 { get; private set; }
    public Items.ItemPickUp ItemPickUp { get; private set; }

    [SerializeField] private EntityTypeSO stats;

    private void Awake()
    {
        Movement = GetComponent<Movement>();
        Shooter = GetComponent<Shooter.BulletShooter>();
        ShooterV2 = GetComponent<Shooter.BulletShooterV2>();
        ItemPickUp = GetComponent<Items.ItemPickUp>();


        Movement.SetValues(Stats.WalkSpeed, Stats.RunSpeed, Stats.RotationSpeed);
    }

    private bool CheckForMovement()
    {
        var isNull = Movement == null;
        if (isNull)
        {
            Debug.LogWarning("Shooter is Null");
            return true;
        }

        else return false;
    }

    private bool CheckForShooter()
    {
        var isNull = ShooterV2 == null;
        if (isNull)
        {
            Debug.LogWarning("Shooter is Null");
            return true;
        }

        else return false;
    }

    private bool CheckForItemPickUp()
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
        if (CheckForMovement()) return;

        Movement.Walking(direction, isRunning);
    }

    public void DoRotation(Vector2 rotation)
    {
        if (CheckForMovement()) return;

        Movement.Rotation(rotation);
    }

    public void DoShoot()
    {
        if (CheckForShooter()) return;

        ShooterV2.DoShoot();
    }

    public void DoReload()
    {
        if (CheckForShooter()) return;

        ShooterV2.DoReload();
    }

    public void DoPickUp()
    {
        if (CheckForItemPickUp()) return;

        ItemPickUp.PickUp();
    }

    public void DoItemInGroundUse()
    {
        if (CheckForItemPickUp()) return;

        ItemPickUp.UseItemInGround();
    }

    public void DoShowItems()
    {
        if (CheckForItemPickUp()) return;

        ItemPickUp.ShowItems();
    }
}