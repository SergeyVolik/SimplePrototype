using UnityEngine;
using Zenject;

namespace SerV112.UtilityAI.Game.Installer
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField]
        InputReader m_input;
        public override void InstallBindings()
        {
            Debug.Log("project context");
            Container.Bind<GameUpdate>().FromNewComponentOnNewGameObject().AsSingle().NonLazy();
            Container.Bind<InputReader>().FromInstance(m_input).AsSingle();
        }
    }

}
