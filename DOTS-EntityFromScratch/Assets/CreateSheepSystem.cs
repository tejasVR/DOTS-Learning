using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Rendering;
using Unity.Transforms;
using UnityEngine;

public class CreateSheepSystem : JobComponentSystem
{

    const int numSheep = 100;

    protected override void OnCreate()
    {
        base.OnCreate();

        for (int i = 0; i < numSheep; i++)
        {
            CreateSheep();            
        }       
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        return inputDeps;
    }

    private void CreateSheep()
    {
        // Create an instance of an entity with components
        var instance = EntityManager.CreateEntity(
            ComponentType.ReadOnly<LocalToWorld>(),    // We still need this for our entity to show up in our world!
            ComponentType.ReadWrite<Translation>(),
            ComponentType.ReadWrite<Rotation>(),
            ComponentType.ReadOnly<RenderMesh>(),
            ComponentType.ReadOnly<NonUniformScale>()
            );

        // We can't set values to LocalToWorld AND NonUniformScale
        //EntityManager.SetComponentData(instance,
        // new LocalToWorld
        // {
        //     Value = new float4x4(rotation: quaternion.identity, translation: RandomPosition())
        // });

        // We need to establigh Translation and Rotation values to position our sheep
        EntityManager.SetComponentData(instance, new Translation { Value = RandomPosition() });
        EntityManager.SetComponentData(instance, new Rotation { Value = new quaternion(0, 0, 0, 0) });
        EntityManager.SetComponentData(instance, new NonUniformScale { Value = RandomScale() }); //new  float3(10, 10, 10)

        // get ResourceLoader mesh from Resources folder
            var rHolder = Resources.Load<GameObject>("SheepHolder").GetComponent<SheepHolder>();
        EntityManager.SetSharedComponentData(instance,
            new RenderMesh
            {
                mesh = rHolder.sheepMesh,
                material = rHolder.sheepMaterial
            });
    }

    private float3 RandomPosition()
    {
        return new float3(UnityEngine.Random.Range(-10, 10), 0, UnityEngine.Random.Range(-10, 10));
    }

    private float3 RandomScale()
    {
        return new float3(UnityEngine.Random.Range(10, 20), UnityEngine.Random.Range(10, 20), UnityEngine.Random.Range(10, 20));
    }
}
