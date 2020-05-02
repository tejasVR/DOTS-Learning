using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Jobs;
using Unity.Transforms;
using UnityEngine;

public class MoveSystem : JobComponentSystem
{
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        var jobHandle = Entities
            .WithName("MoveSystem")
            .ForEach((ref Translation position, ref Rotation rotation) =>
            {

            })
        .Schedule(inputDeps);

        return jobHandle;
    }    
}
