using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

// Spawns sheep and turns them into entities
public class ECSManager : MonoBehaviour
{
    EntityManager entityManager; // The container that holds everything
    public GameObject sheepPrefab;
    const int numSheep = 10000;

    void Start()
    {
        entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

        // Grabs settings from default EntityManager
        var settings = GameObjectConversionSettings.FromWorld(World.DefaultGameObjectInjectionWorld, null);

        // Creating an entity from a GameObject
        var prefabEntity = GameObjectConversionUtility.ConvertGameObjectHierarchy(sheepPrefab, settings);

        // Create sheep instances
        for (int i = 0; i < numSheep; i++)
        {
            var instance = entityManager.Instantiate(prefabEntity);

            // Creates a random point in 3D space. We are using a float3 becuase it is a structure compared to Vecto3 which is a class
            var position = transform.TransformPoint(new float3(UnityEngine.Random.Range(0, 100), UnityEngine.Random.Range(-100, 100), UnityEngine.Random.Range(-100, 100)));

            // Adding a new Translation component to entity with a value of the position variable we initialized above
            entityManager.SetComponentData(instance, new Translation { Value = position });

            // Adding a new Rotation component to entity with a value of a default Quaternion
            entityManager.SetComponentData(instance, new Rotation { Value = new quaternion(0, 0, 0, 0) });
        }
    }
}
