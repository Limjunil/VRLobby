using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRBellyManager : MonoBehaviour
{
    public string bundleName;
    private VRContentManager vrContent;

    //private List<GameObject> bellyObj = new List<GameObject>();
    private GameObject[] bellyObjs = new GameObject[4];
    private Transform mainCam;

    public string[] test;

    // Start is called before the first frame update
    void Start()
    {
        bundleName = "vrbellydance";

        GameObject temp_ = gameObject.transform.parent.gameObject;
        GameObject tempParent_ = temp_.transform.parent.gameObject;

        vrContent = tempParent_.GetComponent<VRContentManager>();
        mainCam = GameObject.FindWithTag("MainCamera").transform;
    }

    public void ShowBellyModel(string useObjName)
    {
        AllOffModel();

        //// 밸리 파일명을 보고 가져오는거
        int tempStrNum_ = int.Parse(useObjName.Substring(7, 2));
        //Debug.Log(tempStrNum_);

        if(bellyObjs[tempStrNum_] == null)
        {
            GameObject tempModel_ = AssetBundleDataManager.Instance.GetAssetUIObj(bundleName, useObjName);

            bellyObjs[tempStrNum_] = Instantiate(tempModel_);
        }

        bellyObjs[tempStrNum_].gameObject.SetActive(true);

        vrContent.OffVRUI();

        //danceObj.transform.eulerAngles = new Vector3(0, -180, 0);

        bellyObjs[tempStrNum_].transform.position = new Vector3(gameObject.transform.position.x
            , 0, gameObject.transform.position.z);

        Vector3 dist_ = (mainCam.position - bellyObjs[tempStrNum_].transform.position).normalized * -1;
        bellyObjs[tempStrNum_].transform.position += new Vector3(dist_.x, 0, dist_.z);

        //bellyObj[tempStrNum_].transform.eulerAngles = new Vector3(0, -180, 0);
    }

    public void AllOffModel()
    {
        if (bellyObjs.Length <= 0 || bellyObjs == default || bellyObjs == null)
            return;

        foreach (var model in bellyObjs)
        {
            if (model == null)
            {
                /* Do Nothing */
            }
            else
            {
                model.gameObject.SetActive(false);

            }
            
        }
    }

    public void BackBtn()
    {
        vrContent.OnVRUIMenu();
    }
}
