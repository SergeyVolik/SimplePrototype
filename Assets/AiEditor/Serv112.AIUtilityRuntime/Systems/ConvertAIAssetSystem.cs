using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

namespace SerV112.UtilityAIRuntime
{
    [UpdateInGroup(typeof(GameObjectConversionGroup))]
    public class ConvertAIAssetSystem : GameObjectConversionSystem
    {
        // We made this static so that other systems can access the blob asset.
        // We'll modify this later to work with job systems. 
        // For now, let's keep it simple.
        public static BlobAssetReference<UtilityAIGraphAsset> NpcBlobAssetReference;

        protected override void OnCreate()
        {
            base.OnCreate();

            // Let's debug here to make sure the system ran
            Debug.Log("Prefab entities system created!");
        }

        protected override void OnUpdate()
        {
            // Access the DataContainer attached to a gameObject here and copy the data to a blob asset
            this.Entities.ForEach((UtilityAIGraphAuthoring container) => {

                // We use a using block since the BlobBuilder needs to be disposed after using it
                using (BlobBuilder blobBuilder = new BlobBuilder(Allocator.Temp))
                {
                    var entity = GetPrimaryEntity(container);

                    // Take note of the "ref" keywords. Unity will throw an error without them, since we're working with structs.
                    ref UtilityAIGraphAsset npcDataBlobAsset = ref blobBuilder.ConstructRoot<UtilityAIGraphAsset>();

                    npcDataBlobAsset.Value = 10;
                    // Copy data. We'll work with lists/arrays later.
                    //npcDataBlobAsset.TotalNumberOfNpcs = container.GraphAsset.TotalNumberOfNpcs;
                    //npcDataBlobAsset.TotalFriends = container.NpcManagerData.TotalFriends;

                    // Store the created reference to the memory location of the blob asset
                    NpcBlobAssetReference = blobBuilder.CreateBlobAssetReference<UtilityAIGraphAsset>(Allocator.Persistent);

                    DstEntityManager.AddComponentData(entity, new UtilityAIGraph
                    {
                        Ref = NpcBlobAssetReference
                    });
                }


            });


        }
    }
}
