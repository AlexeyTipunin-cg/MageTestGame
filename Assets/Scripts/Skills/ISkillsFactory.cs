using Assets.Scripts.Player;
using Assets.Scripts.Scene;
using Skills;
using System.Threading.Tasks;

namespace Assets.Scripts.UI
{
    public interface ISkillsFactory
    {
        ISkillController SkillController { get; }

        Task<ISkillController> CreateSkillController(LevelConfig levelConfig, PlayerModel playerModel);
    }
}