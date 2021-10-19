using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities; 

namespace AntsGame
{
    public class PrefabsHolder : MonoBehaviour, IConvertGameObjectToEntity, IDeclareReferencedPrefabs
    {

        public static Entity AntPrefab { get; private set; }

        [SerializeField]
        private GameObject m_AntGOPrefab;


        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {

            AntPrefab = conversionSystem.GetPrimaryEntity(m_AntGOPrefab);
        }

        public void DeclareReferencedPrefabs(List<GameObject> referencedPrefabs)
        {
            referencedPrefabs.Add(m_AntGOPrefab);
        }
    }



}

