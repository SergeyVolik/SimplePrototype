using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameInit : MonoInstaller
{
    public override void InstallBindings()
    {
        Debug.Log("project context");
        Container.Bind<GameUpdate>().FromNewComponentOnNewGameObject().AsSingle().NonLazy();
       
    }
}
