using System.Collections;
using System;
using UnityEngine;

public class Character : MonoBehaviour
{
    public EntityTypeSO Stats => stats;
    public Movement Movement { get; private set; }
    public Weapons.WeaponController Weapon { get; private set; }
    public Items.ItemInteracter ItemInteracter { get; private set; }
    public Items.ItemLooter ItemLooter { get; private set; }
    public Skills.SkillController SkillController { get; private set; }

    [SerializeField] private EntityTypeSO stats;

    private void Awake()
    {
        Movement = GetComponent<Movement>();
        Weapon = GetComponent<Weapons.WeaponController>();
        ItemInteracter = GetComponent<Items.ItemInteracter>();
        ItemLooter = GetComponent<Items.ItemLooter>();
        SkillController = GetComponent<Skills.SkillController>();


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

    private bool IsWeaponNull()
    {
        var isNull = Weapon == null;
        if (isNull)
        {
            Debug.LogWarning("Shooter is Null");
            return true;
        }

        else return false;
    }

    private bool IsItemInteractorNull()
    {
        var isNull = ItemInteracter == null;
        if (isNull)
        {
            Debug.LogWarning("ItemPickup is Null");
            return true;
        }

        else return false;
    }

    private bool IsItemLooterNull()
    {
        var isNull = ItemLooter == null;
        if (isNull)
        {
            Debug.LogWarning("ItemPickup is Null");
            return true;
        }

        else return false;
    }

    private bool IsExpandForceNull()
    {
        var isNull = SkillController == null;
        if (isNull)
        {
            Debug.LogWarning("ExpandForce is Null");
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
        if (IsWeaponNull()) return;

        Weapon.DoShoot();
    }

    public void DoReload()
    {
        if (IsWeaponNull()) return;

        Weapon.DoReload();
    }

    public void DoCycleWeapons()
    {
        if (IsWeaponNull()) return;

        Weapon.DoCycleWeapons();
    }

    public void DoItemUse()
    {
        if (IsItemInteractorNull() || IsItemLooterNull()) return;

        ItemInteracter.InteractWithItem();
        ItemLooter.LootItem();
    }

    public void DoSkill()
    {
        if (IsExpandForceNull()) return;

        SkillController.UseSkill();
    }
}