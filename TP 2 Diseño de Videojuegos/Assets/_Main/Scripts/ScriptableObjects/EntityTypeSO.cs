using UnityEngine;

[CreateAssetMenu(menuName = "New Entity", fileName = "Entity Type", order = 1)]
public class EntityTypeSO : ScriptableObject
{
    [SerializeField] private string enemyName;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float rotationSpeed;

    public string EnemyName => enemyName;
    public float WalkSpeed => walkSpeed;
    public float RunSpeed => walkSpeed * 1.75f;
    public float RotationSpeed => rotationSpeed;
}
