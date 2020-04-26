using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Jobs;
using Unity.Jobs;

public class SpawnParallel : MonoBehaviour
{
    public GameObject sheepPrefab;
    const int numSheep = 10000;

    // Need a structure to execute a job
    struct MoveJob: IJobParallelForTransform
    {
        // Here we don't have fancy methods like transform.Translate(). We have to do the math ourselves
        public void Execute(int index, TransformAccess transformAccess)
        {
            // We are changing the position of an object by the Z-forward axis of the corresponding object
            transformAccess.position += 0.08F * (transformAccess.rotation * new Vector3(0, 0, 1));

            // Re-position the object when is moves passed a certain Z-value
            if (transformAccess.position.z > 50F)
            {
                transformAccess.position = new Vector3(transformAccess.position.x, 0F, -50F);
            }
        }
    }
    
    MoveJob moveJob; // Initializing MoveJob    
    JobHandle moveHandle; // Handling the job via JobHandle    
    TransformAccessArray transforms; // Need to declare a TransformAccessArray 

    Transform[] allSheepTransform;

    void Start()
    {
        allSheepTransform = new Transform[numSheep];

        for (int i = 0; i < numSheep; i++)
        {
            // Position a sheep randomly within parameters
            Vector3 sheepPos = new Vector3(Random.Range(-50, 50), 0, Random.Range(-50, 50));
            GameObject sheep = Instantiate(sheepPrefab, sheepPos, Quaternion.identity);

            // Get the Transform of a sheep and add it to a Transform array
            allSheepTransform[i] = sheep.transform;
        }

        // Assign all transforms of sheep to our TransformAccessArray
        transforms = new TransformAccessArray(allSheepTransform);
    }

    private void Update()
    {
        // Just creating the job will NOT execute the job on Update
        moveJob = new MoveJob { };

        // Call all transforms in the TransformAccessArray to schedule MoveJob
        moveHandle = moveJob.Schedule(transforms);
    }

    private void LateUpdate()
    {
        // This ensures that all MoveJob have been completed before executing job again
        moveHandle.Complete();
    }

    private void OnDestroy()
    {
        // We are working with native code, so we need to manually dispose job in memory
        transforms.Dispose();
    }
}
