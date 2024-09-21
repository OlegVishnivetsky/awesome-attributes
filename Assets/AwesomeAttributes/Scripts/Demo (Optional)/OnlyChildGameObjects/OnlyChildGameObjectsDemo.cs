using UnityEngine;

namespace AwesomeAttributes.Demo
{
    public class OnlyChildGameObjectsDemo : MonoBehaviour
    {
        [OnlyChildGameObjects]
        [SerializeField] private CircleCollider2D onlyChild;
    }
}