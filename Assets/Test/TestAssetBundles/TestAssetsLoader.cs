using UnityEngine;
using System.Collections;

public class TestAssetsLoader : MonoBehaviour {

    public string[] assetBundleNames;
    public string[] assetNames;
	// Use this for initialization
	void Start () {

        StartCoroutine(LoadAssets(assetBundleNames, assetNames));

	}

    IEnumerator LoadAsset(string p_assetBundleName, string p_assetName)
    {
        if (p_assetBundleName == null || p_assetName == null)
        {
            goto error;
        }
        yield return StartCoroutine(AssetsLoader.assetsLoader.LoadAsset(p_assetBundleName, p_assetName));
    error:
        yield return -1;
    }

    IEnumerator LoadAssets(string[] p_assetBundleNames, string[] p_assetNames)
    {
        if (p_assetBundleNames.Length != p_assetNames.Length)
        {
            goto error;
        }
        if (p_assetBundleNames.Length == 0)
        {
            goto error;
        }
        for (int i = 0; i < p_assetBundleNames.Length; i++)
        {
            yield return StartCoroutine(AssetsLoader.assetsLoader.LoadAsset(p_assetBundleNames[i], p_assetNames[i]));
        }
    error:
        yield return -1;
    }
}
