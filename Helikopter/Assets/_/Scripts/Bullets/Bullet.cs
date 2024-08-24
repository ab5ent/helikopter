using UnityEngine;

namespace Helikopter
{
    public class Bullet : MonoBehaviour
    {
        [field: SerializeField]
        public TrailRenderer BulletTrailRenderer { get; private set; }

        private BulletPool pool;

        public void Initialize(BulletPool bulletPool)
        {
            pool = bulletPool;
            gameObject.SetActive(false);
        }

        public void Return()
        {
            pool.Release(this);
        }
    }
}