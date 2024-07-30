using Assets.Scripts.Player;
using UniRx;
using UnityEngine;

namespace Skills
{
    public class SkillView : MonoBehaviour
    {
        [SerializeField] protected ParticleSystem _skillVFX;
        [SerializeField] private SkillType _skillType;

        public SkillType GetSkillType => _skillType;

        protected Skill _config;
        protected PlayerModel _playerModel;

        public virtual void Init(PlayerModel playerModel, SkillModel model)
        {
            _config = model._skill;
            _playerModel = playerModel;
            model.command.Subscribe(UseSkill).AddTo(this);
        }

        protected virtual void ApplyInfluenceImmediattly()
        {
            
        }

        private void UseSkill(SkillData data)
        {
            _skillVFX.Play();
            ApplyInfluenceImmediattly();
        }
        
        
    }
}