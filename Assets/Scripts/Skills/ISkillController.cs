using Assets.Scripts.Player;
using Assets.Scripts.Scene;
using UniRx;

namespace Skills
{
    public interface ISkillController
    {
        ReactiveProperty<SkillModel> CurrentSkill { get; }
        SkillModel[] GetSkillModels { get; }

        void Lanch(LevelConfig levelConfig, PlayerModel playerModel);
    }
}