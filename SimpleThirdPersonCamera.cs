using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlendTrees {

    public class SimpleThirdPersonCamera : MonoBehaviour {

        public static SimpleThirdPersonCamera Instance {
            get; private set;
        }

        public float rotationSpeed = 1f;
        public bool invertXAxes, inverYAxes;

        public Transform target;

        private float m_rotationX, m_rotationY;

        private void Awake() {
            Instance = this;
            invertXAxes = inverYAxes = false;
            m_rotationX = m_rotationY = 0f;

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void LateUpdate() {
            this.rotateCamera();
        }

        private void rotateCamera() {
            m_rotationX += (Input.GetAxis("Mouse X") * rotationSpeed) * (invertXAxes ? -1 : 1);
            m_rotationY += (Input.GetAxis("Mouse Y") * rotationSpeed) * (invertXAxes ? -1 : 1);

            m_rotationY = Mathf.Clamp(m_rotationY, -35, 60);
            transform.LookAt(target);
            target.rotation = Quaternion.Euler(m_rotationY, m_rotationX, 0);
        }
    }
}
