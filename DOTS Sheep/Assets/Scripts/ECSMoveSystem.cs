using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Jobs;
using Unity.Transforms;
using Unity.Jobs;
using Unity.Mathematics;

public class ECSMoveSystem : JobComponentSystem
{
    // An implenented method because we are inheriting from JobComponentSystem
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        // Puts all entities in a group called 'MoveSystem'
        var jobHandle = Entities.WithName("MoveSystem")
            // ForEach gets all entities with specific components
            .ForEach((ref Translation position, ref Rotation rotation) =>
            {
                position.Value.y += 0.05F;
                if (position.Value.y > 100)
                {
                    position.Value.y = 0;
                }                
            })

            .Schedule(inputDeps);

        return jobHandle;
    }
}
