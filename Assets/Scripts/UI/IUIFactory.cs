using Assets.Scripts.Player;
using DefaultNamespace;
using System.Threading.Tasks;

namespace Assets.Scripts.UI
{
    public interface IUIFactory
    {
        Task<HUDController> CreateHUD(PlayerModel playerModel);
    }
}