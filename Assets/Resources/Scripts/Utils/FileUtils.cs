using UnityEngine;

public class FileUtils
{
    public static GameObject LoadPrefab(string filename)
    {
        var loadedPrefab = Resources.Load("Prefabs/" + filename, typeof(GameObject)) as GameObject;
        if (loadedPrefab == null)
        {
            Debug.Log("Prefab " + filename + " not found");
        }
        return loadedPrefab;
    }
}
