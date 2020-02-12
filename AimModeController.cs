using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace BlendTrees {

    public class AimModeController : MonoBehaviour {

        public float aimPrecision, sliceTime, sliceDelay;

        private Vector3 myCurrentDirection;

        private bool myAimMode;

        private float myAimX, myAimY;
        private bool mySlice, myHasSliced;

        private void Awake() {
            aimPrecision = sliceTime = 0.05f;
            sliceDelay = 0.25f;
        }

        private void Update() {
            this.getInputs();
            this.aim();
            this.slice();
        }

        private void slice() {
            if(mySlice && !myHasSliced) {
                DOTween.To(() => myAimX, x => myAimX = x, (myAimX * -1), sliceTime).SetEase(Ease.Linear).Play();
                DOTween.To(() => myAimX, y => myAimY = y, (myAimY * -1), sliceTime).SetEase(Ease.Linear).Play();

                StartCoroutine(sliceCooldown());
            }
        }

        private void aim() {
            PlayerController.Instance.setAimCoordinates(myAimX, myAimY, myAimMode);
        }

        private void getInputs() {
            myAimX += Input.GetAxis("Mouse X") * aimPrecision;
            myAimY += Input.GetAxis("Mouse Y") * aimPrecision;

            myAimX = Mathf.Clamp(myAimX, -1, 1);
            myAimX = Mathf.Clamp(myAimY, -1, 1);

            myAimMode = Input.GetKey(KeyCode.Mouse1) || Input.GetKey(KeyCode.Joystick1Button4);
            mySlice = myAimMode && (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Joystick1Button5));

        }

    private IEnumerator sliceCooldown() {
            myHasSliced = true;
            yield return new WaitForSeconds(sliceDelay);
            myHasSliced = false;
        }
    }
}
