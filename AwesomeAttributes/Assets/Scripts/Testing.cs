using UnityEngine;

public class Testing : MonoBehaviour
{
    [Title("Health", "Player health")]
    [SerializeField] private float maxHealth;
    [SerializeField] private float health;
    [SeparationLine(1, 3)]
    [SerializeField] private float speed;
    [SerializeField] private Transform childTransform;

    public void TestDebug()
    {
        Debug.Log("Button pressed");
    }
}