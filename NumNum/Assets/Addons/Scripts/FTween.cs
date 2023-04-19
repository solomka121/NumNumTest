using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlexusTween {
    public static class FTween {

        public static float InSine(float time) {
            time = Mathf.Clamp01(time);
            return -(float)Mathf.Cos(time / 1f * Mathf.PI / 2f) + 1f;
        }
        public static float OutSine(float time) {
            time = Mathf.Clamp01(time);
            return (float)Mathf.Sin(time / 1f * Mathf.PI / 2f);
        }
        public static float InOutSine(float time) {
            time = Mathf.Clamp01(time);
            return -0.5f * ((float)Mathf.Cos(Mathf.PI * time / 1f) - 1f);
        }
        public static float InQuad(float time) {
            time = Mathf.Clamp01(time);
            return (time /= 1) * time;
        }
        public static float OutQuad(float time) {
            time = Mathf.Clamp01(time);
            return -(time /= 1f) * (time - 2f);
        }
        public static float InOutQuad(float time) {
            time = Mathf.Clamp01(time);
            if ((time /= 1 * 0.5f) < 1f) return 0.5f * time * time;
            return -0.5f * ((--time) * (time - 2f) - 1f);
        }
        public static float InCubic(float time) {
            time = Mathf.Clamp01(time);
            return (time /= 1f) * time * time;
        }
        public static float OutCubic(float time) {
            time = Mathf.Clamp01(time);
            return ((time = time / 1f - 1f) * time * time + 1f);
        }
        public static float InOutCubic(float time) {
            time = Mathf.Clamp01(time);
            if ((time /= 1f * 0.5f) < 1) return 0.5f * time * time * time;
            return 0.5f * ((time -= 2f) * time * time + 2f);
        }
        public static float InQuart(float time) {
            time = Mathf.Clamp01(time);
            return (time /= 1f) * time * time * time;
        }
        public static float OutQuart(float time) {
            time = Mathf.Clamp01(time);
            return -((time = time / 1f - 1f) * time * time * time - 1f);
        }
        public static float InOutQuart(float time) {
            time = Mathf.Clamp01(time);
            if ((time /= 1f * 0.5f) < 1) return 0.5f * time * time * time * time;
            return -0.5f * ((time -= 2f) * time * time * time - 2f);
        }
        public static float InQuint(float time) {
            time = Mathf.Clamp01(time);
            return (time /= 1f) * time * time * time * time;
        }
        public static float OutQuint(float time) {
            time = Mathf.Clamp01(time);
            return ((time = time / 1f - 1f) * time * time * time * time + 1f);
        }
        public static float InOutQuint(float time) {
            time = Mathf.Clamp01(time);
            if ((time /= 1f * 0.5f) < 1) return 0.5f * time * time * time * time * time;
            return 0.5f * ((time -= 2f) * time * time * time * time + 2f);
        }
        public static float InExpo(float time) {
            time = Mathf.Clamp01(time);
            return (time == 0f) ? 0f : (float)Mathf.Pow(2f, 10f * (time / 1f - 1));
        }
        public static float OutExpo(float time) {
            time = Mathf.Clamp01(time);
            if (time == 1f) return 1f;
            return (-(float)Mathf.Pow(2f, -10f * time / 1f) + 1f);

        }
        public static float InOutExpo(float time) {
            time = Mathf.Clamp01(time);
            if (time == 0f) return 0f;
            if (time == 1f) return 1f;
            if ((time /= 1f * 0.5f) < 1) return 0.5f * (float)Mathf.Pow(2f, 10f * (time - 1f));
            return 0.5f * (-(float)Mathf.Pow(2f, -10f * --time) + 2f);
        }
        public static float InCirc(float time) {
            time = Mathf.Clamp01(time);
            return -((float)Mathf.Sqrt(1f - (time /= 1f) * time) - 1f);
        }
        public static float OutCirc(float time) {
            time = Mathf.Clamp01(time);
            return (float)Mathf.Sqrt(1f - (time = time / 1f - 1f) * time);
        }
        public static float InOutCirc(float time) {
            time = Mathf.Clamp01(time);
            if ((time /= 1f * 0.5f) < 1f) return -0.5f * ((float)Mathf.Sqrt(1f - time * time) - 1f);
            return 0.5f * ((float)Mathf.Sqrt(1f - (time -= 2f) * time) + 1f);
        }
        public static float InBack(float time, float amplitude) { 
            time = Mathf.Clamp01(time);
            return (time /= 1f) * time * ((amplitude + 1f) * time - amplitude);
        }
        public static float OutBack(float time, float amplitude) { 
            time = Mathf.Clamp01(time);
            return ((time = time / 1f - 1f) * time * ((amplitude + 1) * time + amplitude) + 1f);
        }
        public static float InOutBack(float time, float amplitude) { 
            time = Mathf.Clamp01(time);
            if ((time /= 1f * 0.5f) < 1f) return 0.5f * (time * time * (((amplitude *= (1.525f)) + 1f) * time - amplitude));
            return 0.5f * ((time -= 2f) * time * (((amplitude *= (1.525f)) + 1f) * time + amplitude) + 2f);
        }
        public static float InElastic(float time, float amplitude = 1.5f, float period = 1f) {
            time = Mathf.Clamp01(time);
            float s0;
            if (time == 0f) return 0f;
            if ((time /= 1f) == 1f) return 1f;
            if (period == 0f) period = 1f * 0.3f;
            if (amplitude < 1f) {
                amplitude = 1f;
                s0 = period / 4f;
            } else s0 = period / Mathf.PI*2f * (float)Mathf.Asin(1f / amplitude);
            return -(amplitude * (float)Mathf.Pow(2f, 10f * (time -= 1f)) * (float)Mathf.Sin((time * 1f - s0) * Mathf.PI / period));
        }
        public static float OutElastic(float time, float amplitude = 1.5f, float period = 1f) {
            time = Mathf.Clamp01(time);
            float s1;
            if (time == 0f) return 0f;
            if ((time /= 1f) == 1f) return 1f;
            if (period == 0f) period = 1f * 0.3f;
            if (amplitude < 1f) {
                amplitude = 1f;
                s1 = period / 4f;
            } else s1 = period / Mathf.PI*2f * (float)Mathf.Asin(1f / amplitude);
            return (amplitude * (float)Mathf.Pow(2f, -10f * time) * (float)Mathf.Sin((time * 1f - s1) * Mathf.PI*2f / period) + 1f);
        }
        public static float InOutElastic(float time, float amplitude = 1.5f, float period = 1f) {
            time = Mathf.Clamp01(time);
            float s;
            if (time == 0f) return 0f;
            if ((time /= 1f * 0.5f) == 2f) return 1f;
            if (period == 0) period = 1f * (0.3f * 1.5f);
            if (amplitude < 1f) {
                amplitude = 1f;
                s = period / 4f;
            } else s = period / Mathf.PI*2f * (float)Mathf.Asin(1f / amplitude);
            if (time < 1) return -0.5f * (amplitude * (float)Mathf.Pow(2, 10 * (time -= 1f)) * (float)Mathf.Sin((time * 1f - s) * Mathf.PI*2 / period));
            return amplitude * (float)Mathf.Pow(2f, -10f * (time -= 1f)) * (float)Mathf.Sin((time * 1f - s) * Mathf.PI*2f / period) * 0.5f + 1f;
        }
        public static float InBounce(float time) {
            time = Mathf.Clamp01(time);
            return 1f - OutBounce(1f - time);
        }
        public static float OutBounce(float time) {
            time = Mathf.Clamp01(time);
            if ((time /= 1f) < (1f / 2.75f)) {
                return (7.5625f * time * time);
            }
            if (time < (2f / 2.75f)) {
                return (7.5625f * (time -= (1.5f / 2.75f)) * time + 0.75f);
            }
            if (time < (2.5f / 2.75f)) {
                return (7.5625f * (time -= (2.25f / 2.75f)) * time + 0.9375f);
            }
            return (7.5625f * (time -= (2.625f / 2.75f)) * time + 0.984375f);
        }
        public static float InOutBounce(float time) {
            time = Mathf.Clamp01(time);
            if (time < 1f * 0.5f) {
                return InBounce(time * 2f) * 0.5f;
            }
            return OutBounce(time * 2f - 1f) * 0.5f + 0.5f;
        }
        public static float JumpLinear(float time) { 
            time = Mathf.Clamp01(time);
            return -Mathf.Pow((time * 2f) - 1f, 2f) + 1f;
        }
        public static float JumpSine(float time) { 
            time = Mathf.Clamp01(time);
            return (Mathf.Cos((time * Mathf.PI * 2f) + Mathf.PI) + 1f) / 2f;
        }

    }
}
