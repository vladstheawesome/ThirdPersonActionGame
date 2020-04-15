using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ThirdPersonGame.PooledObjects
{
    public enum PoolObjectType
    {
        ATTACKINFO,
        LIGHTSABER_OBJ,
        HAMMER_OBJ,
        LIGHTSABER_VFX,
        HAMMER_VFX
    }

    public class PoolObjectLoader : MonoBehaviour
    {
        public static PoolObject InstantiatePrefab(PoolObjectType objType)
        {
            GameObject obj = null;

            switch(objType)
            {
                case PoolObjectType.ATTACKINFO:
                    {
                        obj = Instantiate(Resources.Load("AttackInfo", typeof(GameObject)) as GameObject);
                        break;
                    }
                case PoolObjectType.LIGHTSABER_OBJ:
                    {
                        obj = Instantiate(Resources.Load("LightSaber", typeof(GameObject)) as GameObject);
                        break;
                    }
                case PoolObjectType.HAMMER_OBJ:
                    {
                        obj = Instantiate(Resources.Load("ThorHammer", typeof(GameObject)) as GameObject);
                        break;
                    }
                case PoolObjectType.LIGHTSABER_VFX:
                    {
                        obj = Instantiate(Resources.Load("LightSaberVFX", typeof(GameObject)) as GameObject);
                        break;
                    }

            }

            return obj.GetComponent<PoolObject>();
        }
    }
}
