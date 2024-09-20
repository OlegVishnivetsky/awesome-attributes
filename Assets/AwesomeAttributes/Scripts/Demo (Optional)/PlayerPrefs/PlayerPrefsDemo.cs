using AwesomeAttributes;
using UnityEngine;

public class PlayerPrefsDemo : MonoBehaviour
{
    [PlayerPrefs(SaveMeTestKey)]
    [SerializeField] private int saveMe;

    private const string SaveMeTestKey = "SaveMeKey";

    private void Awake()
    {
        saveMe = PlayerPrefs.GetInt(SaveMeTestKey, 0);
    }
}