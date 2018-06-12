using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformAnimationScript : MonoBehaviour {

    public Animator devonAnimator;
    public Animator malphasAnimator;

    public string animationParemeter = "isToMalphas";
    // Use this for initialization
    void Start()
    {
        devonAnimator = GameObject.Find("DevonTransform").GetComponent<Animator>();
        malphasAnimator = GameObject.Find("MalphasTransform").GetComponent<Animator>();
    }

    // Update is called once per frame
    public void SetToMalphas(bool toMalphas)
    {
        devonAnimator.SetBool(animationParemeter, toMalphas);
        malphasAnimator.SetBool(animationParemeter, toMalphas);
    }

}