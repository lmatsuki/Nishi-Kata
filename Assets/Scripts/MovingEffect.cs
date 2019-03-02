using UnityEngine;

public class MovingEffect : MonoBehaviour
{
    public GameObject movingParticleFX;

    private ParticleSystem currentMovingFX;
    private Rigidbody rigidbody;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        GameObject movingParticle = Instantiate(movingParticleFX, transform);
        currentMovingFX = movingParticle.GetComponent<ParticleSystem>();
    }

    void Update()
    {
        if (IsMoving())
        {
            if (!currentMovingFX.isPlaying)
            {
                currentMovingFX.Play();
            }
        }
        else
        {
            if (currentMovingFX.isPlaying)
            {
                currentMovingFX.Stop();
            }
        }
    }

    private bool IsMoving()
    {
        return (rigidbody.velocity != Vector3.zero);
    }
}
