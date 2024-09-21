using UnityEngine;

namespace AwesomeAttributes.Demo
{
    public class RequiredDemo : MonoBehaviour
    {
        [Required]
        [SerializeField] private GameObject requiredObject;

        [Required("Please don't forget this", RequiredMessageType.Info)]
        [SerializeField] private GameObject withCustomMessage;

        [Required(RequiredMessageType.Warning)]
        [SerializeField] private GameObject withDifMessageType;
    }
}