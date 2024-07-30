using System;
using UnityEngine;
using UniRx;
using Zenject;

namespace DefaultNamespace
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Transform _health;
        private Camera _camera;
        public void Init(Camera cameraInject)
        {
            _camera = cameraInject;
        }

        public void SetProgress(float currentVal, float maxVal)
        {
            var scale = _health.transform.localScale;
            scale.x = Mathf.Max(currentVal / maxVal, 0);
            _health.transform.localScale = scale;
        }

        private void LateUpdate()
        {
            //transform.LookAt(_camera.transform.position);
            transform.LookAt(transform.position + _camera.transform.rotation * Vector3.back, _camera.transform.rotation * Vector3.down);

        }
    }
}