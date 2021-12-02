//-----------------------------------------------------------------------
// This file is AUTO-GENERATED.
// Changes for this script by hand might be lost when auto-generation is run.
// (Generated date: 2021.12.02 19:03:55)
//-----------------------------------------------------------------------
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;


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
