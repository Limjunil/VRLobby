using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VRMenuManager : MonoBehaviour
{
    private VRContentManager vrContent;


    // Start is called before the first frame update
    void Start()
    {
        GameObject temp_ = gameObject.transform.parent.gameObject;
        GameObject tempParent_ = temp_.transform.parent.gameObject;

        vrContent = tempParent_.GetComponent<VRContentManager>();
        vrContent.AddVRUIList(gameObject);
    }

    public void NextSceneOpen(string sceneName)
    {
        if (sceneName == null)
        {
            sceneName = Define.VR_SCENE_TWO;
        }

        SceneManager.LoadScene(sceneName);
    }   // NextSceneOpen()
}
