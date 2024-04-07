using UnityEngine;

public class Testing : MonoBehaviour
{
    [Title("Health", "Player health")]
    [GUIColor("#faab09")]
    [SerializeField] private float maxHealth;
    [SerializeField] private float health;
    [GUIColor("#ff00ff")]
    [SeparationLine(1, 3)]
    [SerializeField] private float speed;
    [SerializeField, Readonly] public float maxSpeed = 4;
    [SerializeField] private Transform childTransform;

    [ShowProperty("MyProperty")]
    [SerializeField] private int myProperty = 0;

    public int MyProperty
    {
        get { return myProperty; }
        set
        {
            if (myProperty > 100)
            {
                return;
            }

            myProperty = value;
        }
    }

    public void TestDebug()
    {
        Debug.Log("Button pressed");
    }
}