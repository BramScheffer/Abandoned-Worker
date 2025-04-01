using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quitgame : MonoBehaviour
{
    public void Quit()
    {
        // Quit the game
        Application.Quit();

        // Stop play mode in the Unity Editor
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
