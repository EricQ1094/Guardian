using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
// The Master Controller that communicate with all other Controller scripts.
// This script controls global values like Crusor states and global functions like press key to stop playing.
public class GlobalController : MonoBehaviour
{
    private static GlobalController thisGCInstance;
    public static GlobalController thisGlobalController
    {
        get
        {
            return thisGCInstance;
        }
    }

    public bool inMenu = false;
    // Start is called before the first frame update
    private void Awake()
    {
        thisGCInstance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (inMenu)
        {
            Cursor.lockState = CursorLockMode.None;
        }

        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

#if (UNITY_EDITOR)

        if (Input.GetKeyDown(KeyCode.P))
        {
            EditorApplication.isPlaying = false;
        }

#endif
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
