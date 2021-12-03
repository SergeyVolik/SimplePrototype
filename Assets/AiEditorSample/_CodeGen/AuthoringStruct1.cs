//-----------------------------------------------------------------------
// This file is AUTO-GENERATED.
// Changes for this script by hand might be lost when auto-generation is run.
// (Generated date: 2021.12.03 19:46:38)
//-----------------------------------------------------------------------
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;


namespace MyNamespace
{
    [DisallowMultipleComponent]
    public class AuthoringStruct1 : MonoBehaviour, IConvertGameObjectToEntity
    {

         [SerializeField]
private Enum1  m_Field1;


        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {

            dstManager.AddComponentData(entity, new Struct1 {
                Field1 = m_Field1           
            });
        
        }
    }

}
