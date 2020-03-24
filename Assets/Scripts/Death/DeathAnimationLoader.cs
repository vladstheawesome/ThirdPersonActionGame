using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ThirdPersonGame.Death
{
    public class DeathAnimationLoader : MonoBehaviour
    {
        // To hold the scriptable objects of each death animation data
        // 1 or (to) many
        public List<DeathAnimationData> DeathAnimationDataList = new List<DeathAnimationData>();
    }
}
