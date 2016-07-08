using UnityEngine;
using System.Collections;

public class AssetsLoader : BaseLoader {

    static public AssetsLoader assetsLoader;
    static bool isInitialized = false;

    void Awake()
    {
        assetsLoader = this;
    }

	// Use this for initialization
	IEnumerator Start () {

		yield return StartCoroutine(Initialize() );
        isInitialized = true;

	}

    public IEnumerator LoadAsset(string p_assetBundleName, string p_assetName)
    {
        while (!isInitialized)
            yield return 0;

        // Load asset.
        yield return StartCoroutine(Load(p_assetBundleName, p_assetName));

        // Unload assetBundles.
        AssetBundleManager.UnloadAssetBundle(p_assetBundleName);

    }

    public IEnumerator LoadScene(string p_sceneAssetBundleName, string p_sceneName, bool p_loadLevelAdditive = false)
    {
        while (!isInitialized)
            yield return 0;

        // Load level.
        yield return StartCoroutine(LoadLevel(p_sceneAssetBundleName, p_sceneName, p_loadLevelAdditive));

        // Unload assetBundles.
        AssetBundleManager.UnloadAssetBundle(p_sceneAssetBundleName);

    }
}
