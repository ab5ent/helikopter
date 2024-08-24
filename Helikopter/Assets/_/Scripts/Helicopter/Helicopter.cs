using UnityEngine;

namespace Helikopter
{
    public class Helicopter : MonoBehaviour
    {
        [SerializeField]
        private HelicopterMainEngine mainEngine;

        [SerializeField]
        private HelicopterAutoAim autoAim;

        [SerializeField]
        private HelicopterMachineGun[] guns;

        public void Initialize()
        {
            for (int i = 0; i < guns.Length; i++)
                guns[i].Initialize(autoAim);
        }

        public void StartEngine() => mainEngine.StartEngine();

        public void SetPosition(Transform startPoint)
        {
            GetComponent<Rigidbody>().Move(startPoint.position, startPoint.rotation);
        }
    }
}