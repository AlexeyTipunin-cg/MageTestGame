using UniRx;
using UnityEngine;

namespace Skills
{
    public class SkillView : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _skillVFX;
        [SerializeField] private SkillType _skillType;

        public SkillType GetSkillType => _skillType;


        public void Init(SkillModel model)
        {
            model.command.Subscribe(UseSkill).AddTo(this);
        }

        private void UseSkill(SkillData data)
        {
            _skillVFX.Play();
        }
    }
}