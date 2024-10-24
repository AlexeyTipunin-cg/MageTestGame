using Assets.Scripts.Scene;
using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public interface IHeroFactory
    {
        public event Action<GameObject> onPlayerCreated;
        IGetHeroPosition HeroPosition { get; }
        public Task<GameObject> CreateHero(Vector3 postion, LevelConfig levelConfig, ISceneLimits limits, PlayerModel playerModel);

        public GameObject GetHero();
    }
}