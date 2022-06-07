using System.Linq;
using Unity.FPS.Game;
using UnityEngine;
using UnityEngine.Events;

namespace Unity.FPS.AI
{
    public class DetectionModule : MonoBehaviour
    {
        [Tooltip("The point representing the source of target-detection raycasts for the enemy AI")]
        public Transform DetectionSourcePoint;

        [Tooltip("The max distance at which the enemy can see targets")]
        public float DetectionRange = 20f;

        [Tooltip("The max distance at which the enemy can attack its target")]
        public float AttackRange = 10f;

        [Tooltip("Time before an enemy abandons a known target that it can't see anymore")]
        public float KnownTargetTimeout = 4f;

        [Tooltip("Optional animator for OnShoot animations")]
        public Animator Animator;

        public UnityAction onDetectedTarget;
        public UnityAction onLostTarget;

        public GameObject Player { get; private set; }
        public bool IsTargetInAttackRange { get; private set; }
        public bool IsSeeingTarget { get; private set; }
        public bool HadKnownTarget { get; private set; }

        protected float TimeLastSeenTarget = Mathf.NegativeInfinity;
        bool detected;

        const string k_AnimAttackTrigger = "Attack";
        const string k_AnimOnDamagedTrigger = "OnDamaged";

        private void Start()
        {
            Player = GameObject.FindGameObjectWithTag("Player");
        }

        public virtual void HandleTargetDetection(GameObject actor, Collider[] selfColliders)
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
                            if (!selfColliders.Contains(hit.collider) && hit.distance < closestValidHit.distance)
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

        public virtual void OnLostTarget() => onLostTarget?.Invoke();

        public virtual void OnDetect() => onDetectedTarget?.Invoke();

        public virtual void OnDamaged(GameObject damageSource)
        {
            TimeLastSeenTarget = Time.time;
            Player = damageSource;

            if (Animator)
            {
                Animator.SetTrigger(k_AnimOnDamagedTrigger);
            }
        }

        public virtual void OnAttack()
        {
            if (Animator)
            {
                Animator.SetTrigger(k_AnimAttackTrigger);
            }
        }
    }
}