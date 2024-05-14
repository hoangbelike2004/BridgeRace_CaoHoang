using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;



public class ObjectPooling : Singleton<ObjectPooling>
{
    [System.Serializable]
    class Pool
    {
        public GameObject prefab;
        public Transform parent;
        public PoolType pooltype;
        public int size;

    }
    [SerializeField] private List<Pool> pools;
    private Dictionary<PoolType, IObjectPool<GameObject>> pooldictionary;

    private void Start()
    {
        pooldictionary = new Dictionary<PoolType, IObjectPool<GameObject>>(); ;
        foreach (var pool in pools) {
            Debug.Log(1);
            IObjectPool<GameObject> poolObject = new ObjectPool<GameObject>(
                () => Instantiate(pool.prefab,pool.parent),
                obj => obj.gameObject.SetActive(true),
                obj => obj.gameObject.SetActive(false),
                obj => Destroy(obj.gameObject),
                true,
                pool.size,
                pool.size*2
                    );
            pooldictionary.Add(pool.pooltype, poolObject);
        }
    }

    public GameObject SpawnGameUnitFromPool(PoolType pooltype,Vector3 pos,Quaternion quaternion)
    {
        IObjectPool<GameObject> poolObject = pooldictionary[pooltype];
        if(poolObject != null)
        {
            GameObject instace = poolObject.Get();
            instace.gameObject.transform.position = pos;
            instace.gameObject.transform.rotation = quaternion;
            return instace;
        }
        else
        {
            return null;
        }
    }
    public void ReturnToPool(PoolType pooltype,GameObject gob)
    {
        IObjectPool<GameObject> poolObject = pooldictionary[pooltype];
        if (poolObject != null) {
            poolObject.Release(gob);
        }
        else
        {
            Debug.Log("No pool found");
        }
    }
}
