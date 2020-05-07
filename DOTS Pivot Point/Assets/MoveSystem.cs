using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public class MoveSystem : JobComponentSystem
{
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        var target = new float3(0, 0, 0);
        var deltaTime = Time.DeltaTime;

        var moveSpeed = 11F;

        var jobHandle = Entities
            .WithName("MoveSystem")
            .ForEach((ref Translation position, ref Rotation rotation, ref PlanetData planetData) =>
            {
                var heading = target - position.Value;
                float3 pivot = target;

                // moves the planets by the inverse of how close the planet is to the pivot
                float rotationSpeed = deltaTime * moveSpeed * 1 / math.distance(position.Value, pivot);

                // moves an object around a pivot using math.mul
                position.Value = math.mul(quaternion.AxisAngle(new float3(0, 1, 0), rotationSpeed), position.Value - pivot) + pivot;
            })
        .Schedule(inputDeps);

        return jobHandle;
    }
}
