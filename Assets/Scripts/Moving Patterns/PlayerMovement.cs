using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public IPlayerMovement playerMovement;

    void Start()
    {
        if (Application.platform == RuntimePlatform.WindowsPlayer ||
            Application.platform == RuntimePlatform.WindowsEditor)
        {
            playerMovement = GetComponent<DesktopPlayerMovement>();
        }
    }

	void FixedUpdate() 
	{
        if (playerMovement == null)
        {
            return;
        }

        playerMovement.UpdateMovement();
    }
}
