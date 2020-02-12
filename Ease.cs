namespace MyTweens {
    [System.Serializable]
    public class Ease {
        public EaseCurve curve;

        public float InterpolateSwitch(float time, float baseValue, float change, float duration) {
            float value;

            switch (curve) {
                case EaseCurve.linear:
                    value = Linear(time, baseValue, change, duration);
                    break;

                case EaseCurve.easeInQuad:
                    value = QuadIn(time, baseValue, change, duration);
                    break;

                default:
                    value = 0;
                    break;
            }

            return value;
        }

        delegate float EaseDelegate(float time, float baseValue, float change, float duration);

        //Lo mismo que el switch pero mas compactado
        public float Interpolate(float time, float baseValue, float change, float duration) {
            return methods[(int)curve](time, baseValue, change, duration);
        }

        static EaseDelegate[] methods = new EaseDelegate[] {
            Linear,
            QuadIn,
            QuadOut
        };

        public static float Linear(float t, float b, float c, float d) {
            return c * t / d + b;
        }

        public static float QuadIn(float time, float baseValue, float change, float duration) {
            return change * (time /= duration) * (time) + baseValue;
        }

        public static float QuadOut(float time, float baseValue, float change, float duration) {
            return -change * (time /= duration) *(time - 2) + baseValue;
        }
    }
}
