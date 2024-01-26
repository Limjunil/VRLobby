using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;
using UnityEngine.SceneManagement;
using System.Security.Cryptography;
using System.Text;
using System;
using Newtonsoft.Json;

public class AssetBundleDataManager : GSingleton<AssetBundleDataManager>
{
    
    public AssetBundle vruiBundle;
    public AssetBundle vrdanceBundle;
    public AssetBundle vrbellydanceBundle;

    [SerializeField]
    public GdStageRoot gdStageList = new GdStageRoot();
    [SerializeField]
    public VersionRoot versonList = new VersionRoot();

    protected override void Init()
    {
        
    }

    public void SetBundleData()
    {
        string localUrl_ = Application.persistentDataPath + Define.LOCAL_FILE_PATH;

        switch (SceneManager.GetActiveScene().name)
        {
            case Define.VR_LOBBY_SCENE:
                if (vruiBundle != null)
                    break;
                vruiBundle = AssetBundle.LoadFromFile(localUrl_ + "/" + "vrui");
                break;

            case Define.VR_SCENE_TWO:
                if (vrdanceBundle != null)
                    break;
                vrdanceBundle = AssetBundle.LoadFromFile(localUrl_ + "/" + "vrdance");
                break;

            case Define.VR_SCENE_THREE:

                break;

            case Define.VR_SCENE_FOUR:
                if (vrbellydanceBundle != null)
                    break;
                vrbellydanceBundle = AssetBundle.LoadFromFile(localUrl_ + "/" + "vrbellydance");

                break;

            default:
                break;
        }
    }


    public void SetBundle(string bundleName)
    {
        StartCoroutine(BundleStart(bundleName));
    }

    IEnumerator BundleStart(string bundleName)
    {
        string localUrl_ = Application.persistentDataPath + Define.LOCAL_FILE_PATH;

        using (UnityWebRequest www = UnityWebRequest.Get(Define.SERVER_FILE_PATH + bundleName))
        {
            yield return www.SendWebRequest();


            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log("Error");
            }
            else
            {

                if (!Directory.Exists(localUrl_))
                {
                    Directory.CreateDirectory(localUrl_);
                }

                // 파일 저장
                File.WriteAllBytes(localUrl_  + "/" + bundleName, www.downloadHandler.data);

            }
        }

        using (UnityWebRequest www = UnityWebRequest.Get(Define.SERVER_FILE_PATH + bundleName + ".manifest"))
        {
            yield return www.SendWebRequest();

            File.WriteAllBytes(localUrl_ + "/" + bundleName + ".manifest", www.downloadHandler.data);

        }

    }   // BundleStart()


    // 에셋번들에서 오브젝트를 가져오는 코루틴
    public GameObject GetAssetUIObj(string bundleName, string useObjName)
    {
        GameObject tempAsset_ = null;

        switch (bundleName)
        {
            case "vrui":
                tempAsset_ = vruiBundle.LoadAsset<GameObject>(useObjName);
                break;

            case "vrdance":
                tempAsset_ = vrdanceBundle.LoadAsset<GameObject>(useObjName);
                break;

            case "vrbellydance":
                tempAsset_ = vrbellydanceBundle.LoadAsset<GameObject>(useObjName);
                break;

            default:
                break;
        }

        //if(vruiBundle == null)
        //        vruiBundle = AssetBundle.LoadFromFile(localFilePath + "/" + bundleName);

        

        if (tempAsset_ == null)
        {
            return null;
        }

        return tempAsset_;
    }   // GetAssetUIObj()


}

