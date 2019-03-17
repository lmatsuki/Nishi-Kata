using UnityEngine;

namespace NishiKata.Utilities
{
    public class DebugExtensions
    {
        public static void LogNotFound(object script, string value)
        {
            Debug.LogErrorFormat("{0}.cs: {1} not found!", script.GetType(), value);
        }
    }
}