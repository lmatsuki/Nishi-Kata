using System.Collections.Generic;

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
        //protected Dictionary<Poolable, AutoComponentPrefabPool<Poolable>> m_Pools;

        ///// <summary>
        ///// Gets a poolable component from the corresponding pool
        ///// </summary>
        ///// <param name="poolablePrefab"></param>
        ///// <returns></returns>
        //public Poolable GetPoolable(Poolable poolablePrefab)
        //{
        //    if (!m_Pools.ContainsKey(poolablePrefab))
        //    {
        //        m_Pools.Add(poolablePrefab, new AutoComponentPrefabPool<Poolable>(poolablePrefab, Initialize, null,
        //                                                                          poolablePrefab.initialPoolCapacity));
        //    }

        //    AutoComponentPrefabPool<Poolable> pool = m_Pools[poolablePrefab];
        //    Poolable spawnedInstance = pool.Get();

        //    spawnedInstance.pool = pool;
        //    return spawnedInstance;
        //}
    }
}
