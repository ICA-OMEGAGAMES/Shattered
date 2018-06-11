using System.Collections;
using UnityEngine;

[RequireComponent(typeof(MarkerManagerPlayer))]
[RequireComponent(typeof(Statistics))]
public class CharacterTransformer : MonoBehaviour
{
    public float transformationDuration = 1f;
    
    public GameObject devon;
    public GameObject malphas;
    public GameObject transformation;
   // public TransformAnimationScript animaterScript;

    //forms
    public enum CharacterForm
    {
        devon,
        transformToMalphas,
        malphas,
        transformToDevon,
    }
    public CharacterForm currentForm = CharacterForm.devon;


    private float transformationTimeStamp = 0;

    void Start()
    {
        SelectCharacter();
        //     animaterScript = transformation.GetComponent<TransformAnimationScript>();

    }

    void Update()
    {
        if (Input.GetButton(Constants.TRANSFORM_BUTTON) && transformationTimeStamp <= Time.time)
        {
            SwitchForm();
            transformationTimeStamp = Time.time + transformationDuration;
        }
    }

    private void SwitchForm()
    {
        switch (currentForm)
        {
            case (CharacterForm.devon):
                currentForm = CharacterForm.transformToMalphas;
                SelectCharacter();
                break;
            case (CharacterForm.transformToMalphas):
                currentForm = CharacterForm.malphas;
                SelectCharacter();
                break;
            case (CharacterForm.malphas):
                currentForm = CharacterForm.transformToDevon;
                SelectCharacter();
                break;
            case (CharacterForm.transformToDevon):
                currentForm = CharacterForm.devon;
                SelectCharacter();
                break;
        }
    }

    void SelectCharacter()
    {
        //switch the active states of the player character
        switch (currentForm)
        {
            case (CharacterForm.devon):
                //set position and rotation to the previous character
                devon.transform.position = transformation.transform.position;
                devon.transform.rotation = transformation.transform.rotation;

                //Change from Malphas to Devon
                devon.SetActive(true);
                malphas.SetActive(false);
                transformation.SetActive(false);

                break;
            case (CharacterForm.transformToMalphas):
                //Change from Davon to Transformation
                devon.SetActive(false);
                malphas.SetActive(false);
                transformation.SetActive(true);
                //   animaterScript.SetToMalphas(true);
                //set position and rotation to the previous character
                transformation.transform.position = devon.transform.position;
                transformation.transform.rotation = devon.transform.rotation;

                StartCoroutine(StartTransformation(CharacterForm.malphas));

                //give combat status
                if (malphas.GetComponent<CharacterMovement>().CombatState != devon.GetComponent<CharacterMovement>().CombatState)
                    malphas.GetComponent<CharacterMovement>().SwitchCombatState();
                break;
            case (CharacterForm.malphas):
                //Change from Transformation to Malphas
                devon.SetActive(false);
                malphas.SetActive(true);
                transformation.SetActive(false);

                //set position and rotation to the previous character
                malphas.transform.position = transformation.transform.position;
                malphas.transform.rotation = transformation.transform.rotation;
                break;
            case (CharacterForm.transformToDevon):
                //Change from Davon to Transformation
                devon.SetActive(false);
                malphas.SetActive(false);
                transformation.SetActive(true);
                // animaterScript.SetToMalphas(false);

                //set position and rotation to the previous character
                transformation.transform.position = malphas.transform.position;
                transformation.transform.rotation = malphas.transform.rotation;

                StartCoroutine(StartTransformation(CharacterForm.devon));
                //give combat status
                if (malphas.GetComponent<CharacterMovement>().CombatState != devon.GetComponent<CharacterMovement>().CombatState)
                    devon.GetComponent<CharacterMovement>().SwitchCombatState();
                break;
        }
    }

    private IEnumerator StartTransformation(CharacterForm targetForm)
    {
        yield return new WaitForSeconds(transformationDuration);
        currentForm = targetForm;
        SelectCharacter();
    }
}
