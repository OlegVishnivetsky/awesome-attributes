using UnityEngine;

namespace AwesomeAttributes.Demo
{
    public class ReadonlyIfDemo : MonoBehaviour
    {
        [ReadonlyIf("isValueReadonly")]
        [SerializeField] private string value = "Name";
        [SerializeField] private bool isValueReadonly;

        [ReadonlyIf("&&", "firstBool", "secondBool")]
        [SerializeField] private string withConditionsOperator = "If both bool is true/false";
        [SerializeField] private bool firstBool;
        [SerializeField] private bool secondBool;

        [ReadonlyIf(ShowIfTestEnum.Hide, "testEnum")]
        [SerializeField] private string enumReadonly = "Readonly if enum Hide";
        [SerializeField] private ShowIfTestEnum testEnum;
    }
}