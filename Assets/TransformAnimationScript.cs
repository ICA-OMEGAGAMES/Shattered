using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformAnimationScript : MonoBehaviour {

    private Animator devonTransform;
    private Animator malphasTransform;

    public string animationParemeter = "isToMalphas";
	// Use this for initialization
	void Start () {
        devonTransform = this.gameObject.transform.GetChild(0).GetComponent<Animator>();
        malphasTransform = this.gameObject.transform.GetChild(1).GetComponent<Animator>();
    }
	
	// Update is called once per frame
	public void SetToMalphas(bool toMalphas) { 
        devonTransform.SetBool(animationParemeter, toMalphas);
        devonTransform.SetBool(animationParemeter, toMalphas);
    }

}
