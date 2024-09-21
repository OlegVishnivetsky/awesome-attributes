using UnityEngine;

namespace AwesomeAttributes.Demo
{
    public class LabelDemo : MonoBehaviour
    {
        [Label("Short Name")]
        [SerializeField] private string veryVeryLongName;
    }
}