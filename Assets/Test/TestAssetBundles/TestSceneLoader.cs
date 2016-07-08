using UnityEngine;
using System.Collections;

public class TestSceneLoader : MonoBehaviour {

    public string sceneAssetBundleName;
    public string sceneName;

    public bool loadLevelAdditive = false;
    // Use this for initialization
    void Start()
    {
        StartCoroutine(LoadScene(sceneAssetBundleName, sceneName, loadLevelAdditive));
    }

    IEnumerator LoadScene(string p_sceneAssetBundleName, string p_sceneName, bool p_loadLevelAdditive)
    {
        if (p_sceneAssetBundleName == null || p_sceneName == null) 
        {
            goto error;
        }

        yield return StartCoroutine(AssetsLoader.assetsLoader.LoadScene(p_sceneAssetBundleName, p_sceneName, p_loadLevelAdditive));

    error:
        yield return -1;
    }
}
