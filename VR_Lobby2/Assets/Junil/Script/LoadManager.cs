using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using System.Threading;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;


public class LoadManager : MonoBehaviour
{
    private string localFilePath;               // 로컬 버전파일 경로
    private string serverFilePath;              // 서버 버전파일 경로

    private AssetBundle vruiAsset;

    void Start()
    {
        serverFilePath =
        "https://s3.ap-northeast-2.amazonaws.com/cdn.habul.co.kr/habul/AssetBundles_US/VRAsset/";
        localFilePath = "./Bundle";

        StartCoroutine(BundleStart("vrui"));
    }


    // 서버에서 에셋 가져오는 코루틴
    IEnumerator BundleStart(string bundleName)
    {

        using (UnityWebRequest www = UnityWebRequest.Get(serverFilePath + bundleName))
        {
            yield return www.SendWebRequest();


            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log("Error");
            }
            else
            {
                //string directory_ = "./Bundle";

                if (!Directory.Exists(localFilePath))
                {
                    Directory.CreateDirectory(localFilePath);
                }
                Debug.Log("가져는 옴");


                File.WriteAllBytes(localFilePath + "/" + bundleName, www.downloadHandler.data);

                AssetBundle temp_ = AssetBundle.LoadFromFile(localFilePath + "/" + bundleName);

                switch (bundleName)
                {
                    case "vrui":
                        vruiAsset = temp_;
                        break;

                    default: 
                        break;
                }
            }
        }
    }   // BundleStart()

    public void GetAssetObj(string bundleName, string useObjName)
    {
        StartCoroutine(GetAssetStart(bundleName, useObjName));
    }

    // 에셋번들에서 오브젝트를 가져오는 코루틴
    IEnumerator GetAssetStart(string bundleName, string useObjName)
    {
        AssetBundle temp_ = null;
        switch (bundleName)
        {
            case "vrui":
                temp_ = vruiAsset;
                break;

            default:
                break;
        }


        if (temp_ == null)
        {
            yield break;
        }
        //"VRUI_Canvas"
        var tempAsset_ = temp_.LoadAsset<GameObject>(useObjName);
        var Obj_ = Instantiate(temp_);
        //var tempObj_ = temp_.LoadAsset<GameObject>("CH01_DS01_000");
        //var Obj_ = Instantiate(tempObj_);
    }
}
