using UnityEngine;

public class MoveTogether : MonoBehaviour
{
    public Transform follow;

	void Update ()
    {
        transform.position = follow.position;	
	}
}
