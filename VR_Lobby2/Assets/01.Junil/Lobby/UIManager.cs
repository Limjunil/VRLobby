using BNG;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{

    public float spawnDistance = 3f;

    public Transform playerHead;
    public InputActionProperty showButton;

    public GameObject vruiCanvas;
    public GameObject createUI;
    public GameObject createRightUI;


    public GameObject vrHandUI;
    // 임시 추후 에셋번들로 가져오면 바꿀 예정
    public GameObject vrAnim;

    public bool isAnimPlay = false;


    private void Start()
    {
        spawnDistance = 3f;
        //SetVRUI();
    }

    // Update is called once per frame
    void Update()
    {
        if (vruiCanvas == null)
            return;

        float checkVal = (vruiCanvas.transform.position - playerHead.transform.position).magnitude;

        if (6f < checkVal && vruiCanvas.activeSelf == true && createUI.activeSelf == true)
        {
            OnOffMainUI();
        }

        if (showButton.action.WasPressedThisFrame() && isAnimPlay == false || Input.GetKeyDown(KeyCode.L))
        {
            OnOffMainUI();

            vruiCanvas.transform.position = playerHead.position +
            new Vector3(playerHead.forward.x, -0.2f, playerHead.forward.z).normalized * spawnDistance;

        }

        if (vruiCanvas.activeSelf == false)
            return;
        
        vruiCanvas.transform.LookAt(new Vector3(playerHead.position.x,
            vruiCanvas.transform.position.y, playerHead.position.z));

        vruiCanvas.transform.forward *= -1;
    }

    public void OffUICanvas()
    {
        //vruiCanvas.SetActive(!vruiCanvas.activeSelf);

        int tempNum_ = vruiCanvas.transform.childCount;

        for(int i = 0; i < tempNum_; i++)
        {
            vruiCanvas.transform.GetChild(i).gameObject.SetActive(false);

        }
    }

    public void OnOffMainUI()
    {
        createUI.SetActive(!createUI.activeSelf);
    }

    public void OnOffRightUI()
    {
        createRightUI.SetActive(!createRightUI.activeSelf);

    }

    public void SetVRUI()
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case Define.VR_LOBBY_SCENE:
                AssetBundleDataManager.Instance.SetBundle("vrui");

                GetSetVRUI("VRUI_Menu");

                break;

            case Define.VR_SCENE_TWO:
                AssetBundleDataManager.Instance.SetBundle("vrdance");

                GetSetVRUI("VRUI_Dance");

                break;

            case Define.VR_SCENE_THREE:

                break;

            case Define.VR_SCENE_FOUR:
                AssetBundleDataManager.Instance.SetBundle("vrbellydance");
                                
                GetSetVRUI("VRUI_Belly");
                break;

            default:
                break;
        }

        //var Obj_ = Instantiate(vruiTemp_, vruiCanvas.transform);
        //OffUICanvas();
        //if (vruiCanvas.activeSelf)
        //{
        //    vruiCanvas.SetActive(false);
        //}
    }

    public void GetSetVRUI(string uiName)
    {
        AssetBundleDataManager.Instance.SetBundleData();
        GameObject vruiTemp_ = AssetBundleDataManager.Instance.GetAssetUIObj("vrui", uiName);
        var Obj_ = Instantiate(vruiTemp_, createUI.transform);
    }

}
