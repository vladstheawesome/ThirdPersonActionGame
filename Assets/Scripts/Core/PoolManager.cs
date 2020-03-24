using System.Collections;
using System.Collections.Generic;
using ThirdPersonGame.PooledObjects;
using UnityEngine;

namespace ThirdPersonGame.Core
{
    public class PoolManager : Singleton<PoolManager>
    {
        public Dictionary<PoolObjectType, List<GameObject>> PoolDictionary = new Dictionary<PoolObjectType, List<GameObject>>();

        public void SetUpDictionary()
        {
            // List for every Enum
            PoolObjectType[] arr = System.Enum.GetValues(typeof(PoolObjectType)) as PoolObjectType[];

            foreach (PoolObjectType p in arr)
            {
                if (!PoolDictionary.ContainsKey(p))
                {
                    PoolDictionary.Add(p, new List<GameObject>());
                }
            }
        }

        // Get the object by specifying the ENUM
        public GameObject GetObject(PoolObjectType objType)
        {
            if (PoolDictionary.Count == 0)
            {
                SetUpDictionary();
            }

            List<GameObject> list = PoolDictionary[objType];
            GameObject obj = null;

            if (list.Count > 0)
            {
                obj = list[0];
                list.RemoveAt(0);
            }
            else
            {
                obj = PoolObjectLoader.InstantiatePrefab(objType).gameObject;
            }

            return obj;
        }

        // Once an object is used, we put it back into the pool
        public void AddObject(PoolObject obj)
        {
            List<GameObject> list = PoolDictionary[obj.poolObjectType];
            list.Add(obj.gameObject);
            obj.gameObject.SetActive(false);
        }
    }
}
