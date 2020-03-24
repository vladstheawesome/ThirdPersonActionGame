using System.Collections;
using System.Collections.Generic;
using ThirdPersonGame.PooledObjects;
using UnityEngine;

namespace ThirdPersonGame.Core
{
    public class AttackManager : Singleton<AttackManager>
    {
        // This is where a list of all current attacks is going to be stored
        public List<AttackInfo> CurrentAttacks = new List<AttackInfo>();
    }
}
