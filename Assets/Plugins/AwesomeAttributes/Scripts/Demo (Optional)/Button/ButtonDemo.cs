using UnityEngine;

namespace AwesomeAttributes.Demo
{
    public class ButtonDemo : MonoBehaviour
    {
        [Button("DebugHealth")]
        [SerializeField] private float health;

        [Button(nameof(DebugWithParameters), "With Custom Label and Height", 50f)]
        [SerializeField] private string withTitle;

        public void DebugHealth()
        {
            Debug.Log($"Health: {health}");
        }

        public void DebugWithParameters(string text, int health)
        {
            Debug.Log($"Text: {text}, 2 * health = {health * 2}");
        }
    }
}