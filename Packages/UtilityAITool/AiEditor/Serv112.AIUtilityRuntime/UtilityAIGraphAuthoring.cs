using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using SerV112.UtilityAIEditor;
using Unity.Collections;

namespace SerV112.UtilityAIRuntime
{
    public struct UtilityAIGraphAsset
    {
        public int Value;
    }

    public struct UtilityAIGraph : IComponentData
    {
        public BlobAssetReference<UtilityAIGraphAsset> Ref;
    }



    [DisallowMultipleComponent]
    public class UtilityAIGraphAuthoring : MonoBehaviour
    {

        [SerializeField]
        AIGraphAssetModel m_GraphAsset;

        public AIGraphAssetModel GraphAsset => m_GraphAsset;

    }

    

   



}
