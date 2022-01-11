using Zenject;
using UnityEngine;
using System.Collections;

public class TestInstaller : MonoInstaller
{
    [SerializeField]
    GameUpdate GameUpdate;

    [SerializeField]
    GameObject pregab;

    [SerializeField]
    int NumberOfPrefabs = 10000;
    public override void InstallBindings()
    {

        Container.Bind<GameUpdate>().FromNewComponentOnNewGameObject().AsSingle().NonLazy();
        //Container.Bind<ITickable>().To<UpdateableMonoBehaviour>().FromComponentInNewPrefab(pregab).AsTransient();

        //Container.BindInterfacesAndSelfTo<UpdateableMonoBehaviour>().FromInstance(pregab).AsSingle();

        //for (int i = 0; i < NumberOfPrefabs; i++)
        //{
        //    Container.InstantiatePrefab(pregab);    
        //}
       

    }
}
