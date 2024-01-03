using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
#if UNITY_ANDROID
        Debug.Log("안드로이드");
#elif UNITY_EDITOR_WIN
        Debug.Log("윈도우");
#endif
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
