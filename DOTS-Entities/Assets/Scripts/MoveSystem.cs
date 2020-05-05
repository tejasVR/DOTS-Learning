using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Jobs;
public class MoveSystem : JobComponentSystem
{
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        var jobHandle = Entities
            .WithName("MoveSystem")

            // Gets all entities with SheepData
            .ForEach((ref Translation position, ref Rotation rotation, ref SheepData sheepData) =>
            {
                // Moving objects up
                position.Value += 0.01F * math.up();
            })
            .Schedule(inputDeps);

        return jobHandle;
    }
}
