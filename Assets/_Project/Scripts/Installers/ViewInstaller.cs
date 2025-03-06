using UI;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class ViewInstaller : MonoInstaller
    {
        [SerializeField] private GameResultView _gameResultView;
        [SerializeField] private TilemapView _tilemapView;

        public override void InstallBindings()
        {
            Container.Bind<GameResultView>().FromInstance(_gameResultView).AsSingle();
            Container.Bind<TilemapView>().FromInstance(_tilemapView).AsSingle();
        }
    }
}