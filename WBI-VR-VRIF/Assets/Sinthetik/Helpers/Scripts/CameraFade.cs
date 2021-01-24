using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sinthetik.Helpers
{
    public class CameraFade : MonoBehaviour
    {
        public AnimationCurve FadeCurve = new AnimationCurve(new Keyframe(0, 1), new Keyframe(0.6f, 0.7f, -1.8f, -1.2f), new Keyframe(1, 0));

        private float _alpha = 1;
        private Texture2D _texture;
        private bool _done;
        private float _time;
        private bool _reverse;

        public void Reset()
        {
            _done = false;
            _alpha = 1;
            _time = 0;
            _reverse = false;
        }

        public void Reverse()
        {
            _done = false;
            _alpha = 0;
            _time = 1;
            _reverse = true;
        }

        [RuntimeInitializeOnLoadMethod]
        public void RedoFade()
        {
            Reset();
        }

        public void OnGUI()
        {
            if (_texture == null) _texture = new Texture2D(1, 1);

            _texture.SetPixel(0, 0, new Color(0, 0, 0, _alpha));
            _texture.Apply();

            if (!_reverse)
            {
                if (!_done)
                {
                    _time += Time.deltaTime;
                    _alpha = FadeCurve.Evaluate(_time);
                    GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), _texture);
                }

                if (_alpha <= 0) _done = true;
            }
            else
            {
                if (!_done)
                {
                    _time -= Time.deltaTime;
                    _alpha = FadeCurve.Evaluate(_time);
                    GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), _texture);
                }
                else
                {
                    GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), _texture);
                }

                if (_alpha >= 1) _done = true;
            }
        }

        
    }
}