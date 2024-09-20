using AwesomeAttributes;
using UnityEngine;

public class ResourcesPathDemo : MonoBehaviour
{
    [ResourcesPath]
    [SerializeField] private string path;
}