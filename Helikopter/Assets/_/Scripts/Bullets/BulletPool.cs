using Common;
using System.Collections.Generic;
using UnityEngine;

namespace Helikopter
{
    public class BulletPool : SingletonMonoBehaviour<BulletPool>
    {
        [SerializeField]
        private Bullet bulletPrefab;

        private List<Bullet> avaliableBullets;

        protected override void Awake()
        {
            base.Awake();
            avaliableBullets = new List<Bullet>();
        }

        public Bullet GetBullet()
        {
            if (avaliableBullets.Count > 0)
            {
                Bullet bullet = avaliableBullets[0];
                avaliableBullets.RemoveAt(0);
                return bullet;
            }
            else
            {
                Bullet bullet = Instantiate(bulletPrefab, transform);
                bullet.Initialize(this);
                return bullet;
            }
        }

        public void Release(Bullet bullet)
        {
            bullet.gameObject.SetActive(false);
            avaliableBullets.Add(bullet);
        }
    }
}