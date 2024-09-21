using UnityEngine;

namespace AwesomeAttributes.Demo
{
    public class MinMaxSliderDemo : MonoBehaviour
    {
        [MinMaxSlider(-10f, 10f)]
        [SerializeField] private Vector2 value;
    }
}