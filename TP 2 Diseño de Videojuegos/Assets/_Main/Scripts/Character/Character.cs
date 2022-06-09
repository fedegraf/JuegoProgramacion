using System.Collections;
using UnityEngine;

public class Character : MonoBehaviour
{
    public EntityTypeSO Stats => stats;
    public Movement Movement { get; private set; }
    public BulletShooter Shooter { get; private set; }

    [SerializeField] private EntityTypeSO stats;

    private void Awake()
    {
        Movement = GetComponent<Movement>();
        Shooter = GetComponent<BulletShooter>();

        Movement.SetValues(Stats.WalkSpeed ,Stats.RunSpeed , Stats.RotationSpeed);
    }

    public void DoWalking(Vector2 direction, bool isRunning)
    {
        if (!Movement) return;

        Movement.Walking(direction, isRunning);
    }

    public void DoRotation(Vector2 rotation)
    {
        if (!Movement) return;

        Movement.Rotation(rotation);
    }

    public void DoShoot()
    {
        if (!Shooter) return;

        Shooter.DoShoot();
    }

    public void DoReload()
    {
        if (!Shooter) return;

        Shooter.DoReloadAmmo();
    }
}