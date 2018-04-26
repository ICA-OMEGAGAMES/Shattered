using System;
using System.Collections;
using UnityEngine;

public class CharacterTransformer : MonoBehaviour {

    [System.Serializable]
    public class TransformationSettings
    {
        public float transformationDuration = 0.5f;
    }
    [SerializeField]
    public TransformationSettings transformationSettings;

    public GameObject devon;
    public GameObject malphas;
    public GameObject transformation;

    //forms
    public enum characterForm
    {
        devon,
        transformToMalphas, 
        malphas,
        transformToDevon,
    }
    public characterForm currentForm = characterForm.devon;


    private float transformationTimeStamp = 0;

    void Start()
    {
        SelectCharacter();
    }

    void Update()
    {
        if (Input.GetButton(Constants.TRANSFORM_BUTTON) && transformationTimeStamp <= Time.time)
        {
            SwitchForm();
            transformationTimeStamp = Time.time + transformationSettings.transformationDuration;
        }
    }

    private void SwitchForm()
    {
        switch (currentForm)
        {
            case (characterForm.devon):
                currentForm = characterForm.transformToMalphas;
                SelectCharacter();
                break;
            case (characterForm.transformToMalphas):
                currentForm = characterForm.malphas;
                SelectCharacter();
                break;
            case (characterForm.malphas):
                currentForm = characterForm.transformToDevon;
                SelectCharacter();
                break;
            case (characterForm.transformToDevon):
                currentForm = characterForm.devon;
                SelectCharacter();
                break;
        }
    }

    void SelectCharacter()
    {
        //switch the active states of the player character
        switch (currentForm)
        {
            case (characterForm.devon):
                //Change from Malphas to Devon
                devon.SetActive(true);
                malphas.SetActive(false);
                transformation.SetActive(false);

                //set position and rotation to the previous character
                devon.transform.position = malphas.transform.position;
                devon.transform.rotation = malphas.transform.rotation;
                break;
            case (characterForm.transformToMalphas):
                //Change from Davon to Transformation
                devon.SetActive(false);
                malphas.SetActive(false);
                transformation.SetActive(true);

                //set position and rotation to the previous character
                transformation.transform.position = devon.transform.position;
                transformation.transform.rotation = devon.transform.rotation;

                StartCoroutine(startTransformation(characterForm.malphas));
                break;
            case (characterForm.malphas):
                //Change from Transformation to Malphas
                devon.SetActive(false);
                malphas.SetActive(true);
                transformation.SetActive(false);

                //set position and rotation to the previous character
                malphas.transform.position = transformation.transform.position;
                malphas.transform.rotation = transformation.transform.rotation;
                break;
            case (characterForm.transformToDevon):
                //Change from Davon to Transformation
                devon.SetActive(false);
                malphas.SetActive(false);
                transformation.SetActive(true);

                //set position and rotation to the previous character
                transformation.transform.position = malphas.transform.position;
                transformation.transform.rotation = malphas.transform.rotation;

                StartCoroutine(startTransformation(characterForm.devon));
                break;
        }
    }

    private IEnumerator startTransformation(characterForm targetForm)
    {
        yield return new WaitForSeconds(transformationSettings.transformationDuration);
        currentForm = targetForm;
        SelectCharacter();
    }
}
