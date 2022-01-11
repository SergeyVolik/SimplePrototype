using SerV112.UtilityAI.Game;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
namespace SerV112.UtilityAI.Game.Installer
{
    public class GameplaySceneInstaller : MonoInstaller
    {
        [SerializeField]
        Transform m_PlayerSpwanPoint;
        [SerializeField]
        Player m_PlayerPrefab;


        public override void InstallBindings()
        {
           SpawnPlayer();
        }

        private void SpawnPlayer()
        {
            Debug.Log("SpawnPlayerInit");
            var player = Container.InstantiatePrefabForComponent<Player>(m_PlayerPrefab, m_PlayerSpwanPoint.position, Quaternion.identity, null);

            Container.Bind<Player>().FromInstance(player).AsSingle();

        }
    }
}