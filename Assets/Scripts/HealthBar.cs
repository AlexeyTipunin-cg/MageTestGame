using System;
using UnityEngine;
using UniRx;

namespace DefaultNamespace
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Transform _health;
        private Camera _camera;
        private void Awake()
        {
            _camera = FindObjectOfType<Camera>();
        }

        public void SetProgress(float currentVal, float maxVal)
        {
            var scale = _health.transform.localScale;
            scale.x = Mathf.Max(currentVal / maxVal, 0);
            _health.transform.localScale = scale;
        }

        private void LateUpdate()
        {
            transform.LookAt(_camera.transform.position);
        }
    }
}