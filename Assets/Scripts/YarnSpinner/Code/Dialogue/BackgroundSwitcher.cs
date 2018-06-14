using UnityEngine;
using System.Collections;
using Yarn.Unity.Shattered;
using UnityEngine.UI;
/// Attach sprite renderer to game object

[RequireComponent(typeof(SpriteRenderer))]
/// Attach SpriteSwitcher to game object
public class BackgroundSwitcher : MonoBehaviour
{
    [System.Serializable]
    public struct BackgroundInfo
    {
        public string name;
        public Sprite background;
    }

    public BackgroundInfo[] backgrounds;

    public Image background;
	public GameObject backgroundCanvas;

    /// Create a command to use on a sprite
    [YarnCommand("setBackground")]
    public void UseBackground(string backgroundName)
    {
        Sprite s = null;
        foreach (var info in backgrounds)
        {
            if (info.name == backgroundName)
            {
                s = info.background;
                break;
            }
        }
        if (s == null)
        {
            Debug.LogErrorFormat("Can't find sprite named {0}!", backgroundName);
            return;
        }

        backgroundCanvas.SetActive(true);

        background.sprite = s;
    }
}