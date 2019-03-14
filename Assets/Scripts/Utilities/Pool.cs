using System;
using System.Collections.Generic;

namespace NishiKata.Utilities
{
    public class Pool<T>
    {
        /// <summary>
        /// Our factory function
        /// </summary>
        protected Func<T> factory;

        /// <summary>
        /// A list of all m_Available items
        /// </summary>
        protected readonly List<T> availableItems;

        /// <summary>
        /// A list of all items managed by the pool
        /// </summary>
        protected readonly List<T> allItems;

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
        /// Returns an object to the m_Available list. Does not check for consistency
        /// </summary>
        protected virtual void ReturnToPoolInternal(T element)
        {
            availableItems.Add(element);
        }
    }
}
