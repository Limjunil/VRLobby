using BNG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowHeadUI : MonoBehaviour
{
    public float spawnDistance = 4f;

    public Transform playerHead;


    private void OnEnable()
    {
        //SetPosUI();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SetPosUI();
    }


    public void SetPosUI()
    {
        gameObject.transform.position = playerHead.position +
            new Vector3(playerHead.forward.x, 0, playerHead.forward.z).normalized * spawnDistance;
    }
}
