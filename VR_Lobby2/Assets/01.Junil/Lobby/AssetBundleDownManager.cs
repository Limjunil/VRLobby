using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class AssetBundleDownManager : MonoBehaviour
{

    public string[] serverBundleArray;
    public List<AssetBundleInfo> oldAsset = new List<AssetBundleInfo>();
    public List<AssetBundleInfo> newAsset = new List<AssetBundleInfo>();

    public int checkVal = 0;
    public bool isEnd = false;

    private Queue<Coroutine> _queue = new Queue<Coroutine>();
    //public TMPro.TMP_Text checkTxt;

    public bool isNoUpdate = false;

    private void Start()
    {
        CheckBtn();
    }
    public void CheckBtn()
    {
        AssetBundleDataManager.Instance.Create();

        //checkTxt.text = "버튼 눌렀어요";
        // 패치 파일 있는지 확인
        StartCoroutine(CheckUpdateAsset());
    }

    // 에셋번들 패치 확인하는 코루틴
    IEnumerator CheckUpdateAsset()
    {
        string localUrl_ = Application.persistentDataPath + Define.LOCAL_FILE_PATH;
        Debug.Log(localUrl_);

        
        // 로컬의 번들 파일이 있다면 정보 가져오기
        if (File.Exists(localUrl_ + "/Bundle"))
        {
            AssetBundle bundle_ = AssetBundle.LoadFromFile(localUrl_ + "/Bundle");

            AssetBundleManifest manifest_ = bundle_.LoadAsset<AssetBundleManifest>("AssetBundleManifest");

            // 에셋번들의 목록 가져옴
            string[] localBundleArray_ = manifest_.GetAllAssetBundles();

            for (int i = 0; i < localBundleArray_.Length; i++)
            {
                oldAsset.Add(new AssetBundleInfo(localBundleArray_[i],
                    manifest_.GetAssetBundleHash(localBundleArray_[i])));
            }

            bundle_.Unload(true);
        }

        if (isNoUpdate)
        {
            // 개수가 같지 않으니 바로 업데이트
            for (int i = 0; i < oldAsset.Count; i++)
            {
                //checkVal++;

                _queue.Enqueue(StartCoroutine(SaveAndDownload(oldAsset[i].bundleName)));
            }

            yield break;
        }
        // 서버의 번들 파일 가져오기
        using (UnityWebRequest www = UnityWebRequest.Get(Define.SERVER_FILE_PATH + "Bundle"))
        {

            yield return www.SendWebRequest();

            string checkUrl_ = Application.persistentDataPath + Define.LOCAL_FILE_CHECK_PATH;
            // 서버에서 가져온 패치 파일 임시로 저장
            if (!Directory.Exists(checkUrl_))
            {
                Directory.CreateDirectory(checkUrl_);
            }

            File.WriteAllBytes(checkUrl_ + "/Bundle", www.downloadHandler.data);



            AssetBundle serverBundle_ = AssetBundle.LoadFromFile(checkUrl_ + "/Bundle");

            AssetBundleManifest serverManifest1_ = serverBundle_.LoadAsset<AssetBundleManifest>("AssetBundleManifest");


            // 에셋번들의 목록 가져옴
            string[] serverBundleArray_ = serverManifest1_.GetAllAssetBundles();

            for (int i = 0; i < serverBundleArray_.Length; i++)
            {
                newAsset.Add(new AssetBundleInfo(serverBundleArray_[i],
                    serverManifest1_.GetAssetBundleHash(serverBundleArray_[i])));
            }
        }


        /*
        //using (UnityWebRequest www = UnityWebRequest.Get(Define.SERVER_FILE_PATH + "Bundle" + ".manifest"))
        //{

        //    yield return www.SendWebRequest();

        //    if (!Directory.Exists(Define.LOCAL_FILE_PATH))
        //    {
        //        Directory.CreateDirectory(Define.LOCAL_FILE_PATH);
        //    }

        //    File.WriteAllBytes(Define.LOCAL_FILE_PATH + "/" + "Bundle" + ".manifest", www.downloadHandler.data);

        //    AssetBundle serverBundle_ = AssetBundle.LoadFromFile(Define.LOCAL_FILE_CHECK_PATH + "/Bundle");

        //    AssetBundleManifest serverManifest1_ = serverBundle_.LoadAsset<AssetBundleManifest>("AssetBundleManifest");


        //    // 에셋번들의 목록 가져옴
        //    string[] serverBundleArray_ = serverManifest1_.GetAllAssetBundles();

        //    for (int i = 0; i < serverBundleArray_.Length; i++)
        //    {
        //        newAsset.Add(new AssetBundleInfo(serverBundleArray_[i],
        //            serverManifest1_.GetAssetBundleHash(serverBundleArray_[i])));
        //    }

        //}
        */

        //checkVal++;

        
        _queue.Enqueue(StartCoroutine(SaveAndDownload("Bundle")));


        if (oldAsset.Count != newAsset.Count)
        {
            // 개수가 같지 않으니 바로 업데이트
            for (int i = 0; i < newAsset.Count; i++)
            {
                //checkVal++;

                _queue.Enqueue(StartCoroutine(SaveAndDownload(newAsset[i].bundleName)));
            }
        }
        else
        {
            for (int i = 0; i < oldAsset.Count; i++)
            {
                Debug.Log("로컬 해시 " + oldAsset[i].hashVal + " || 서버 해시 " + newAsset[i].hashVal);
                if (oldAsset[i].hashVal != newAsset[i].hashVal)
                {
                    // 값이 같지 않으니 다운
                    //checkVal++;
                    _queue.Enqueue(StartCoroutine(SaveAndDownload(newAsset[i].bundleName)));
                }
            }
        }
        


    }   // CheckUpdateAsset


    IEnumerator SaveAndDownload(string bundleName)
    {
        //checkTxt.text = "다운 받고 있어요";
        string localUrl_ = Application.persistentDataPath + Define.LOCAL_FILE_PATH;

        // 서버의 번들 파일 가져오기
        using (UnityWebRequest www = UnityWebRequest.Get(Define.SERVER_FILE_PATH + bundleName))
        {

            yield return www.SendWebRequest();

            if (!Directory.Exists(localUrl_))
            {
                Directory.CreateDirectory(localUrl_);
            }

            File.WriteAllBytes(localUrl_ + "/" + bundleName, www.downloadHandler.data);


        }

        _queue.Dequeue();

        if(_queue.Count == 0)
        {
            Invoke("LoadNextScene", 2f);
        }

    }   // SaveAndDownload

    public void LoadNextScene() 
    {
        //checkTxt.text = "로딩해야해요";

        SceneManager.LoadScene(Define.VR_LOBBY_SCENE);

    }

}





// 에셋번들 정보를 저장할 클래스
[System.Serializable]
public class AssetBundleInfo
{
    public string bundleName;
    public Hash128 hashVal;

    public AssetBundleInfo(string bundleName, Hash128 hashVal)
    {
        this.bundleName = bundleName;
        this.hashVal = hashVal;
    }
}
