using UnityEngine;

public class FileUtils
{
    public static GameObject LoadPrefab(string filename)
    {
        var loadedPrefab = UnityEngine.Resources.Load("Prefabs/" + filename, typeof(GameObject)) as GameObject;
        return !loadedPrefab ? null : loadedPrefab;
    }
}
