using UnityEngine;

namespace AwesomeAttributes.Demo
{
    public class TagSelectorDemo : MonoBehaviour
    {
        [TagSelector]
        [SerializeField] private string playerTag;
    }
}