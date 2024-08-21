using UnityEngine;
using AwesomeAttributes;

public class Testing : MonoBehaviour
{ 
    [Gradient] 
    [SerializeField] private Gradient _gradient;
    
    [ResourcesPath]
    [SerializeField] private string spritePath;
    
    [PlayerPrefs("SaveMe")]
    [SerializeField] private int saveMe;
    
    [TagSelector]
    [SerializeField] private string tagSelector;

    [Scene]
    [SerializeField] private string sceneField;

    [Title("Health", "Player health")]
    [GUIColor("#faab09")]
    [SerializeField] private float maxHealth;

    [Button("ButtonPressedDebug", "Show Current Helth")]
    [SerializeField] private float currentHealth;

    [GUIColor("#ff00ff")]
    [SeparationLine(1, 10, 10)]
    [SerializeField] private float speed;
    [SerializeField, Readonly] public float maxSpeed = 4;

    [ShowProperty("MyProperty")]
    [SerializeField] private int myProperty = 43;
    [SerializeField] private Transform childTransform;
    [SerializeField] private Sprite sprite;
    [GUIColor("#ff00ff")]
    [SerializeField, Label("Max Speed")] private int veryLongFieldNameForMaxSpeed = 20;
    [SerializeField, WithoutLabel] private Vector3 withoutLabelVector;
    [SerializeField] private bool showHidenValue;
    [SerializeField] private bool enableReadonly;
    [SerializeField] private ShowIfTestEnum showIfEnumTest;

    [ShowIf("showHiddenValue")]
    [SerializeField] private int showMePlease = 1;

    [ReadonlyIf("enableReadonly")]
    [SerializeField] private int readonlyHide = 30;
    [SerializeField, OnlyChildGameObjects] private Rigidbody2D onlyChildObjects;

    [Required]
    [SerializeField] private GameObject requiredObject;

    [MinMaxSlider(0, 20)]
    [SerializeField] private Vector2 minMaxValue;

    private void Start()
    {
        saveMe = PlayerPrefs.GetInt("SaveMe");
        Debug.Log(onlyChildObjects.name);
    }

    public void ButtonPressedDebug()
    {
        Debug.Log($"Current health {currentHealth}");
    }

    public bool CheckCondition()
    {
        return true;
    }

    public void IncreaseSaveMeValue()
    {
        saveMe++;
    }
}

public enum ShowIfTestEnum
{
    Show,
    Hide
}