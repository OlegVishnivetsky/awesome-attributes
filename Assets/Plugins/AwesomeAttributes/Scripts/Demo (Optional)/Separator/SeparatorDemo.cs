using UnityEngine;

namespace AwesomeAttributes.Demo
{
    public class SeparatorDemo : MonoBehaviour
    {
        [SeparationLine(1, 1, 30)]
        [SerializeField] private float maxHealth;
        [SerializeField] private float currentHealth;

        [SeparationLine(1, 30, 30)]
        [SerializeField] private float ammo;
        [SerializeField] private float maxAmmo;

        [SeparationLine(5, 10, 10)]
        [SerializeField] private float speed;
        [SerializeField] private float currentSpeed;
    }
}