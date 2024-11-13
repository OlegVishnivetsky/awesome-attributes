using UnityEngine;

namespace AwesomeAttributes.Demo
{
    public class WithoutLableDemo : MonoBehaviour
    {
        [WithoutLabel]
        [SerializeField] private Vector2 startingPosition;
    }
}