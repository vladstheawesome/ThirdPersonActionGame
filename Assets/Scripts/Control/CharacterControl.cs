using System;
using System.Collections;
using System.Collections.Generic;
using ThirdPersonGame.PooledObjects;
using ThirdPersonGame.Core;
using UnityEngine;
using ThirdPersonGame.Interact;

namespace ThirdPersonGame.Control
{
    public enum TransitionParameter 
    { 
        Move, 
        MoveBack, 
        Jump,
        ForceTransition,
        Grounded,
        Attack,
        StrafeRight,
        StrafeLeft,
        TransitionIndex,
        ShimmyRight,
        ShimmyLeft,
    }

    public class CharacterControl : MonoBehaviour
    {
        public Animator SkinnedMeshAnimator;
        public bool MoveForward;
        public bool MoveBackwards;
        public bool Jump;
        public bool Attack;
        public bool StrafeRight;
        public bool StrafeLeft;
        public bool MoveUp;
        public bool MoveDown;
        public bool ShimmyRight;
        public bool ShimmyLeft;
        public LedgeChecker ledgeChecker;

        public GameObject ColliderEdgePrefab;
        public List<GameObject> BottomSpheres = new List<GameObject>();
        public List<GameObject> FrontSpheres = new List<GameObject>();
        public List<Collider> RagdollParts = new List<Collider>();
        //public List<Collider> CollidingParts = new List<Collider>();

        public float angularSpeed;

        public float GravityMultiplier;
        public float PullMultiplier;

        private List<TriggerDetector> TriggerDetectors = new List<TriggerDetector>();
        private Dictionary<string, GameObject> ChildObjects = new Dictionary<string, GameObject>();
        private Rigidbody rigid;        

        public Rigidbody RIGID_BODY
        {
            get
            {
                if (rigid == null)
                {
                    rigid = GetComponent<Rigidbody>();
                }
                return rigid;
            }
        }

        private void Awake()
        {
            //bool SwitchBack = false;

            /*if (!IsFacingForward())
            {
                SwitchBack = true;
            }

            FaceForward(true);*/

            //SetRagDollParts();
            SetColliderSpheres();

            //if (SwitchBack)
            //{
            //    FaceForward(false);
            //}

            ledgeChecker = GetComponentInChildren<LedgeChecker>();

            RegisterCharacter();
        }

        private void RegisterCharacter()
        {
            if (!CharacterManager.Instance.Characters.Contains(this))
            {
                CharacterManager.Instance.Characters.Add(this);
            }
        }

        public List<TriggerDetector> GetAllTriggers()
        {
            if(TriggerDetectors.Count == 0)
            {
                TriggerDetector[] arr = this.gameObject.GetComponentsInChildren<TriggerDetector>();

                foreach(TriggerDetector d in arr)
                {
                    TriggerDetectors.Add(d);
                }
            }

            return TriggerDetectors;
        }

        /*private IEnumerator Start()
        {
            yield return new WaitForSeconds(5f);
            RIGID_BODY.AddForce(200f * Vector3.up);
            yield return new WaitForSeconds(0.5f);
            TurnOnRagDoll();
        }*/

        public void SetRagDollParts()
        {
            RagdollParts.Clear();

            Collider[] colliders = this.gameObject.GetComponentsInChildren<Collider>();

            foreach (Collider c in colliders)
            {
                if (c.gameObject != this.gameObject)
                {
                    c.isTrigger = true;
                    RagdollParts.Add(c);

                    if (c.GetComponent<TriggerDetector>() == null)
                    {
                        c.gameObject.AddComponent<TriggerDetector>();
                    }
                }
            }
        }

        private void TurnOnRagDoll()
        {
            RIGID_BODY.useGravity = false;
            RIGID_BODY.velocity = Vector3.zero;
            this.gameObject.GetComponent<BoxCollider>().enabled = false;
            SkinnedMeshAnimator.enabled = false;
            SkinnedMeshAnimator.avatar = null;

            foreach (Collider c in RagdollParts)
            {
                c.isTrigger = false;
                c.attachedRigidbody.velocity = Vector3.zero;
            }
        }

        private void SetColliderSpheres()
        {
            BoxCollider box = GetComponent<BoxCollider>();

            float bottom = box.bounds.center.y - box.bounds.extents.y;
            float top = box.bounds.center.y + box.bounds.extents.y;
            float front = box.bounds.center.z + box.bounds.extents.z;
            float back = box.bounds.center.z - box.bounds.extents.z;

            GameObject bottomFront = CreateEdgeSphere(new Vector3(0f, bottom, front));
            GameObject bottomBack = CreateEdgeSphere(new Vector3(0f, bottom, back));
            GameObject topFront = CreateEdgeSphere(new Vector3(0f, top, front));

            // Set the parent of the edge colliders to be the player
            bottomFront.transform.parent = this.transform;
            bottomBack.transform.parent = this.transform;
            topFront.transform.parent = this.transform;

            BottomSpheres.Add(bottomFront);
            BottomSpheres.Add(bottomBack);

            FrontSpheres.Add(bottomFront);
            FrontSpheres.Add(topFront);

            float horsec = (bottomFront.transform.position - bottomBack.transform.position).magnitude / 5f;
            CreateMiddleSpheres(bottomFront, -this.transform.forward, horsec, 4, BottomSpheres);

            float versec = (bottomFront.transform.position - topFront.transform.position).magnitude / 10f;
            CreateMiddleSpheres(bottomFront, this.transform.up, versec, 9, FrontSpheres);
        }

        private void FixedUpdate()
        {
            // Character is going down
            if(RIGID_BODY.velocity.y < 0f)
            {
                RIGID_BODY.velocity += (-Vector3.up * GravityMultiplier);
            }

            // Player let go of Jump button while player was going (Jumping) up
            if(RIGID_BODY.velocity.y > 0f && !Jump)
            {
                RIGID_BODY.velocity += (-Vector3.up * PullMultiplier);
            }
        }

        public void CreateMiddleSpheres(GameObject start, Vector3 dir, float sec, int iterations, List<GameObject> spheresList)
        {
            for (int i = 0; i < iterations; i++)
            {
                Vector3 pos = start.transform.position + (dir * sec * (i + 1));

                GameObject newObj = CreateEdgeSphere(pos);
                newObj.transform.parent = this.transform;
                spheresList.Add(newObj);
            }
        }

        public GameObject CreateEdgeSphere(Vector3 pos)
        {
            GameObject obj = Instantiate(ColliderEdgePrefab, pos, Quaternion.identity);
            return obj;
        }

        public void PlayerMoveForward(float Speed, float SpeedGraph)
        {            
            transform.Translate(Vector3.forward * Speed * SpeedGraph * Time.deltaTime);            
        }

        public void PlayerMoveBackwards(float Speed, float SpeedGraph)
        {
            transform.Translate(-Vector3.forward * SpeedGraph * Speed * Time.deltaTime);
            //transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }

        public void FaceForward(bool forward)
        {
            if (forward)
            {
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            }
        }

        public bool IsFacingForward()
        {
            if (transform.forward.z > 0f)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void PlayerStrafeOrShimmyRight(float Speed, float SpeedGraph)
        {
            transform.Translate(Vector3.right * SpeedGraph * Speed * Time.deltaTime);  
        }

        public void PlayerStrafeOrShimmyLeft(float Speed, float SpeedGraph)
        {
            transform.Translate(-Vector3.right * SpeedGraph * Speed * Time.deltaTime);
        }

        public Collider GetBodyPart(string name)
        {
            foreach (Collider c in RagdollParts)
            {
                if (c.name.Contains(name))
                {
                    return c;
                }
            }

            return null;
        }

        public GameObject GetChildObj(string name)
        {
            if (ChildObjects.ContainsKey(name))
            {
                return ChildObjects[name];
            }

            Transform[] arr = this.gameObject.GetComponentsInChildren<Transform>();

            foreach (Transform t in arr)
            {
                if (t.gameObject.name.Equals(name))
                {
                    ChildObjects.Add(name, t.gameObject);
                    return t.gameObject;
                }
            }

            return null;
        }
    }
}
