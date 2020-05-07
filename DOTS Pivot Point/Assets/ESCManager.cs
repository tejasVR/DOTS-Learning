using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public class ESCManager : MonoBehaviour
{
    EntityManager entityManger;
    public GameObject planetPrefab;
    const int numPlanets = 10000;

    private void Start()
    {
        // Define EntityManager, world settings, and convert prefab into entity
        entityManger = World.DefaultGameObjectInjectionWorld.EntityManager;
        var settings = GameObjectConversionSettings.FromWorld(World.DefaultGameObjectInjectionWorld, null);
        var prefab = GameObjectConversionUtility.ConvertGameObjectHierarchy(planetPrefab, settings);

        for (int i = 0; i < numPlanets; i++)
        {
            var instance = entityManger.Instantiate(prefab);

            float x = UnityEngine.Random.Range(-70, 70);
            float z = UnityEngine.Random.Range(-70, 70);

            entityManger.SetComponentData(instance, new Translation { Value = new float3(x, 0, z) });            
        }
    }
}
