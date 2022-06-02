using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using Unity.FPS.Game;


namespace Unity.FPS.Game
{
    public class ProjectileBase : MonoBehaviour
    {
        public GameObject Owner { get; private set; }
        public Vector3 InitialPosition { get; private set; }
        public Vector3 InitialDirection { get; private set; }
        public Vector3 InheritedMuzzleVelocity { get; private set; }
        public float InitialCharge { get; private set; }
        Rigidbody rb;
        public UnityAction OnShoot;
        public float GravityDownAcceleration => GameManager.Instance.BulletGravity;

        public float speed = 5f;

        public void Shoot(WeaponController controller)
        {
            Owner = controller.Owner;
            InitialPosition = transform.position;
            InitialCharge = controller.CurrentCharge;
            rb = GetComponent<Rigidbody>();

            float x = Screen.width / 2;
            float y = Screen.height / 2;
            var ray = Camera.main.ScreenPointToRay(new Vector3(x, y, 0));
            InitialDirection = ray.direction;
            InheritedMuzzleVelocity = controller.MuzzleWorldVelocity + ray.direction;
            rb.velocity = (ray.direction)*speed;
            OnShoot?.Invoke();

        }

        private void Update()
        {
            /*   if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 10, layerMask))
               {
                   Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                   Debug.Log("Did Hit");
               }*/

            if (GravityDownAcceleration > 0)
            {
                // add gravity to the projectile velocity for ballistic effect
                rb.velocity += Vector3.down * GravityDownAcceleration * Time.deltaTime;
            }


             /*public void OnCollisionEnter(Collision collision)
            {
                if (collision.collider.tag == "Enemy")
                {
                    Debug.Log("ENEMY");
                }
                Destroy(this);
            }*/
        }
        
    }
}