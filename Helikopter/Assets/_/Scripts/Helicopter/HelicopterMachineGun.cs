using System.IO;
using UnityEngine;

namespace Helikopter
{
    public class HelicopterMachineGun : MonoBehaviour
    {
        [SerializeField]
        private Transform bulletStartPoint;

        [SerializeField]
        private LayerMask hittableLayers;

        private HelicopterAutoAim autoAim;

        [SerializeField]
        private float fireRate;

        private float lastFireTime;

        private Ray ray;

        private RaycastHit raycastHit;

        public void Initialize(HelicopterAutoAim helicopterAutoAim)
        {
            autoAim = helicopterAutoAim;
            ray = new Ray();
            //lastFireTime = 
        }

        private void Update()
        {
            Shoot();
        }

        public void Shoot()
        {
            if (!autoAim.Target.Value)
            {
                return;
            }

            if (Time.time - lastFireTime < fireRate)
            {
                return;
            }

            Vector3 targetPosition = autoAim.Target.Value.Center();

            ray.origin = bulletStartPoint.position;
            ray.direction = (targetPosition - bulletStartPoint.position).normalized;

            var bullet = BulletPool.Instance.GetBullet();
            bullet.BulletTrailRenderer.AddPosition(ray.origin);
            bullet.gameObject.SetActive(true);

            if (Physics.Raycast(ray, out raycastHit, 3000, hittableLayers))
            {
                bullet.transform.position = raycastHit.point;
            }

            lastFireTime = Time.time - Random.Range(-0.5f, 0.5f);
        }
    }
}