using UnityEngine;
using Unity.Transforms;
using Unity.Mathematics;
using Unity.Entities;

namespace AntsGame.ECS.Systems
{
    public class EntitySpawnerSystem : ComponentSystem
    {

        private float m_SpawnTimer;
        private Unity.Mathematics.Random m_Random;
        private int m_UnitsCreated;
        private const float m_DelayToCreate = .0001f;
        private const float m_SpawnStep = 50f;
        protected override void OnCreate()
        {
            m_Random = new Unity.Mathematics.Random(56);
        }

        protected override void OnUpdate()
        {

            if (Input.GetKey(KeyCode.Space) || Input.touchCount > 0)
            {
               



                for (var i = 0; i < 10000; i++)
                {
                    m_UnitsCreated++;

                    Entity spawnerEntity = EntityManager.Instantiate(PrefabsHolder.AntPrefab);

                    EntityManager.SetComponentData(spawnerEntity,
                        new Translation
                        {
                            Value = new float3(
                            m_Random.NextFloat(-m_SpawnStep, m_SpawnStep),
                            0,
                                m_Random.NextFloat(-m_SpawnStep, m_SpawnStep))
                        });

                      
                }

                UIManager.Intsance.SetUnitsCount(m_UnitsCreated);
            }
        }

    }

}
