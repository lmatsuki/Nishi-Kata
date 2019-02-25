using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public IPlayerMovement playerMovement;

    void Start()
    {
        // Set PlayerMovement depending on platform
        #if UNITY_EDITOR
            AssignMovementForEditor();
        #else
            AssignMovementForPlayer();
        #endif
    }

    void FixedUpdate() 
	{
        if (playerMovement == null)
        {
            return;
        }

        playerMovement.UpdateMovement();
    }

    void AssignMovementForEditor()
    {
        if (EditorUserBuildSettings.activeBuildTarget == BuildTarget.StandaloneWindows)
        {
            playerMovement = GetComponent<DesktopPlayerMovement>();
        }
        else if (EditorUserBuildSettings.activeBuildTarget == BuildTarget.Android)
        {
            playerMovement = GetComponent<AndroidPlayerMovement>();
        }
    }

    void AssignMovementForPlayer()
    {
        if (Application.platform == RuntimePlatform.WindowsPlayer)
        {
            playerMovement = GetComponent<DesktopPlayerMovement>();
        }
        else if (Application.platform == RuntimePlatform.Android)
        {
            playerMovement = GetComponent<AndroidPlayerMovement>();
        }
    }
}
