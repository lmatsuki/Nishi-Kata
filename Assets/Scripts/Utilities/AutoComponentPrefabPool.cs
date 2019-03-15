using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace NishiKata.Utilities
{
    /// <summary>
    /// Variant pool that automatically instantiates objects from a given Unity component prefab.
    /// </summary>
    public class AutoComponentPrefabPool<T> : UnityComponentPool<T>
        where T : Component
    {
        /// <summary>
        /// Our base prefab.
        /// </summary>
        protected readonly T prefab;

        /// <summary>
        /// Initialisation method for objects.
        /// </summary>
        protected readonly Action<T> initialize;

        /// <summary>
        /// Create a new pool for the given Unity prefab.
        /// </summary>
        /// <param name="prefab">The prefab we're cloning</param>
        public AutoComponentPrefabPool(T prefab)
            : this(prefab, null, null, 0)
        {
        }

        /// <summary>
        /// Create a new pool for the given Unity prefab with a given number of starting elements.
        /// </summary>
        /// <param name="prefab">The prefab we're cloning</param>
        /// <param name="initialCapacity">The number of elements to seed the pool with</param>
        public AutoComponentPrefabPool(T prefab, int initialCapacity)
            : this(prefab, null, null, initialCapacity)
        {
        }

        /// <summary>
        /// Create a new pool for the given Unity prefab.
        /// </summary>
        /// <param name="prefab">The prefab we're cloning</param>
        /// <param name="initialize">An initialisation function to call after creating prefabs</param>
        public AutoComponentPrefabPool(T prefab, Action<T> initialize)
            : this(prefab, initialize, null, 0)
        {
        }

        /// <summary>
        /// Create a new pool for the given Unity prefab.
        /// </summary>
        /// <param name="prefab">The prefab we're cloning</param>
        /// <param name="initialize">An initialisation function to call after creating prefabs</param>
        /// <param name="reset">Function to use to reset items when retrieving from the pool</param>
        /// <param name="initialCapacity">The number of elements to seed the pool with</param>
        public AutoComponentPrefabPool(T prefab, Action<T> initialize, Action<T> reset, int initialCapacity)
            : base(DummyFactory, reset, 0)
        {
            // Pass 0 to initial capacity because we need to set ourselves up first
            // We then call Grow again ourselves
            this.initialize = initialize;
            this.prefab = prefab;
            this.factory = PrefabFactory;

            if (initialCapacity > 0)
            {
                Grow(initialCapacity);
            }
        }

        /// <summary>
        /// Create our new prefab item clone.
        /// </summary>
        private T PrefabFactory()
        {
            T newElement = Object.Instantiate(prefab);
            if (initialize != null)
            {
                initialize(newElement);
            }

            return newElement;
        }
    }
}
