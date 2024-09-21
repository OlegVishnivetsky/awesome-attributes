using UnityEngine;

namespace AwesomeAttributes.Demo
{
    public class ResourcesPathDemo : MonoBehaviour
    {
        [ResourcesPath]
        [SerializeField] private string path;
    }
}