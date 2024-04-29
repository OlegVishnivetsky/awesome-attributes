using UnityEngine;

public class Testing : MonoBehaviour
{
    [Title("Health", "Player health")]
    [GUIColor("#faab09")]
    [SerializeField] private float maxHealth;
    [Button("ButtonPressedDebug", "Show Current Helth")]
    [SerializeField] private float currentHealth;
    [GUIColor("#ff00ff")]
    [SeparationLine(1, 3)]
    [SerializeField] private float speed;
    [ShowProperty("MyProperty")]
    [SerializeField] private int myProperty = 43;
    [SerializeField, Readonly] public float maxSpeed = 4;
    [SerializeField] private Transform childTransform;
    [SerializeField] private Sprite sprite;
    [GUIColor("#ff00ff")]
    [SerializeField, Label("Max Speed")] private int veryLongFieldNameForMaxSpeed = 20;
    [SerializeField, WithoutLabel] private Vector3 withoutLabelVector;
    [SerializeField] private bool showHidenValue;
    [SerializeField] private bool enableReadonly;
    [SerializeField] private ShowIfTestEnum showIfEnumTest;

    [ShowIf("showHidenValue")]
    [SerializeField] private int showMePlease = 1;

    [ReadonlyIf("enableReadonly")]
    [SerializeField] private int readonlyHide = 30;

    [Required]
    [SerializeField] private GameObject requiredObject;

    [MinMaxSlider(0, 20)]
    [SerializeField] private Vector2 minMaxValue;

    public int MyProperty 
    {
        get { return myProperty; }
        set
        {
            myProperty = value;
        }
    }

    public void ButtonPressedDebug()
    {
        Debug.Log($"Current health {currentHealth}");
    }

    public bool CheckCondition()
    {
        return true;
    }
}

public enum ShowIfTestEnum
{
    Show,
    Hide
}