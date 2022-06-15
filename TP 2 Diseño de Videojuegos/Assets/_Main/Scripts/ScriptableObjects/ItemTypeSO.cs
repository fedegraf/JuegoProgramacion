using System.Collections;
using UnityEngine;

namespace Items
{
    [CreateAssetMenu(menuName = "New Item", fileName = "Item Type", order = 1)]
    public class ItemTypeSO : ScriptableObject
    {
        [SerializeField] private Vector3 scale = Vector3.one;
        [SerializeField] private Color color = Color.white;
        [SerializeField] private Mesh mesh;
        [SerializeField] private float interactionRadius;

        public string ItemName => name;
        public Vector3 Scale => scale;
        public Color Color => color;
        public Mesh Mesh => mesh;
        public float InteractionRadius => interactionRadius;
    }
}