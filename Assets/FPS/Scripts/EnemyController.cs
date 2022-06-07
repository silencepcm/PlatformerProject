using System.Collections.Generic;
using System.Linq;
using Unity.FPS.Game;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

namespace Unity.FPS.AI
{
    public class EnemyController : MonoBehaviour
    {


        [Header("Parameters")]
        [Tooltip("The Y height at which the enemy will be automatically killed (if it falls off of the level)")]
        public float SelfDestructYHeight = -20f;

        [Tooltip("The distance at which the enemy considers that it has reached its current path destination point")]
        public float PathReachingRadius = 2f;

        [Tooltip("The speed at which the enemy rotates")]
        public float OrientationSpeed = 10f;

        [Tooltip("Delay after death where the GameObject is destroyed (to allow for animation)")]
        public float DeathDuration = 0f;


        [Tooltip("Time delay between a weapon swap and the next attack")]
        public float DelayAfterWeaponSwap = 0f;


        [Tooltip("The gradient representing the color of the flash on hit")] [GradientUsageAttribute(true)]
        public Gradient OnHitBodyGradient;

        [Tooltip("The duration of the flash on hit")]
        public float FlashOnHitDuration = 0.5f;

        [Header("Sounds")] [Tooltip("Sound played when recieving damages")]
        public AudioClip DamageTick;

        [Header("VFX")] [Tooltip("The VFX prefab spawned when the enemy dies")]
        public GameObject DeathVfx;

        [Tooltip("The point at which the death VFX is spawned")]
        public Transform DeathVfxSpawnPoint;

        [Header("Loot")] [Tooltip("The object this enemy can drop when dying")]
        public GameObject LootPrefab;

        [Tooltip("The chance the object has to drop")] [Range(0, 1)]
        public float DropRate = 1f;

        [Header("Debug Display")] [Tooltip("Color of the sphere gizmo representing the path reaching range")]
        public Color PathReachingRangeColor = Color.yellow;

        [Tooltip("Color of the sphere gizmo representing the attack range")]
        public Color AttackRangeColor = Color.red;

        [Tooltip("Color of the sphere gizmo representing the detection range")]
        public Color DetectionRangeColor = Color.blue;

        public UnityAction onAttack;
        public UnityAction onDetectedTarget;
        public UnityAction onLostTarget;

        float m_LastTimeDamaged = float.NegativeInfinity;

        public PatrolPath PatrolPath { get; set; }
        public GameObject Player;
        public bool IsTargetInAttackRange;
        public bool IsSeeingTarget;
        public bool HadKnownTarget;
        public NavMeshAgent NavMeshAgent { get; private set; }
        public DetectionModule DetectionModule { get; private set; }

        int m_PathDestinationNodeIndex;
        //ActorsManager m_ActorsManager;
        //Health m_Health;
        //Actor m_Actor;
        Collider[] m_SelfColliders;
        GameFlowManager m_GameFlowManager;
        bool m_WasDamagedThisFrame;
        public WeaponController CurrentWeapon;
        public Transform DetectionSourcePoint;
        [Tooltip("The max distance at which the enemy can see targets")]
        public float DetectionRange = 20f;
        bool CanShoot;
        public float AttackDelay;
        float LastTimeAttack;
        [Tooltip("The max distance at which the enemy can attack its target")]
        public float AttackRange = 10f;

        [Tooltip("Time before an enemy abandons a known target that it can't see anymore")]
        public float KnownTargetTimeout = 4f;
        protected float TimeLastSeenTarget = Mathf.NegativeInfinity;
        bool detected;
        public Animator Animator;

        const string k_AnimAttackTrigger = "Attack";
        const string k_AnimOnDamagedTrigger = "OnDamaged";
        public enum EnemyType { Brute, Tourelle, Fronde};
        public EnemyType enemyType;
        float WalkSpeed;
        float RunSpeed;
        void ImportParams()
        {
            switch (enemyType)
            {
                case EnemyType.Brute:
                    WalkSpeed = GameManager.Instance.BruteWalkSpeed;
                    RunSpeed = GameManager.Instance.BruteRunSpeed;
                    NavMeshAgent.angularSpeed = GameManager.Instance.BruteAngleSpeed;
                    NavMeshAgent.speed = GameManager.Instance.BruteWalkSpeed;
                    NavMeshAgent.acceleration = GameManager.Instance.BruteAcceleration;
                    break;
                case EnemyType.Tourelle:
                    break;
                case EnemyType.Fronde:

                    break;
            }
        }
        void Start()
        {
            Player = GameObject.FindGameObjectWithTag("Player");

            FindObjectOfType<Unity.FPS.Gameplay.ToyboxScript>().setEnemyParamsUpdate += ImportParams;
            //m_EnemyManager = FindObjectOfType<EnemyManager>();
             
            //m_EnemyManager.RegisterEnemy(this);



            NavMeshAgent = GetComponent<NavMeshAgent>();
            m_SelfColliders = GetComponentsInChildren<Collider>();

            m_GameFlowManager = FindObjectOfType<GameFlowManager>();

            var detectionModules = GetComponentsInChildren<DetectionModule>();
            DebugUtility.HandleErrorIfNoComponentFound<DetectionModule, EnemyController>(detectionModules.Length, this,
                gameObject);
            DebugUtility.HandleWarningIfDuplicateObjects<DetectionModule, EnemyController>(detectionModules.Length,
                this, gameObject);
            // Initialize detection module
            DetectionModule = detectionModules[0];
            onAttack += OnAttack;
            onDetectedTarget += OnDetectedTarget;
            ImportParams();

             /*   NavMeshAgent.speed = m_NavigationModule.MoveSpeed;
                NavMeshAgent.angularSpeed = m_NavigationModule.AngularSpeed;
                NavMeshAgent.acceleration = m_NavigationModule.Acceleration;*/
            
        }

        void Update()
        {
            
            EnsureIsWithinLevelBounds();

            HandleTargetDetection();


            m_WasDamagedThisFrame = false;
        }






        public void HandleTargetDetection()
        {
            // Handle known target detection timeout
            if (Player && !IsSeeingTarget && (Time.time - TimeLastSeenTarget) > KnownTargetTimeout)
            {
                detected = false;
            }

            // Find the closest visible hostile actor
            float sqrDetectionRange = DetectionRange * DetectionRange;
            IsSeeingTarget = false;
            float closestSqrDistance = Mathf.Infinity;
            float DistanceToPlayer = Vector3.Distance(Player.transform.position, DetectionSourcePoint.position);
            if (DistanceToPlayer < sqrDetectionRange && DistanceToPlayer < closestSqrDistance)
            {
                // Check for obstructions
                RaycastHit[] hits = Physics.RaycastAll(DetectionSourcePoint.position,
                    (Player.transform.Find("AimPoint").position - DetectionSourcePoint.position).normalized, DetectionRange,
                    -1, QueryTriggerInteraction.Ignore);
                RaycastHit closestValidHit = new RaycastHit();
                closestValidHit.distance = Mathf.Infinity;
                bool foundValidHit = false;
                foreach (var hit in hits)
                {
                    if (!m_SelfColliders.Contains(hit.collider) && hit.distance < closestValidHit.distance)
                    {
                        closestValidHit = hit;
                        foundValidHit = true;
                    }
                }

                if (foundValidHit)
                {
                    IsSeeingTarget = true;
                    closestSqrDistance = DistanceToPlayer;

                    TimeLastSeenTarget = Time.time;
                }
            }

            IsTargetInAttackRange = Player != null &&
                                    Vector3.Distance(transform.position, Player.transform.position) <=
                                    AttackRange;

            // Detection events
            if (!HadKnownTarget &&
                !detected)
            {
                OnDetect();
            }

            if (HadKnownTarget &&
                Player == null)
            {
                OnLostTarget();
            }
        }


        public virtual void OnDetect() => onDetectedTarget?.Invoke();

        public virtual void OnAttack()
        {
            if (Animator)
            {
                Animator.SetTrigger(k_AnimAttackTrigger);
            }
        }

        public virtual void OnDamaged(float damage, GameObject damageSource)
        {
            // test if the damage source is the player
            if (damageSource && !damageSource.GetComponent<EnemyController>())
            {
                // pursue the player
                TimeLastSeenTarget = Time.time;
                if (Animator)
                {
                    Animator.SetTrigger(k_AnimOnDamagedTrigger);
                }

                m_LastTimeDamaged = Time.time;

                // play the damage tick sound
                if (DamageTick && !m_WasDamagedThisFrame)
                    AudioUtility.CreateSFX(DamageTick, transform.position, AudioUtility.AudioGroups.DamageTick, 0f);

                m_WasDamagedThisFrame = true;
            }
        }







        void EnsureIsWithinLevelBounds()
        {
            // at every frame, this tests for conditions to kill the enemy
            if (transform.position.y < SelfDestructYHeight)
            {
                Destroy(gameObject);
                return;
            }
        }

        void OnLostTarget()
        {
            onLostTarget.Invoke();

        }

        void OnDetectedTarget()
        {
            //onDetectedTarget.Invoke();

        }

        public void OrientTowards(Vector3 lookPosition)
        {
            Vector3 lookDirection = Vector3.ProjectOnPlane(lookPosition - transform.position, Vector3.up).normalized;
            if (lookDirection.sqrMagnitude != 0f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
                transform.rotation =
                    Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * OrientationSpeed);
            }
        }

        bool IsPathValid()
        {
            return PatrolPath && PatrolPath.PathNodes.Count > 0;
        }

        public void ResetPathDestination()
        {
            m_PathDestinationNodeIndex = 0;
        }

        public void SetPathDestinationToClosestNode()
        {
            if (IsPathValid())
            {
                int closestPathNodeIndex = 0;
                for (int i = 0; i < PatrolPath.PathNodes.Count; i++)
                {
                    float distanceToPathNode = PatrolPath.GetDistanceToNode(transform.position, i);
                    if (distanceToPathNode < PatrolPath.GetDistanceToNode(transform.position, closestPathNodeIndex))
                    {
                        closestPathNodeIndex = i;
                    }
                }

                m_PathDestinationNodeIndex = closestPathNodeIndex;
            }
            else
            {
                m_PathDestinationNodeIndex = 0;
            }
        }

        public Vector3 GetDestinationOnPath()
        {
            if (IsPathValid())
            {
                return PatrolPath.GetPositionOfPathNode(m_PathDestinationNodeIndex);
            }
            else
            {
                return transform.position;
            }
        }

        public void SetNavDestination(Vector3 destination)
        {
            if (NavMeshAgent)
            {
                NavMeshAgent.SetDestination(destination);
            }
        }

        public void UpdatePathDestination(bool inverseOrder = false)
        {
            if (IsPathValid())
            {
                // Check if reached the path destination
                if ((transform.position - GetDestinationOnPath()).magnitude <= PathReachingRadius)
                {
                    // increment path destination index
                    m_PathDestinationNodeIndex =
                        inverseOrder ? (m_PathDestinationNodeIndex - 1) : (m_PathDestinationNodeIndex + 1);
                    if (m_PathDestinationNodeIndex < 0)
                    {
                        m_PathDestinationNodeIndex += PatrolPath.PathNodes.Count;
                    }

                    if (m_PathDestinationNodeIndex >= PatrolPath.PathNodes.Count)
                    {
                        m_PathDestinationNodeIndex -= PatrolPath.PathNodes.Count;
                    }
                }
            }
        }

        void OnDie()
        {
            // spawn a particle system when dying
            var vfx = Instantiate(DeathVfx, DeathVfxSpawnPoint.position, Quaternion.identity);
            Destroy(vfx, 5f);

            // tells the game flow manager to handle the enemy destuction
            //m_EnemyManager.UnregisterEnemy(this);

            // loot an object
            if (TryDropItem())
            {
                Instantiate(LootPrefab, transform.position, Quaternion.identity);
            }

            // this will call the OnDestroy function
            Destroy(gameObject, DeathDuration);
        }

        void OnDrawGizmosSelected()
        {
            // Path reaching range
            Gizmos.color = PathReachingRangeColor;
            Gizmos.DrawWireSphere(transform.position, PathReachingRadius);

            if (DetectionModule != null)
            {
                // Detection range
                Gizmos.color = DetectionRangeColor;
                Gizmos.DrawWireSphere(transform.position, DetectionModule.DetectionRange);

                // Attack range
                Gizmos.color = AttackRangeColor;
                Gizmos.DrawWireSphere(transform.position, DetectionModule.AttackRange);
            }
        }

        public bool TryAtack(Vector3 enemyPosition)
        {
            if (m_GameFlowManager.GameIsEnding)
                return false;
            bool didFire;
            if (CanShoot)
            {
                didFire = CurrentWeapon.TryShoot();
                if (didFire && onAttack != null)
                {
                    onAttack.Invoke();
                }
            }
            else
            {
                if (LastTimeAttack + AttackDelay < Time.time && onAttack != null)
                {
                    onAttack.Invoke();
                    LastTimeAttack = Time.time;
                    didFire = true;
                } else didFire = false;
            }
            return didFire;
        }

        public bool TryDropItem()
        {
            if (DropRate == 0 || LootPrefab == null)
                return false;
            else if (DropRate == 1)
                return true;
            else
                return (Random.value <= DropRate);
        }

    }
}