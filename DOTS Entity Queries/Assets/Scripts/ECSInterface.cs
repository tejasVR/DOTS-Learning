using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using TMPro;

public class ECSInterface : MonoBehaviour
{
    World world;

    [Space(7)]
    [SerializeField] GameObject palmTreePrefab;
    [SerializeField] GameObject tankPrefab;

    [Space(7)]
    [SerializeField] TextMeshProUGUI sheepCount;
    [SerializeField] TextMeshProUGUI tankCount;

    void Start()
    {
        // Here we get the default world
        world = World.DefaultGameObjectInjectionWorld;

        // We are logging the number of entities in the console
        Debug.Log("All Entities: " + world.GetExistingSystem<MoveSystem>().EntityManager.GetAllEntities().Length);
    }

    private void Update()
    {
        // Create a sheep or tank in a random location
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AddingEntityFromPrefab();
        }
    }

    public void CountSheep()
    {
        // Create a new entity manager
        EntityManager entityManager = world.GetExistingSystem<MoveSystem>().EntityManager;

        // Create an entity query based on the found component data
        EntityQuery entityQuery = entityManager.CreateEntityQuery(ComponentType.ReadOnly<SheepData>());

        UpdateSheepCount(entityQuery.CalculateEntityCount());
    }
 
    public void CountTanks()
    {
        EntityManager entityManager = world.GetExistingSystem<MoveSystem>().EntityManager;
        EntityQuery entityQuery = entityManager.CreateEntityQuery(ComponentType.ReadOnly<TankData>());
        UpdateTankCount(entityQuery.CalculateEntityCount());
    }

    private void AddEntityAtRuntime()
    {
        // Becuase our prefab is converted to an entity automatically, we just instantiate it
        Vector3 randomPosition = new Vector3(UnityEngine.Random.Range(-10, 10), 0, UnityEngine.Random.Range(-10, 10));
        Instantiate(tankPrefab, randomPosition, Quaternion.identity);
    }

    private void AddingEntityFromPrefab()
    {
        // Create EntityManager and prefabEntity that convert our GameObject into an entity
        EntityManager entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        var settings = GameObjectConversionSettings.FromWorld(World.DefaultGameObjectInjectionWorld, null);
        var prefabEntity = GameObjectConversionUtility.ConvertGameObjectHierarchy(palmTreePrefab, settings);

        // Create an instance of the entity and add Translation and Rotation components to it
        var instance = entityManager.Instantiate(prefabEntity);
        var position = transform.TransformPoint(new float3(UnityEngine.Random.Range(-15, 15), 0, UnityEngine.Random.Range(-15, 15)));
        entityManager.SetComponentData(instance, new Translation { Value = position });
        entityManager.SetComponentData(instance, new Rotation { Value = new quaternion(0, 0, 0, 0) });
    }

    private void UpdateSheepCount(int _count)
    {
        sheepCount.text = _count.ToString();
    }

    private void UpdateTankCount(int _count)
    {
        tankCount.text = _count.ToString();
    }
}
