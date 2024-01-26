using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;
using static NetworkManager;


public class MySqlSelectTest : MonoBehaviour
{
    

    // Start is called before the first frame update
    void Start()
    {        
        //url = "http://34.217.102.173:6023/";

        //ListGameData("GdStage");

        //Http_GET(url + "GameData/GetVersion", GetHTTPParamsString(), test);
    }

    /// <summary>
    /// 버전 정보 조회
    /// </summary>
    public void GetVersion()
    {
        APIRequest request = new APIRequest("GameData/GetVersion");
        request.signature = false;
        ApiUpDate(request);
    }


    /// <summary>
    /// 게임 테이블 요청 (GameData/ListGameData)
    /// </summary>
    /// <param name="tableName">테이블 이름</param>
    public void ListGameData(string tableName)
    {
        APIRequest request = new APIRequest("GameData/ListGameData");
        request.parameters["tableName"] = tableName;
        request.showsIndicator = false;
        request.signature = false;
        ApiUpDate(request);
    }

    public void ApiUpDate(APIRequest apiName)
    {
        
        string uri = string.Format("{0}?{1}", apiName.url, apiName.GetHTTPParamsString());
        
        Http_GET(uri, testCallBack);
    }


    public void Http_GET(string uri, Action<string, string> callback)
    {
        StartCoroutine(WaitForRequest(uri, callback));
    }

    public void DELETE(string uri, Hashtable htRequestHeader, Action<string, string> callback)
    {
        UnityWebRequest request = UnityWebRequest.Delete(uri);
        request.SetRequestHeader("Content-Type", "text/json");

        if (htRequestHeader != null)
        {
            IDictionaryEnumerator e = htRequestHeader.GetEnumerator();
            while (e.MoveNext())
                request.SetRequestHeader(e.Key.ToString(), e.Value.ToString());
        }
        //StartCoroutine(WaitForRequest(request, callback));
    }

    IEnumerator WaitForRequest(string uri, Action<string, string> callback)
    {
        using (UnityWebRequest www = UnityWebRequest.Get(uri))
        {
            www.downloadHandler = new DownloadHandlerBuffer();
            www.timeout = 10;

            yield return www.SendWebRequest();
            Debug.Log(www.result);
            // null이 아닐때만 호출
            callback?.Invoke(www.error, www.downloadHandler.text);
        }
    }

    public void testCallBack(string a, string b)
    {
        Debug.Log("Error : " + a);
        Debug.Log("암호화 : " + b);
        Debug.Log(AES256Cipher.Decrypt(b));
        //DataManager.Instance.versonList = JsonUtility.FromJson<VersionRoot>(AES256Cipher.Decrypt(b));
        AssetBundleDataManager.Instance.gdStageList = JsonUtility.FromJson<GdStageRoot>(AES256Cipher.Decrypt(b));

    }   // testCallBack
}

