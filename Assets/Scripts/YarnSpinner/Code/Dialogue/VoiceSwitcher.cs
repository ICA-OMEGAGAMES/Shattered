using UnityEngine;
using System.Collections;
using Yarn.Unity.Shattered;
using UnityEngine.UI;

public class VoiceSwitcher : MonoBehaviour
{
    [System.Serializable]
    public struct VoiceInfo
    {
        public string name;
        public AudioClip voice;
    }

    public VoiceInfo[] voices;
    private float duration;
    private bool isRunning = false;
    private IEnumerator coroutine;

    void Start()
    {
        coroutine = WaitForSound();
    }

    [YarnCommand("setVoice")]
    public void SetVoice(string voiceName)
    {
        if (isRunning)
        {
            StopCoroutine(coroutine);
        }

        if (voiceName == "dialogueFinished")
        {
            StopCoroutine(coroutine);
            return;
        }

        AudioClip tempClip = null;

        foreach (var info in voices)
        {
            if (info.name == voiceName)
            {
                tempClip = info.voice;
                break;
            }
        }

        if (tempClip == null)
        {
            Debug.LogErrorFormat("Can't find voice named {0}!", voiceName);
            return;
        }

        AudioSource audio = GetComponent<AudioSource>();
        audio.clip = tempClip;
        audio.Play();
        isRunning = true;

        duration = tempClip.length;

        StartCoroutine(coroutine);
    }

    IEnumerator WaitForSound()
    {
        yield return new WaitForSeconds(duration);
        isRunning = false;
    }
}
