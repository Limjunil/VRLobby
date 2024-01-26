using BNG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.OpenXR.Input;

public class VRDanceUIManager : MonoBehaviour
{
    public string bundleName;
    public GameObject animUICanvas;
    private VRContentManager vrContent;
    private UIManager uiManager;

    private List<GameObject> danceObj = new List<GameObject>();

    private Transform mainCam;

    private AnimSpeedController animController;

    private int[] dirZ = new int[] { -1, 1, 0, 0 };
    private int[] dirX = new int[] { 0, 0, -1, 1 };

    
    private void Start()
    {
        bundleName = "vrdance";

        GameObject temp_ = gameObject.transform.parent.gameObject;
        GameObject tempParent_ = temp_.transform.parent.gameObject;

        vrContent = tempParent_.GetComponent<VRContentManager>();
        Debug.Log("실행은 되유");
        uiManager = vrContent.uiManager;
        GameObject anim_ = uiManager.vrAnim;
        animController = anim_.GetComponentInChildren<AnimSpeedController>();

        //animController = tempUImanager_.vrAnim.GetComponent<AnimSpeedController>();
        animController.closeBtn.onClick.AddListener(() => CloseDance());

        animUICanvas = animController.gameObject;
        Debug.Log(animUICanvas);

        if (animUICanvas.activeSelf)
        {

            animUICanvas.SetActive(false);
        }


        mainCam = GameObject.FindWithTag("MainCamera").transform;

        vrContent.AddVRUIList(gameObject);
        uiManager.OffUICanvas();
    }
    public void CloseDance()
    {
        vrContent.StopPlayerMove(true);
        vrContent.StopInputPlayer(true);
        uiManager.isAnimPlay = false;

        animUICanvas.SetActive(false);

        foreach (var anim in danceObj)
        {
            
            anim.SetActive(false);

        }
    }

    public void ShowDanceModel(string useObjName)
    {
        vrContent.StopPlayerMove(false);
        vrContent.StopInputPlayer(false);
        uiManager.OffUICanvas();
        uiManager.isAnimPlay = true;
        animUICanvas.SetActive(true);

        GameObject tempModel_ = AssetBundleDataManager.Instance.GetAssetUIObj(bundleName, useObjName);

        if (tempModel_ == null)
        {
            return;
        }
        //vrContent.OpenRightUI();


        if(danceObj.Count < 4)
        {
            for (int i = 0; i < 4; i++)
            {
                danceObj.Add(Instantiate(tempModel_));
                //danceObj[i].transform.localEulerAngles = new Vector3(0f, 180f, 0f);
                animController.nowAnimator.Add(danceObj[danceObj.Count - 1].transform.GetComponent<Animator>());
            }
        }


        foreach(var anim in danceObj)
        {
            Vector3 temp_ = mainCam.position;
            temp_.y = -0.7f;
            anim.SetActive(true);
            anim.transform.position = temp_;

        }

        for (int i = 0; i < 4; i++)
        {
            int nextZ_ = dirZ[i];
            int nextX_ = dirX[i];

            Vector3 dist_ = new Vector3(nextX_, 0f, nextZ_).normalized * 5f;
            Debug.Log(dist_);

            danceObj[i].transform.position += dist_;
        }
        
        animController.ResetSpeedVal();

    }   // ShowDanceModel()

    public void BackBtn()
    {
        vrContent.OnVRUIMenu();
    }

}
