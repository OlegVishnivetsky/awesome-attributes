using AwesomeAttributes;
using UnityEngine;

public class OnlyChildGameObjectsDemo : MonoBehaviour
{
    [OnlyChildGameObjects]
    [SerializeField] private CircleCollider2D onlyChild;
}