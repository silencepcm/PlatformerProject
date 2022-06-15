using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TourelleProjectileScript : MonoBehaviour
{
    public float InitialCharge { get; private set; }
    [Header("General")]
    [Tooltip("Radius of this projectile's collision detection")]
    public float Radius = 0.01f;
    public float GravityDownAcceleration =0;
    [Tooltip("Layers this projectile can collide with")]
    public LayerMask HittableLayers = -1;
    public float speed;
    public float hspeed;
    public float mass;
    const QueryTriggerInteraction k_TriggerInteraction = QueryTriggerInteraction.Collide;
    Vector3 m_LastRootPosition;
    Vector3 target;
    Rigidbody rb;
    List<Collider> m_IgnoredColliders;
    public void EnemyShoot(GameObject owner)
    {
        rb = GetComponent<Rigidbody>();
        rb.MoveRotation(owner.transform.rotation);
        if (owner.name == "Fronde2")
        {
            rb.useGravity = true;
            rb.mass = mass;
            rb.AddForce((Camera.main.transform.position - owner.transform.position) * speed, ForceMode.Force);
            rb.AddForce( Vector3.up* hspeed, ForceMode.Impulse);
        }
        else
        {
            rb.AddForce((Camera.main.transform.position - owner.transform.position) * speed, ForceMode.Force);
        }
        m_LastRootPosition = transform.position;
        m_IgnoredColliders = new List<Collider>();
        // Ignore colliders of owner
        Collider[] ownerColliders = owner.GetComponentsInChildren<Collider>();
        m_IgnoredColliders.AddRange(ownerColliders);
    }

    void Update()
        {

        // Gravity
        if (GravityDownAcceleration > 0)
            {
            // add gravity to the projectile velocity for ballistic effect
           // rb.velocity += Vector3.down * GravityDownAcceleration * Time.deltaTime;
            }

            // Hit detection
            {
                RaycastHit closestHit = new RaycastHit();
                closestHit.distance = Mathf.Infinity;
                bool foundHit = false;

                // Sphere cast
                Vector3 displacementSinceLastFrame = transform.position - m_LastRootPosition;
                RaycastHit[] hits = Physics.SphereCastAll(transform.position, Radius,
                    displacementSinceLastFrame.normalized, displacementSinceLastFrame.magnitude, HittableLayers,
                    k_TriggerInteraction);
            
                foreach (var hit in hits)
                {
                    if (hit.distance < closestHit.distance)
                    {
                        foundHit = true;
                        closestHit = hit;
                    }
                }

                if (foundHit)
                {
                    // Handle case of casting while already inside a collider
                    if (closestHit.distance <= 0f)
                    {
                        closestHit.point = transform.position;
                        closestHit.normal = -transform.forward;
                    }
                if (validHit(closestHit))
                {
                    OnHit(closestHit.point, closestHit.normal, closestHit.collider);
                }
                }
            }

            m_LastRootPosition = transform.position;
        }

    bool validHit(RaycastHit hit)
    {
        if (m_IgnoredColliders != null && m_IgnoredColliders.Contains(hit.collider))
        {
            return false;
        }
        return true;
    }
    void OnHit(Vector3 point, Vector3 normal, Collider collider)
        {
            if(collider.gameObject.tag == "Player")
            {
            collider.gameObject.GetComponent<Unity.FPS.Gameplay.PlayerCharacterController>().OnDamage(transform.position, 7f);
            collider.gameObject.GetComponent<PlayerStatsScript>().OnDamage(10);
            }
        Debug.Log(collider.gameObject.tag);
        if (collider.gameObject.tag != "MainCamera")
        {
            Destroy(this.gameObject);
        }
       
        }
}
