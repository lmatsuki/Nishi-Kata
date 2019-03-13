using UnityEngine;

namespace NishiKata.Utilities
{
    public class UtilityExtensions
    {
        public static void DestroyObjectsByTag(string tag)
        {
            GameObject[] gameObjects = GameObject.FindGameObjectsWithTag(tag);

            for (int i = 0; i < gameObjects.Length; i++)
            {
                Object.Destroy(gameObjects[i]);
            }
        }
    }
}
