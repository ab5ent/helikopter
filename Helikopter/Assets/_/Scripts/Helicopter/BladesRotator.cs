using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Helikopter
{
    public class BladesRotator : MonoBehaviour
    {
        public enum Axis
        {
            X,
            Y,
            Z
        }

        [SerializeField]
        private Axis rotationAxis;

        [field: SerializeField]
        public float BladeSpeed { get; set; }

        [SerializeField]
        private bool inverseRotation = false;

        private Vector3 Rotation;

        private float rotationDegree;

        private void Start()
        {
            Rotation -= transform.localEulerAngles;
        }

        private void Update()
        {
            if (inverseRotation)
            {
                rotationDegree -= BladeSpeed * Time.deltaTime;
            }
            else
            {
                rotationDegree += BladeSpeed * Time.deltaTime;
            }

            rotationDegree %= 360;

            switch (rotationAxis)
            {
                case Axis.X:
                    transform.localRotation = Quaternion.Euler(rotationDegree, Rotation.y, Rotation.z);
                    break;
                case Axis.Y:
                    transform.localRotation = Quaternion.Euler(Rotation.x, rotationDegree, Rotation.z);
                    break;
                case Axis.Z:
                    transform.localRotation = Quaternion.Euler(Rotation.x, Rotation.y, rotationDegree);
                    break;
                default:
                    break;
            }
        }
    }
}