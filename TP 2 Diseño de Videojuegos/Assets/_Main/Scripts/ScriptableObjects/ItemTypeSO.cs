using System.Collections;
using UnityEngine;

namespace Items
{
    [CreateAssetMenu(menuName = "New Item", fileName = "Item Type", order = 1)]
    public class ItemTypeSO : ScriptableObject
    {
        [SerializeField] private Vector3 scale = Vector3.one;
        [SerializeField] private Material material;
        [SerializeField] private Mesh mesh;
        [SerializeField] private float interactionRadius;

        public string ItemName => name;
        public Vector3 Scale => scale;
        public Material Material => material;
        public Mesh Mesh => mesh;
        public float InteractionRadius => interactionRadius;
    }
}