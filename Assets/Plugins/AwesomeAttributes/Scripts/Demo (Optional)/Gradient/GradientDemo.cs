using UnityEngine;

namespace AwesomeAttributes.Demo
{
    public class GradientDemo : MonoBehaviour
    {
        [Gradient()]
        [SerializeField] private Gradient gradientColor;
    }
}