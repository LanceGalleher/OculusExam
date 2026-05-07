using TMPro;
using UnityEngine;

/// <summary>
/// This class holds the score and ensures there is only one instance of it. Also allows it to persist over multiple scenes.
/// </summary>
public class Score : MonoBehaviour
{
    public static Score Instance; // singleton instance of the score controller
    [HideInInspector] public int score = 0;

    void Awake()
    {
        // make it so only one instance of the ScoreController can exist and that it persists between scenes
        if (Instance == null)
        {
            Instance = this; // set the instance to this object
            DontDestroyOnLoad(gameObject); // mark this GameObject to not be destroyed
        }
        else
        {
            Destroy(gameObject); // if an instance already exists, destroy this new one
        }
    }

}
