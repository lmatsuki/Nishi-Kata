using System;
using System.Collections.Generic;

namespace NishiKata.Utilities
{
    /// <summary>
    /// Maintains a pool of objects.
    /// </summary>
    public class Pool<T>
    {
        /// <summary>
        /// Our factory function.
        /// </summary>
        protected Func<T> factory;

        /// <summary>
        /// Our resetting function.
        /// </summary>
        protected readonly Action<T> reset;

        /// <summary>
        /// A list of all available items.
        /// </summary>
        protected readonly List<T> availableItems;

        /// <summary>
        /// A list of all items managed by the pool.
        /// </summary>
        protected readonly List<T> allItems;

        /// <summary>
        /// Creates a new blank pool.
        /// </summary>
        /// <param name="factory">The function that creates pool objects</param>
        public Pool(Func<T> factory)
            : this(factory, null, 0)
        {
        }

        /// <summary>
        /// Create a new pool with a given number of starting elements.
        /// </summary>
        /// <param name="factory">The function that creates pool objects</param>
        /// <param name="initialCapacity">The number of elements to seed the pool with</param>
        public Pool(Func<T> factory, int initialCapacity)
            : this(factory, null, initialCapacity)
        {
        }

        /// <summary>
        /// Create a new pool with a given number of starting elements.
        /// </summary>
        /// <param name="factory">The function that creates pool objects</param>
        /// <param name="reset">Function to use to m_Reset items when retrieving from the pool</param>
        /// <param name="initialCapacity">The number of elements to seed the pool with</param>
        public Pool(Func<T> factory, Action<T> reset, int initialCapacity)
        {
            if (factory == null)
            {
                throw new ArgumentNullException("factory");
            }

            availableItems = new List<T>();
            allItems = new List<T>();
            this.factory = factory;
            this.reset = reset;

            if (initialCapacity > 0)
            {
                Grow(initialCapacity);
            }
        }

        /// <summary>
        /// Gets an item from the pool, growing it if necessary.
        /// </summary>
        /// <returns></returns>
        public virtual T Get()
        {
            return Get(reset);
        }

        /// <summary>
        /// Gets an item from the pool, growing it if necessary, and with a specified m_Reset function.
        /// </summary>
        /// <param name="resetOverride">A function to use to m_Reset the given object</param>
        public virtual T Get(Action<T> resetOverride)
        {
            if (availableItems.Count == 0)
            {
                Grow(1);
            }
            if (availableItems.Count == 0)
            {
                throw new InvalidOperationException("Failed to grow pool");
            }

            int itemIndex = availableItems.Count - 1;
            T item = availableItems[itemIndex];
            availableItems.RemoveAt(itemIndex);

            if (resetOverride != null)
            {
                resetOverride(item);
            }

            return item;
        }

        /// <summary>
        /// Return an item to the pool
        /// </summary>
        public virtual void Return(T pooledItem)
        {
            if (allItems.Contains(pooledItem) &&
                !availableItems.Contains(pooledItem))
            {
                ReturnToPoolInternal(pooledItem);
            }
            else
            {
                throw new InvalidOperationException("Trying to return an item to a pool that does not contain it: " + pooledItem +
                                                    ", " + this);
            }
        }

        /// <summary>
        /// Grow the pool by a given number of elements
        /// </summary>
        public void Grow(int amount)
        {
            for (int i = 0; i < amount; ++i)
            {
                AddNewElement();
            }
        }

        /// <summary>
        /// Adds a new element to the pool.
        /// </summary>
        protected virtual T AddNewElement()
        {
            T newElement = factory();
            allItems.Add(newElement);
            availableItems.Add(newElement);

            return newElement;
        }

        /// <summary>
        /// Dummy factory that returns the default T value.
        /// </summary>		
        protected static T DummyFactory()
        {
            return default(T);
        }

        /// <summary>
        /// Returns an object to the m_Available list. Does not check for consistency.
        /// </summary>
        protected virtual void ReturnToPoolInternal(T element)
        {
            availableItems.Add(element);
        }
    }
}
