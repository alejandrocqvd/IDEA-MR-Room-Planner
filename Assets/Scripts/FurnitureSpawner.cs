using UnityEngine;
using System.Collections.Generic;

public class FurnitureSpawner : MonoBehaviour
{
    public Transform playerHead;
    public GameObject cubePrefab;

    // Maintain a list of spawned prefabs
    public static List<GameObject> spawnedPrefabs = new List<GameObject>();

    public void ToggleSpawnFurniture(bool spawnFurniture)
    {
        if (!spawnFurniture)
            return;

        Debug.Log("Toggle On: Spawning furniture (cube)!");

        if (playerHead == null)
        {
            Debug.LogError("Player head transform is not assigned!");
            return;
        }

        // Use a prefab if assigned
        GameObject cubeToSpawn = cubePrefab != null ? cubePrefab : GameObject.CreatePrimitive(PrimitiveType.Cube);

        float spawnDistance = 0.75f;
        Vector3 spawnPosition = playerHead.position + playerHead.forward * spawnDistance;

        // Get the cube's height
        float cubeHeight = cubeToSpawn.GetComponent<Renderer>()?.bounds.size.y ?? 0.0f;

        if (cubeHeight >= 0.6f)
        {
            spawnPosition.y = 0.0f;
        }
        else
        {
            spawnPosition.y = 0.75f;
        }

        // Instantiate the cube and assign the "Spawned" tag
        GameObject spawnedCube = Instantiate(cubeToSpawn, spawnPosition, Quaternion.identity);
        spawnedCube.tag = "Spawned";

        // Add the spawned cube to the list
        spawnedPrefabs.Add(spawnedCube);

        Vector3 directionToPlayer = playerHead.position - spawnedCube.transform.position;
        directionToPlayer.y = 0;
        spawnedCube.transform.rotation = Quaternion.LookRotation(directionToPlayer);
    }

    public void ToggleRemoveMostRecentPrefab(bool removePrefab)
    {
        if (removePrefab)
        {
            if (spawnedPrefabs.Count > 0)
            {
                GameObject mostRecent = spawnedPrefabs[spawnedPrefabs.Count - 1];
                Destroy(mostRecent);
                spawnedPrefabs.RemoveAt(spawnedPrefabs.Count - 1);
                Debug.Log("Removed the most recently spawned prefab.");
            }
            else
            {
                Debug.Log("No spawned prefabs to remove.");
            }
        }
        else
        {
            Debug.Log("Toggle Off: Keeping the prefab mess intact.");
        }
    }
}
