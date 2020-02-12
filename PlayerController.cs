using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlendTrees {
    public class PlayerController : MonoBehaviour {
        // Start is called before the first frame update
        public static PlayerController Instance { get; private set; }

        public float maxSpeed, maxRotationSpeed;

        public bool aimEnabled {
            get; private set;
        }

        private Rigidbody myRb;
        private Camera myCamera;
        private Animator myAnimator;

        private float myMoveX, myMoveY, myCurrentSpeed;
        private float mySpeedDampTime = 0.1f;

        private void Awake() {
            Instance = this;

            myRb = GetComponent<Rigidbody>();
            myCamera = Camera.main;
            myAnimator = GetComponent<Animator>();
        }

        private void Update() {
            this.getInputs();
            this.updateCharacterAnimator();
        }

        private void FixedUpdate() {
            if (!aimEnabled) {
                this.moveCharacter();
                this.rotateCharacter();
            }
        }

        private void rotateCharacter() {
            Vector3 rotation = new Vector3(myMoveX, 0f, myMoveY);

            // Rotamos el vector para que se ajuste a la rotacion de la camara
            rotation = Quaternion.Euler(0, myCamera.transform.eulerAngles.y, 0) * rotation;

            if(rotation != Vector3.zero) {
                // Obtenemos la rotacion final
                Quaternion quatR = Quaternion.LookRotation(rotation);

                // Interpolacion para que la rotacion se realice de forma suave
                myRb.MoveRotation(Quaternion.Lerp(myRb.rotation, quatR, Time.deltaTime * maxRotationSpeed));
            }
        }

        private void moveCharacter() {
            //Movimiento X-Z del input
            Vector3 movement = new Vector3(myMoveX, 0f, myMoveY);

            //Obtenemos deslazamiento input
            myCurrentSpeed = (movement.magnitude > 1 ? 1 : movement.magnitude); // Podemos usar tambien un Clamp

            // Normalizamos y lo hacemos proporcional a la velocidad por segundo
            movement = movement.normalized * maxSpeed * Time.deltaTime;

            //Rotamos el vector para que se ajuste a la rotacion de la camara
            movement = Quaternion.Euler(0, myCamera.transform.eulerAngles.y, 0) * movement;

            // Desplazamos el personaje
            myRb.MovePosition(transform.position + (movement * myCurrentSpeed));
        }

        private void updateCharacterAnimator() {
            myAnimator.SetFloat("Speed", myCurrentSpeed, mySpeedDampTime, Time.deltaTime);
        }

        private void getInputs() {
            myMoveX = Input.GetAxis("Horizontal");
            myMoveY = Input.GetAxis("Vertical");
        }

        public void setAimCoordinates(float x, float y, bool aimEnabled) {
            this.aimEnabled = aimEnabled;
            myAnimator.SetBool("AimMode", this.aimEnabled);
            myAnimator.SetFloat("AimX", x, mySpeedDampTime, Time.deltaTime);
            myAnimator.SetFloat("AimY", y, mySpeedDampTime, Time.deltaTime);

        }
    }
}
