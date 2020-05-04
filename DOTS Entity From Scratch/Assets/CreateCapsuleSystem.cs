using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Rendering;
using Unity.Transforms;
using UnityEngine;

public class CreateCapsuleSystem : JobComponentSystem
{
    protected override void OnCreate()
    {
        base.OnCreate();

        // Create an instance with these components
        var instance = EntityManager.CreateEntity(
            ComponentType.ReadOnly<LocalToWorld>(),   // LocalTo World Component
            ComponentType.ReadOnly<RenderMesh>()
            );

        // Give components values
        EntityManager.SetComponentData(instance,
            new LocalToWorld
            {
                Value = new float4x4(rotation: quaternion.identity, translation: new float3(0, 0, 0))
            });

        // gets the ResourceLoader script from the ResourceLoader GameObject in our Resources folder
        var rHolder = Resources.Load<GameObject>("ResourceHolder").GetComponent<ResourceHolder>();
        EntityManager.SetSharedComponentData(instance,
            // Adding new MeshRenderer with mesh and material values
            new RenderMesh
            {
                mesh = rHolder.entityMesh,
                material = rHolder.entityMaterial
            });
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        return inputDeps;
    }
}
