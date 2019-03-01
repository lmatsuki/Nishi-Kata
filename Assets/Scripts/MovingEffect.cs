using UnityEngine;

public class MovingEffect : MonoBehaviour
{
    public GameObject movingParticleFX;

    private GameObject currentMovingFX;
    private Rigidbody rigidbody;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        currentMovingFX = Instantiate(movingParticleFX, transform);
        currentMovingFX.SetActive(false);
    }

    void Update()
    {
        if (IsMoving())
        {
            print("Moving!");
            if (!currentMovingFX.activeSelf)
            {
                print("Active particles!");
                currentMovingFX.SetActive(true);
            }
        }
        else
        {
            print("Stopped!");
            currentMovingFX.SetActive(false);
        }
    }

    private bool IsMoving()
    {
        return (rigidbody.velocity != Vector3.zero);
    }
}
