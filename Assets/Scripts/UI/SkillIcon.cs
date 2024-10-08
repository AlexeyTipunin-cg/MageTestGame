using Skills;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.UI
{
    public class SkillIcon : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private Image _fill;
        [SerializeField] private Image _chooseBack;
        [SerializeField] private TMP_Text _cooldownText;

        private SkillModel _model;
        
        public void Init(SkillModel model, IReactiveProperty<SkillModel> currentSkillReactiveProperty)
        {
            _model = model;
            _model.skillState.Subscribe(OnStateChange).AddTo(this);
            _model.CurrentCoolDownTime.Subscribe(ShowTime).AddTo(this);
            currentSkillReactiveProperty.Subscribe(skillModel =>
            {
                _chooseBack.gameObject.SetActive(skillModel == _model);

            });
            SetActiveIcon();
        }

        private void OnStateChange(SkillState state)
        {
            if (_model.IsActive)
            {
                SetActiveIcon();
            }
            else
            {
                SetInactiveIcon();
            }
        }

        private void SetActiveIcon()
        {
            _fill.fillAmount = 0;
            _cooldownText.gameObject.SetActive(false);
            StopAllCoroutines();
        }
        
        private void SetInactiveIcon()
        {
            _fill.fillAmount = 1;
            _cooldownText.text = _model.CurrentCoolDownTime.ToString();
            _cooldownText.gameObject.SetActive(true);
        }

        private void ShowTime(float time)
        {
            _cooldownText.text = $"{_model.CurrentCoolDownTime.Value:F1}";
        }
    }
}