using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Jobs;
using System;

public class MoveSystem : JobComponentSystem
{
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        var jobHandle = Entities
               .WithName("MoveSystem")
               // We are moving entity that has a Translation component on it, including the children objects on our tank
               // By adding the TankData filter we will only move the parent object
               .ForEach((ref Translation position, re Rotation rotation, ref TankData tank) =>
               {
                   // Moves entity in the world Z direction

                   position.Value.z += 0.05f * math.forward(Rotation.Value);
               })
               .Schedule(inputDeps);

        return jobHandle;
    }
}
