using UnityEditor;
using System.IO;
using UnityEngine;

public class AssetBundleBuildManager : MonoBehaviour
{

    [MenuItem("MyTool/AssetBundle Build")]
    public static void AssetBundleBuild()
    {
#if UNITY_ANDROID
        BuildTarget buildTarget_ = BuildTarget.Android;
#elif UNITY_EDITOR_WIN
        BuildTarget buildTarget_ = BuildTarget.StandaloneWindows;
#endif
        string directory_ = "./Bundle";

        if (!Directory.Exists(directory_))
        {
            Directory.CreateDirectory(directory_);
        }
        BuildPipeline.BuildAssetBundles(directory_, BuildAssetBundleOptions.None, buildTarget_);

        Debug.Log("AssetBundle Build done");

    }

    [MenuItem("MyTool/Clear Cache")]
    public static void ClearingCache()
    {
        
        Caching.ClearCache();
        Debug.Log("cache clearing done");
    }
}
