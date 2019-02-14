using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform follow;
    public float speed;
	
    void Start()
    {
        // Follow the player by default
        if (follow == null)
        {
            follow = GameObject.FindGameObjectWithTag(Tags.Player).transform;
        }
    }

	void Update ()
    {
        float step = speed * Time.deltaTime;

        Vector3 newPosition = Vector3.MoveTowards(transform.position, follow.transform.position, step);
        transform.position = new Vector3(newPosition.x, 10, newPosition.z);
    }
}
