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
        float deltaTime = Time.DeltaTime;
        float3 targetLocation = new float3(0, 0, 0);
        float positionSpeed = 1.5F;
        float rotationSpeed = 0.5F;

        var jobHandle = Entities
               .WithName("MoveSystem")
               // We are moving entity that has a Translation component on it, including the children objects on our tank
               // By adding the TankData filter we will only move the parent object
               .ForEach((ref Translation position, ref Rotation rotation, ref TankData tank) =>
               {
                   // Create a heading direction for the entity and zero out the y value for th entity to remain upright
                   float3 heading = targetLocation - position.Value;
                   heading.y = 0;

                   // Hold the quaternion for looking at a position
                   quaternion targetDirection = quaternion.LookRotation(heading, math.up());

                   // Slerp the rotation between current rotation and targetDirection by a speed of Time.DeltaTime
                   rotation.Value = math.slerp(rotation.Value, targetDirection, deltaTime * rotationSpeed);

                   // Pushing the tank forwards
                   position.Value += deltaTime * positionSpeed * math.forward(rotation.Value);
               })
               .Schedule(inputDeps);

        return jobHandle;
    }
}
