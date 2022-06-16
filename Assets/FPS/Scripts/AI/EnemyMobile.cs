using Unity.FPS.Game;
using UnityEngine;
using System.Collections;
namespace Unity.FPS.AI
{
    [RequireComponent(typeof(EnemyController))]
    public class EnemyMobile : MonoBehaviour
    {
        public enum AIState
        {
            Patrol,
            Follow,
            Detect,
            Attack,
            Death,
            Damage
        }
        int Vie = 3;
        public Animator Animator;

        [Tooltip("Fraction of the enemy's attack range at which it will stop moving towards target while attacking")]
        [Range(0f, 1f)]
        public float AttackStopDistanceRatio = 0.5f;

        [Tooltip("The random hit damage effects")]
        public ParticleSystem[] RandomHitSparks;

        public ParticleSystem[] OnDetectVfx;
        public AudioClip OnDetectSfx;

        [Tooltip("Sound played PasGauche")] public AudioClip PasGauche;
        [Tooltip("Sound played PasDroit")] public AudioClip PasDroit;
        float speedTemp=0f;
        public AIState AiState { get; private set; }
        EnemyController m_EnemyController;
        public AudioSource m_AudioSource;

        const string k_AnimRunParameter = "Run";
        const string k_AnimWalkParameter = "Walk";
        const string k_AnimAttackParameter = "Attack";
        const string k_AnimAlertedParameter = "PlayerSeen";
        const string k_AnimOnDamagedParameter = "OnDamaged";
        const string k_AnimOnDeathParameter = "Death";
        public float WalkSpeed { get; set; }
        public float RunSpeed { get; set; }
        float lastTimeSpeed;

        float fuiteDelayPathDestination = 5f;
        float actualfuitetimePathDestination = 0f;
        float multiplierFuite = 50f;
        void Start()
        {
            m_EnemyController = GetComponent<EnemyController>();
            DebugUtility.HandleErrorIfNullGetComponent<EnemyController, EnemyMobile>(m_EnemyController, this,
                gameObject);

            m_EnemyController.onAttack += OnAttack;
            m_EnemyController.onDetectedTarget += OnDetectedTarget;
            m_EnemyController.onLostTarget += OnLostTarget;
            m_EnemyController.SetPathDestinationToClosestNode();
            lastTimeSpeed = 0f;
            // Start patrolling
            AiState = AIState.Patrol;

        }

        void Update()
        {
            UpdateAiStateTransitions();
            UpdateCurrentAiState();
            float moveSpeed = m_EnemyController.NavMeshAgent.velocity.magnitude;
            
            bool walking = AiState == AIState.Patrol && Vector3.Distance(transform.position, m_EnemyController.GetDestinationOnPath())>1f;
            bool running = AiState == AIState.Follow && Vector3.Distance(transform.position, m_EnemyController.GetDestinationOnPath()) > m_EnemyController.NavMeshAgent.stoppingDistance && m_EnemyController.NavMeshAgent.speed>1f;
            // Update animator speed parameter
            Animator.SetBool(k_AnimRunParameter, running);
            Animator.SetBool(k_AnimWalkParameter, walking);


            lastTimeSpeed = moveSpeed;
        }
        public void PasGaucheSound()
        {
            m_AudioSource.PlayOneShot(PasGauche);
        }
        public void PasDroitSound()
        {
            m_AudioSource.PlayOneShot(PasDroit);
        }
        void UpdateAiStateTransitions()
        {
            // Handle transitions 
            switch (AiState)
            {
                case AIState.Follow:
                    // Transition to attack when there is a line of sight to the target
                    if (m_EnemyController.IsSeeingTarget && m_EnemyController.IsTargetInAttackRange)
                    {
                        AiState = AIState.Attack;
                        m_EnemyController.SetNavDestination(transform.position);
                    }

                    break;
                case AIState.Attack:
                    // Transition to follow when no longer a target in attack range
                    if (!m_EnemyController.IsTargetInAttackRange)
                    {
                        AiState = AIState.Follow;
                    }

                    break;
            }
        }

        void UpdateCurrentAiState()
        {
            // Handle logic 
            switch (AiState)
            {
                case AIState.Patrol:
                    m_EnemyController.UpdatePathDestination();
                    m_EnemyController.SetNavDestination(m_EnemyController.GetDestinationOnPath());
                    m_EnemyController.OrientTowards(m_EnemyController.GetDestinationOnPath());
                    break;
                case AIState.Follow:
                    if (m_EnemyController.enemyType == EnemyController.EnemyType.Brute)
                    {
                        m_EnemyController.SetNavDestination(m_EnemyController.Player.transform.position);
                        m_EnemyController.OrientTowards(m_EnemyController.Player.transform.position);
                    } else if ((m_EnemyController.enemyType == EnemyController.EnemyType.Fronde)/*&&(Time.time>actualfuitetimePathDestination+fuiteDelayPathDestination)*/)
                    {
                        //actualfuitetimePathDestination = Time.time;
                        m_EnemyController.SetNavDestination((transform.position - m_EnemyController.Player.transform.position)*multiplierFuite);
                        Debug.Log((transform.position - m_EnemyController.Player.transform.position) * multiplierFuite);
                        m_EnemyController.OrientTowards(transform.position -  m_EnemyController.Player.transform.position);
                    }
                    break;
                case AIState.Attack:
                    if (Vector3.Distance(m_EnemyController.Player.transform.position,
                            m_EnemyController.DetectionSourcePoint.position)
                        >= (AttackStopDistanceRatio * m_EnemyController.AttackRange))
                    {
                        m_EnemyController.SetNavDestination(m_EnemyController.Player.transform.position);
                    }
                    else
                    {
                        m_EnemyController.SetNavDestination(transform.position);
                    }

                    m_EnemyController.OrientTowards(m_EnemyController.Player.transform.position);
                    m_EnemyController.TryAtack(m_EnemyController.Player.transform.position);
                    break;
            }
        }

        void OnAttack()
        {
            Animator.SetTrigger(k_AnimAttackParameter);
        }
        void OnDetectedTarget()
        {
            if (AiState == AIState.Patrol)
            {
                AiState = AIState.Detect;
                speedTemp = GameManager.Instance.BruteRunSpeed;
                m_EnemyController.NavMeshAgent.enabled = false;
            }

            for (int i = 0; i < OnDetectVfx.Length; i++)
            {
                OnDetectVfx[i].Play();
            }

            if (OnDetectSfx)
            {
                AudioUtility.CreateSFX(OnDetectSfx, transform.position, AudioUtility.AudioGroups.EnemyDetection, 1f);
            }

            Animator.SetTrigger(k_AnimAlertedParameter);
        }

        void OnLostTarget()
        {
            if (AiState == AIState.Follow || AiState == AIState.Attack)
            {
                AiState = AIState.Patrol;
                m_EnemyController.NavMeshAgent.speed = GameManager.Instance.BruteWalkSpeed;
            }

            for (int i = 0; i < OnDetectVfx.Length; i++)
            {
                OnDetectVfx[i].Stop();
            }

        }

        public void OnDamaged(int degats)
        {
            Debug.Log(Vie);
            AiState = AIState.Damage;
            m_EnemyController.NavMeshAgent.enabled = false;


            if (RandomHitSparks.Length > 0)
            {
                int n = Random.Range(0, RandomHitSparks.Length - 1);
                RandomHitSparks[n].Play();
            }
            Vie-=degats;
            if (Vie > 0)
            {
                Animator.SetTrigger(k_AnimOnDamagedParameter);
            } else
            {
                Animator.SetTrigger(k_AnimOnDeathParameter);
                AiState = AIState.Death;
                m_EnemyController.NavMeshAgent.enabled = false;
                Destroy(gameObject, 2f);
            }
            
        }
        public void SetCanMove()
        {
            m_EnemyController.NavMeshAgent.enabled = true;
            if((AiState == AIState.Detect)|| (AiState == AIState.Damage))
            {
                AiState = AIState.Follow;
            }
        }
    }
}