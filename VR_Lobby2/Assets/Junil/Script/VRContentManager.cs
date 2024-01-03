using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class VRContentManager : MonoBehaviour
{
    private float spawnDistance = 2f;

    public Transform playerHead;
    public InputActionProperty showButton;

    private void Start()
    {
        if (gameObject.activeSelf)
        {
            gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (showButton.action.WasPressedThisFrame())
        {
            gameObject.SetActive(!gameObject.activeSelf);

            gameObject.transform.position = playerHead.position +
                new Vector3(playerHead.forward.x, 0, playerHead.forward.z).normalized * spawnDistance;
        }
        
        gameObject.transform.LookAt(new Vector3(playerHead.position.x, gameObject.transform.position.y, playerHead.position.z));
        gameObject.transform.forward *= -1;
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
