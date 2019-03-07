using NishiKata.Utilities;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public float speed;

    private Transform follow;

    void Start()
    {
        // Follow the player by default
        if (follow == null)
        {
            follow = GameObject.Find(Names.Player).transform;
        }
    }

	void Update ()
    {
        float step = speed * Time.deltaTime;

        Vector3 newPosition = Vector3.MoveTowards(transform.position, follow.transform.position, step);
        transform.position = new Vector3(newPosition.x, 10, newPosition.z);
    }
}
