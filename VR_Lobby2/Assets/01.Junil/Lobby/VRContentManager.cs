using BNG;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class VRContentManager : MonoBehaviour
{

    public List<GameObject> vruiObjs = new List<GameObject>();
    public GameObject vruiMenu;
    public GameObject vruiDance;
    public UIManager uiManager;
    public SmoothLocomotion playerMoveController;


    private void Start()
    {

        //if (SceneManager.GetActiveScene().name == Define.VR_LOBBY_SCENE)
        //    return;
        //DataManager.Instance.SetBundleData();
        //uiManager.SetVRUI();
    }

    public void OnVRUIMenu()
    {
        //로비로
        SceneManager.LoadScene(Define.VR_LOBBY_SCENE);
        //AllOffUI();
        //vruiObjs[0].SetActive(true);
    }

    public void AllOffUI()
    {
        foreach(GameObject ui_ in vruiObjs)
        {
            ui_.SetActive(false);
            Debug.Log("작동함");
        }
    }

    public void AddVRUIList(GameObject vruiObj)
    {
        AllOffUI();
        vruiObjs.Add(vruiObj);
    }

    public void OffVRUI()
    {
        uiManager.OffUICanvas();
    }

    public void OpenMainUI()
    {
        uiManager.createUI.SetActive(true);
    }

    public void StopPlayerMove(bool isPlay)
    {
        playerMoveController.UpdateMovement = isPlay;
    }

    public void StopInputPlayer(bool isInput)
    {
        playerMoveController.AllowInput = isInput;
    }

}
