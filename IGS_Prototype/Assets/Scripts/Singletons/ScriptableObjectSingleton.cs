using UnityEditor;
using UnityEngine;

public abstract class ScriptableObjectSingleton<T> : ScriptableObject where T : ScriptableObject
{
    private static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                T[] results = Resources.FindObjectsOfTypeAll<T>();
                if (results.Length == 0)
                {
                    #if UNITY_EDITOR
                    // It has a tendency to throw this error every second or two, feel free to ignore... its unity being jank (in case instance is not missing)
                    Debug.LogError("ScriptableObjectSingleton: No objects found in Resources folder, scriptable object instance missing");
                    #endif
                    return null;
                }

                if (results.Length > 1)
                {
                    #if UNITY_EDITOR
                    string locations = "";
                    foreach (T result in results)
                    {
                        string assetPath = AssetDatabase.GetAssetPath(result);
                        locations += $"{result.name}: {assetPath}\n";
                        if (assetPath == "" || result.name == "")
                            result.hideFlags = HideFlags.None;
                    }
                    Debug.LogError($"ScriptableObjectSingleton: Multiple objects found in Resources folder: {results.Length}. \n{locations}");
                    #endif
                    
                    return null;
                }

                
                instance = results[0];
                instance.hideFlags = HideFlags.DontUnloadUnusedAsset;
            }
            return instance;
        }
    }
}