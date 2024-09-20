using AwesomeAttributes;
using UnityEngine;

public class LabelDemo : MonoBehaviour
{
    [Label("Short Name")]
    [SerializeField] private string veryVeryLongName;
}