using UnityEngine;

public class ClearPrefabs : MonoBehaviour
{
    public string spawnedPrefabTag = "Spawned";

    public void ToggleClearPrefabs(bool shouldClear)
    {
        if (shouldClear)
        {
            Debug.Log("Toggle On: Clearing all spawned prefabs!");
            GameObject[] spawnedPrefabs = GameObject.FindGameObjectsWithTag(spawnedPrefabTag);
            foreach (GameObject prefab in spawnedPrefabs)
            {
                Destroy(prefab);
            }
        }
        else
        {
            Debug.Log("Toggle Off: Prefabs remain intact—like my trust in bad sequels.");
        }
    }
}
