using AwesomeAttributes;
using UnityEngine;

public class TagSelectorDemo : MonoBehaviour
{
    [TagSelector]
    [SerializeField] private string playerTag;
}