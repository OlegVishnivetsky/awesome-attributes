using UnityEngine;

namespace AwesomeAttributes.Demo
{
    public class ReadonlyDemo : MonoBehaviour
    {
        [Readonly]
        [SerializeField] private string readonlyString = "I am read only";

        [Readonly(ReadonlyType.InPlayMode)]
        [SerializeField] private string inPlayMode = "I am read only in play mode";
    }
}