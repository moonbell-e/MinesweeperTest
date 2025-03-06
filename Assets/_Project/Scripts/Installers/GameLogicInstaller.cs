using Core;
using Core.Input;
using Data;
using Infrastructure.GameStateMachine;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class GameLogicInstaller : MonoInstaller
    {
        [SerializeField] private MinesweeperConfig _config;

        public override void InstallBindings()
        {
            Container.Bind<MinesweeperConfig>().FromScriptableObject(_config).AsSingle();

            BindMinesweeperElements();
            BindGameLogicServices();
        }

        private void BindMinesweeperElements()
        {
            Container.Bind<ICellFactory>().To<CellFactory>().AsSingle();
            Container.Bind<MineGenerator>().AsSingle();
            Container.Bind<Minefield>().AsSingle();
        }

        private void BindGameLogicServices()
        {
            Container.Bind<MinesweeperMediator>().AsSingle();
            Container.Bind<IInputService>().To<InputService>().AsSingle();
            Container.Bind<IStateMachine>().To<GameStateMachine>().AsSingle();
        }
    }
}