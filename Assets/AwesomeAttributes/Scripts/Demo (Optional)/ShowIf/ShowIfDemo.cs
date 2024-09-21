using UnityEngine;

namespace AwesomeAttributes.Demo
{
    public class ShowIfDemo : MonoBehaviour
    {
        [ShowIf("isValueShown")]
        [SerializeField] private string value = "Name";
        [SerializeField] private bool isValueShown;

        [ShowIf("&&", "firstBool", "secondBool")]
        [SerializeField] private string withConditionsOperator = "If both bool is true/false";
        [SerializeField] private bool firstBool;
        [SerializeField] private bool secondBool;

        [ShowIf(ShowIfTestEnum.Show, "testEnum")]
        [SerializeField] private string enumShowIf = "You can see me";
        [SerializeField] private ShowIfTestEnum testEnum;
    }
}