using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using BNG;

public class AnimSpeedController : MonoBehaviour
{
    public List<Animator> nowAnimator = new List<Animator>();

    public float animSpeed = 1.0f;

    public TMP_Text playStopTxt;
    public TMP_Text nowSpeedTxt;

    public bool isPlay = false;
    public InputActionProperty PlusAnimButton;
    public InputActionProperty MinusAnimButton;
    public InputActionProperty RePlayAnimButton;
    public InputActionProperty ResetAnimButton;

    public UnityEngine.UI.Button closeBtn;

    private void Update()
    {
        if (PlusAnimButton.action.WasPressedThisFrame())
        {
            PlusSpeed();
        }

        if (MinusAnimButton.action.WasPressedThisFrame())
        {
            MinusSpeed();
        }

        if (RePlayAnimButton.action.WasPressedThisFrame())
        {
            RePlayAnim();
        }

        if (ResetAnimButton.action.WasPressedThisFrame())
        {
            ResetAnim();
        }
    }


    public void ResetSpeedVal()
    {
        animSpeed = 1.0f;
        nowSpeedTxt.text = "x " + animSpeed.ToString();

        
        foreach (var anim in nowAnimator)
        {
            anim.SetFloat("Speed", animSpeed);
           // anim.gameObject.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }

        if (!isPlay)
            RePlayAnim();

    }

    // 배속
    public void PlusSpeed()
    {
        if (2.0f <= animSpeed)
            return;

        animSpeed += 0.25f;
        nowSpeedTxt.text = "x " + animSpeed.ToString();

        if (!isPlay)
            return;

        foreach (var anim in nowAnimator)
        {
            anim.SetFloat("Speed", animSpeed);
        }
            //nowAnimator.SetFloat("Speed", animSpeed);

    }

    // 감속
    public void MinusSpeed()
    {
        if (animSpeed <= 0.25f)
            return;

        animSpeed -= 0.25f;
        nowSpeedTxt.text = "x " + animSpeed.ToString();

        if (!isPlay)
            return;
        foreach (var anim in nowAnimator)
        {
            anim.SetFloat("Speed", animSpeed);
        }

    }

    // 일시정지
    public void StopSpeed()
    {
        
        playStopTxt.text = "▶";

        foreach (var anim in nowAnimator)
        {
            anim.SetFloat("Speed", 0.0f);
        }
    }

    // 다시 재생
    public void RePlayAnim()
    {
        if (isPlay)
        {
            isPlay = false;
            StopSpeed();
        }
        else
        {
            isPlay = true;

            //nowAnimator.SetFloat("Speed", animSpeed);

            playStopTxt.text = "||";

            foreach (var anim in nowAnimator)
            {
                anim.SetFloat("Speed", animSpeed);
            }
        }
    }

    // 처음부터 재생
    public void ResetAnim()
    {
        ResetSpeedVal();
        //nowAnimator.Play("CH01_DS01_000_ljs01", -1, 0.0f);
        foreach (var anim in nowAnimator)
        {
            anim.Play(0, 0, 0f);
        }
        //nowAnimator.Play(0, 0, 0f);
    }
}
