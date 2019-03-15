using System.Collections.Generic;
using UnityEngine;

namespace NishiKata.Utilities
{
    public class PoolManager : Singleton<PoolManager>
    {
        /// <summary>
        /// List of poolables that will be used to initialize corresponding pools
        /// </summary>
        public List<Poolable> poolables;

        /// <summary>
        /// Dictionary of pools, key is the prefab
        /// </summary>
        protected Dictionary<Poolable, AutoComponentPrefabPool<Poolable>> pools;

        /// <summary>
        /// Gets a poolable component from the corresponding pool
        /// </summary>
        /// <param name="poolablePrefab"></param>
        /// <returns></returns>
        public Poolable GetPoolable(Poolable poolablePrefab)
        {
            if (!pools.ContainsKey(poolablePrefab))
            {
                pools.Add(poolablePrefab, new AutoComponentPrefabPool<Poolable>(poolablePrefab, Initialize, null,
                                                                                  poolablePrefab.initialCapacity));
            }

            AutoComponentPrefabPool<Poolable> pool = pools[poolablePrefab];
            Poolable spawnedInstance = pool.Get();

            spawnedInstance.pool = pool;
            return spawnedInstance;
        }

        /// <summary>
        /// Initializes the dicionary of pools.
        /// </summary>
        protected void Start()
        {
            pools = new Dictionary<Poolable, AutoComponentPrefabPool<Poolable>>();

            foreach (var poolable in poolables)
            {
                if (poolable == null)
                {
                    continue;
                }
                pools.Add(poolable, new AutoComponentPrefabPool<Poolable>(poolable, Initialize, null,
                                                                            poolable.initialCapacity));
            }
        }

        private void Initialize(Component poolable)
        {
            poolable.transform.SetParent(transform, false);
        }
    }
}
