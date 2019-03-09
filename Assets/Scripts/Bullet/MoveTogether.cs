using UnityEngine;

namespace NishiKata.Bullet
{
    public class MoveTogether : MonoBehaviour
    {
        public Transform follow;

        void Update()
        {
            transform.position = follow.position;
        }
    }
}
