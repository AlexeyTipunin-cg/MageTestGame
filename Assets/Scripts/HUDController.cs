using System;
using UnityEngine;
using TMPro;
using UniRx;

namespace DefaultNamespace
{
    public class HUDController : MonoBehaviour
    {
        [SerializeField] private Transform _playerHealthProgressBar;
        [SerializeField] private TMP_Text _endGameText;

        private float _maxHealth;

        private void Start()
        {
            var character = FindObjectOfType<CharacterController>();
            _maxHealth = character.PlayerModel.MaxHealth;

            SetProgressBar(character.PlayerModel.health.Value);

            character.PlayerModel.health.Subscribe(SetProgressBar).AddTo(this);

            character.PlayerModel.isDead.Subscribe(isDead =>
            {
                if (isDead)
                {
                    _endGameText.gameObject.SetActive(true);
                }
            });
        }

        private void SetProgressBar(float health)
        {
            var scale = _playerHealthProgressBar.localScale;
            scale.x = Mathf.Max(health / _maxHealth, 0);
            _playerHealthProgressBar.localScale = scale;
        }
    }
}