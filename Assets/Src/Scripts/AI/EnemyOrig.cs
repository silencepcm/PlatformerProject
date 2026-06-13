using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOrig : MonoBehaviour
{
    /*
    public float SelfDestructYHeight = -20f;

    Tooltip("The distance at which the enemy considers that it has reached its current path destination point")]
        public float StopPointDistance = 2f;

    [Tooltip("The speed at which the enemy rotates")]
    public float OrientationSpeed = 10f;

    [Tooltip("Delay after death where the GameObject is destroyed (to allow for animation)")]
    public float DeathDuration = 0f;



    [Tooltip("Time delay between a weapon swap and the next attack")]
    public float DelayAfterWeaponSwap = 0f;

    [Header("Eye color")]
    [Tooltip("Material for the eye color")]
    public Material EyeColorMaterial;

    [Tooltip("The default color of the bot's eye")]
    [ColorUsageAttribute(true, true)]
    public Color DefaultEyeColor;

    [Tooltip("The attack color of the bot's eye")]
    [ColorUsageAttribute(true, true)]
    public Color AttackEyeColor;

    [Header("Flash on hit")]
    [Tooltip("The material used for the body of the hoverbot")]
    public Material BodyMaterial;

    [Tooltip("The gradient representing the color of the flash on hit")]
    [GradientUsageAttribute(true)]
    public Gradient OnHitBodyGradient;

    [Tooltip("The duration of the flash on hit")]
    public float FlashOnHitDuration = 0.5f;

    [Header("Sounds")]
    [Tooltip("Sound played when recieving damages")]
    public AudioClip DamageTick;

    [Header("VFX")]
    [Tooltip("The VFX prefab spawned when the enemy dies")]
    public GameObject DeathVfx;

    [Tooltip("The point at which the death VFX is spawned")]
    public Transform DeathVfxSpawnPoint;

    [Header("Loot")]
    [Tooltip("The object this enemy can drop when dying")]
    public GameObject LootPrefab;



    public UnityAction onAttack;
    public UnityAction onDetectedTarget;
    public UnityAction onLostTarget;
    public UnityAction onDamaged;
    public PatrolPath PatrolPath { get; set; }
    public bool IsSeeingTarget;
    public NavMeshAgent NavMeshAgent { get; private set; }

    int m_PathDestinationNodeIndex;
    EnemyManager m_EnemyManager;
    Collider[] m_SelfColliders;
    GameFlowManager m_GameFlowManager;
    bool m_WasDamagedThisFrame;
    float m_LastTimeWeaponSwapped = Mathf.NegativeInfinity;
    int m_CurrentWeaponIndex;
    WeaponController m_CurrentWeapon;
    WeaponController[] m_Weapons;
    void Start()
    {
        m_EnemyManager = FindObjectOfType<EnemyManager>();
        DebugUtility.HandleErrorIfNullFindObject<EnemyManager, EnemyController>(m_EnemyManager, this);

        onAttack += EnemyAttackEvent();

        m_EnemyManager.RegisterEnemies(this);



        NavMeshAgent = GetComponent<NavMeshAgent>();
        m_SelfColliders = GetComponentsInChildren<Collider>();

        m_GameFlowManager = FindObjectOfType<GameFlowManager>();
        DebugUtility.HandleErrorIfNullFindObject<GameFlowManager, EnemyController>(m_GameFlowManager, this);

        // Subscribe to damage & death actions
        //m_Health.OnDie += OnDie;
        //m_Health.OnDamaged += OnDamaged;

        // Find and initialize all weapons
        FindAndInitializeAllWeapons();
        var weapon = GetCurrentWeapon();
        weapon.ShowWeapon(true);

        onDetectedTarget += OnDetectedPlayer;
        onLostTarget += OnLostPlayer;
        onAttack += OnAttack;
        NavMeshAgent.speed = m_NavigationModule.MoveSpeed;
        NavMeshAgent.angularSpeed = m_NavigationModule.AngularSpeed;
        NavMeshAgent.acceleration = m_NavigationModule.Acceleration;


    }

    void Update()
    {
        EnsureIsWithinLevelBounds();

        HandleTargetDetection();


        m_WasDamagedThisFrame = false;
    }
    void EnsureIsWithinLevelBounds()
    {
        if (transform.position.y < SelfDestructYHeight)
        {
            Destroy(gameObject);
            return;
        }
    }*/
}
