using System.Collections;
using System.Collections.Generic;
using Unity.FPS.Game;
using UnityEngine;

public class TourelleControllerScript : MonoBehaviour
{
    public enum AIState
    {
        Idle,
        Attack,
        Damaged,
        Death
    }
    public AIState AiState;
    public Animator animator;
    float m_TimeStartedDetection;
    float m_TimeLostDetection;
    public float DetectionFireDelay = 8f;
    private GameObject Player;
    public Transform TurretAimPoint;
    Quaternion m_PreviousPivotAimingRotation;
    Quaternion m_PivotAimingRotation;
    int Vie = 2;
    public float AimRotationSharpness = 5f;
    public float LookAtRotationSharpness = 2.5f;
    Quaternion m_RotationWeaponForwardToPivot;
    public float AimingTransitionBlendTime = 1f;
    public ParticleSystem[] OnDetectVfx;
    public AudioClip OnDetectSfx;
    public Transform TurretPivot;
    float AttackRange = 4;
    float DetectionRange = 5;
    public GameObject ProjectilePrefab;
    float OrientationSpeed = 5f;
    Vector3 whereShoot;
    void Start()
    {
        Player = GameObject.Find("Player");
        FindObjectOfType<Unity.FPS.Gameplay.ToyboxScript>().setEnemyParamsUpdate += ImportParams;
        ImportParams();
    }
    void ImportParams()
    {

        //NavMeshAgent.angularSpeed = GameManager.Instance.TourelleAngleSpeed;
        DetectionFireDelay = 2.5f;
        AttackRange = 14f;
        //NavMeshAgent.speed = 0f;
        DetectionRange = 17f;

    }
    // Update is called once per frame
    void Update()
    {
        UpdateTurretAiming();
        // Handle logic 
        switch (AiState)
        {
            case AIState.Attack:
                OrientTowards(Player.transform.position);
                bool mustShoot = Vector3.Distance(transform.position, Player.transform.position)<AttackRange && Time.time > m_TimeStartedDetection + DetectionFireDelay;
                // Calculate the desired rotation of our turret (aim at target)
                Vector3 directionToTarget =
                    (Player.transform.position - TurretAimPoint.position).normalized;
                Quaternion offsettedTargetRotation =
                    Quaternion.LookRotation(directionToTarget) * m_RotationWeaponForwardToPivot;
                m_PivotAimingRotation = Quaternion.Slerp(m_PreviousPivotAimingRotation, offsettedTargetRotation,
                    (mustShoot ? AimRotationSharpness : LookAtRotationSharpness) * Time.deltaTime);

                // shoot
                if (mustShoot)
                {
                    Vector3 correctedDirectionToTarget =
                        (m_PivotAimingRotation * Quaternion.Inverse(m_RotationWeaponForwardToPivot)) *
                        Vector3.forward;
                    m_TimeStartedDetection = Time.time;
                    ShootAnimation(Camera.main.transform.position);
                } else if (Vector3.Distance(transform.position, Player.transform.position) > DetectionRange)
                {
                    OnLostTarget();
                }

                break;
            case AIState.Idle:
                if(Vector3.Distance(transform.position, Player.transform.position) < DetectionRange)
                {
                    OnDetectedTarget();
                }
                break;
        }
    }
    public void OrientTowards(Vector3 lookPosition)
    {
        Vector3 lookDirection = Vector3.ProjectOnPlane(lookPosition - transform.position, Vector3.up).normalized;
        if (lookDirection.sqrMagnitude != 0f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
            transform.parent.rotation =
                Quaternion.Slerp(transform.parent.rotation, targetRotation, Time.deltaTime * OrientationSpeed);
        }
    }
    void UpdateTurretAiming()
    {
        switch (AiState)
        {
            case AIState.Attack:
                TurretPivot.rotation = m_PivotAimingRotation;
                break;
            default:
                // Use the turret rotation of the animation
                TurretPivot.rotation = Quaternion.Slerp(m_PivotAimingRotation, TurretPivot.rotation,
                    (Time.time - m_TimeLostDetection) / AimingTransitionBlendTime);
                break;
        }

        m_PreviousPivotAimingRotation = TurretPivot.rotation;
    }
    void ShootAnimation(Vector3 where)
    {
        animator.SetTrigger("Attack");
        whereShoot = where;
    }
    public void Shoot()
    {
        GameObject newProjectile = Instantiate(ProjectilePrefab, TurretPivot.position,
           Quaternion.identity);
        newProjectile.GetComponent<TourelleProjectileScript>().EnemyShoot(gameObject);
    }
    void OnDie()
    {
        // this will call the OnDestroy function
        AiState = AIState.Death;
        animator.SetTrigger("Death");
        Destroy(gameObject, 4f);
    }
    void OnLostTarget()
    {
        if (AiState == AIState.Attack)
        {
            AiState = AIState.Idle;
        }

        for (int i = 0; i < OnDetectVfx.Length; i++)
        {
            OnDetectVfx[i].Stop();
        }

        animator.SetTrigger("PlayerLost");
        m_TimeLostDetection = Time.time;
    }
    public void OnDamaged()
    {
        AiState = AIState.Damaged;
        StartCoroutine(WaitDamage());
        Vie --;
        animator.SetTrigger("OnDamaged");
    }
    IEnumerator WaitDamage()
    {
        yield return new WaitForSeconds(2f);
        if (Vie <= 0)
        {
            OnDie();
        }
        else
        {
            AiState = AIState.Attack;
        }
    }
    void OnDetectedTarget()
    {
        if (AiState == AIState.Idle)
        {
            AiState = AIState.Attack;
        }

        for (int i = 0; i < OnDetectVfx.Length; i++)
        {
            OnDetectVfx[i].Play();
        }

        if (OnDetectSfx)
        {
            AudioUtility.CreateSFX(OnDetectSfx, transform.position, AudioUtility.AudioGroups.EnemyDetection, 1f);
        }

        animator.SetTrigger("PlayerSeen");
        m_TimeStartedDetection = Time.time;
    }
}
