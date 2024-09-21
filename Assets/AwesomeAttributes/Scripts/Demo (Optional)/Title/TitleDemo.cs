using UnityEngine;

namespace AwesomeAttributes.Demo
{
    public class TitleDemo : MonoBehaviour
    {
        [Title("Health Parameters", TitleTextAlignments.Center)]
        [SerializeField] private float maxHealth;
        [SerializeField] private float health;

        [Title("Move Speed", "With subtitle")]
        [SerializeField] private float moveSpeed;
        [SerializeField] private float maxSpeed;

        [Title("Player Ammo", TitleTextAlignments.Right)]
        [SerializeField] private float currentAmmo;
        [SerializeField] private float maxAmmo;
    }
}