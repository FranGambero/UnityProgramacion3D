using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyTweens {
    public class TweenPositionY : TweenBase {
        protected override void ApplyTween(float timeValue) {
            transform.position = new Vector3(transform.position.x, timeValue, transform.position.z);
        }

        protected override void Init() {
            transform.position = new Vector3(transform.position.x, startValue, transform.position.z);
        }

    }
}
