using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyTweens {
    public abstract class TweenBase : MonoBehaviour {
        public float duration, startValue, endValue, delaySeconds;
        public Ease easeCurveScript;

        public bool playOnStart = false;

        private float m_timePlayed = 0;

        private void Start() {
            Debug.LogError("blbb");
            play();
            if (playOnStart) {
                this.play();
            } else {

            }
        }

        public void play() {
            StartCoroutine(playTween());
        }

        public bool hasEnded() {
            return (m_timePlayed >= duration);
        }

        private IEnumerator playTween() {
            yield return new WaitForSeconds(delaySeconds);

            float timeValue = 0;
            float changeValue = endValue - startValue;

            while (!hasEnded()) {
                timeValue = easeCurveScript.Interpolate(m_timePlayed, startValue, changeValue, duration);
                ApplyTween(timeValue);
                m_timePlayed += Time.deltaTime;
                yield return 1;
            }
        }

        protected abstract void Init();
        protected abstract void ApplyTween(float timeValue);
    }

}