using System;
using UnityEngine;

namespace NishiKata.Utilities
{
    /// <summary>
    /// A variant pool that takes Unity components. Automatically enables and disables them as necessary.
    /// </summary>
    public class UnityComponentPool<T> : Pool<T>
        where T : Component
    {
        /// <summary>
        /// Creates a new blank pool.
        /// </summary>
        /// <param name="factory">The function that creates pool objects</param>
        public UnityComponentPool(Func<T> factory)
            : base(factory)
        {
        }

        /// <summary>
        /// Create a new pool with a given number of starting elements.
        /// </summary>
        /// <param name="factory">The function that creates pool objects</param>
        /// <param name="initialCapacity">The number of elements to seed the pool with</param>
        public UnityComponentPool(Func<T> factory, int initialCapacity)
            : base(factory, initialCapacity)
        {
        }

        /// <summary>
        /// Create a new pool with a given number of starting elements
        /// </summary>
        /// <param name="factory">The function that creates pool objects</param>
        /// <param name="reset">Function to use to reset items when retrieving from the pool</param>
        /// <param name="initialCapacity">The number of elements to seed the pool with</param>
        public UnityComponentPool(Func<T> factory, Action<T> reset, int initialCapacity)
            : base(factory, reset, initialCapacity)
        {
        }

        /// <summary>
        /// Retrieve an enabled element from the pool.
        /// </summary>
        public override T Get(Action<T> resetOverride)
        {
            T element = base.Get(resetOverride);

            element.gameObject.SetActive(true);

            return element;
        }

        /// <summary>
        /// Automatically disable returned object.
        /// </summary>
        protected override void ReturnToPoolInternal(T element)
        {
            element.gameObject.SetActive(false);

            base.ReturnToPoolInternal(element);
        }

        /// <summary>
        /// Keep newly created objects disabled.
        /// </summary>
        protected override T AddNewElement()
        {
            T newElement = base.AddNewElement();

            newElement.gameObject.SetActive(false);

            return newElement;
        }
    }
}
