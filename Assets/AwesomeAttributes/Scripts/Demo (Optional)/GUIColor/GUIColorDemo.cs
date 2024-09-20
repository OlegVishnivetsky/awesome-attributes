using AwesomeAttributes;
using UnityEngine;

public class GUIColorDemo : MonoBehaviour
{
    [GUIColor("#5394fc")]
    [SerializeField] private string colorMe;

    [GUIColor(65, 207, 33)]
    [SerializeField] private string rgb;
}