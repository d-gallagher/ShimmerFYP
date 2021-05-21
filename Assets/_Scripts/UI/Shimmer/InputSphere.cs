using System;
using UnityEngine;

namespace UI.Shimmer
{
    public class InputSphere : MonoBehaviour
    {

        public float rotationSpeed = 5f;

        [SerializeField]
        public Vector3 _axis;

        public Transform target;



        private void Update()
        {
            if (!(Math.Abs(Input.GetAxis("Mouse X")) >= 0 || Math.Abs(Input.GetAxis("Mouse Y")) >= 0))
            {
                this.transform.parent.transform.rotation = target.rotation;
            }
        }

        void OnMouseDrag()
        {
            float XaxisRotation = Input.GetAxis("Mouse X") * rotationSpeed;
            float YaxisRotation = Input.GetAxis("Mouse Y") * rotationSpeed;
            float ZAxisRotation = (XaxisRotation + YaxisRotation) / 2;
            if (_axis.x == 1 || _axis.y == 1 || _axis.z == 1)
            {
                // select the axis by which you want to rotate the GameObject
                if (_axis.x == 1)
                {
                    RotateTransforms(Vector3.back, XaxisRotation);
                }
                if (_axis.y == 1)
                {
                    RotateTransforms(Vector3.right, YaxisRotation);
                }
                if (_axis.z == 1)
                {
                    RotateTransforms(Vector3.up, ZAxisRotation);
                }
            }
        }
        private void RotateTransforms(Vector3 axis, float angle)
        {
            this.transform.parent.transform.Rotate(axis, angle);
            target.transform.Rotate(axis, angle);
        }
    }
}
