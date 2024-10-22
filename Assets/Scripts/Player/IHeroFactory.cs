using Assets.Scripts.Player;
using Assets.Scripts.ResourceManagement;
using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public interface IHeroFactory
    {
        public event Action<GameObject> onPlayerCreated;
        IGetHeroPosition HeroPosition { get; }
        public Task<GameObject> CreateHero();
    }
}