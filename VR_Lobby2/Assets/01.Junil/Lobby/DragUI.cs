using BNG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragUI : MonoBehaviour, IDragHandler
{
    public UIPointer rightUIPointer;

    public Transform pointer;

    [Header("Select To Include In Drag")]
    public bool axisX;
    public bool axisY;
    public bool axisZ;

    private void Start()
    {
        pointer = rightUIPointer.cursor.transform;
    }
    public void OnDrag(PointerEventData eventData)
    {
        
    }

    public void OnDragUI()
    {
        Debug.Log(pointer.position);
        float newAxisX_ = axisX ? pointer.position.x : transform.position.x;
        float newAxisY_ = axisX ? pointer.position.y : transform.position.y;
        float newAxisZ_ = axisX ? pointer.position.z : transform.position.z;

        gameObject.transform.position = new Vector3(newAxisX_, newAxisY_, newAxisZ_);
    }
}
