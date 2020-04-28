using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public class LargeCubeMove : JobComponentSystem
{
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        var jobHandle = Entities
            .WithName("MoveSystem")
            .ForEach((ref Translation position, ref Rotation rotation, ref LargeCubeData largeCube) =>
            {
                position.Value -= 0.01F * math.up();
            })
            .Schedule(inputDeps);

        return jobHandle;
    }
}
