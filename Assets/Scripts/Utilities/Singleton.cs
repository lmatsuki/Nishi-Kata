using System;
using UnityEngine;

namespace NishiKata.Utilities
{
    public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        //public static T instance { get; set; }

        private static T s_instance;

        /// <summary>
        /// The static reference to the instance
        /// </summary>
        public static T instance
        {
            get
            {
                return s_instance;
            }
            protected set
            {
                s_instance = value;
            }
        }

        public static bool InstanceExists
        {
            get
            {
                return s_instance != null;
            }
        }

        public static event Action InstanceSet;

        protected virtual void Awake()
        {
            if (s_instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                s_instance = (T)this;
                if (InstanceSet != null)
                {
                    InstanceSet();
                }
            }
        }

        protected virtual void OnDestroy()
        {
            if (s_instance == this)
            {
                s_instance = null;
            }
        }
    }
}
