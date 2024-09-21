using UnityEngine;

namespace AwesomeAttributes.Demo
{
    public class ButtonDemo : MonoBehaviour
    {
        [Button("DebugHealth")]
        [SerializeField] private float health;

        [Button("DebugWithTitle", "With Custom Label and Height", 50f)]
        [SerializeField] private string withTitle;

        public void DebugHealth()
        {
            Debug.Log($"Health: {health}");
        }

        public void DebugWithTitle()
        {
            Debug.Log("Button clicked");
        }
    }
}