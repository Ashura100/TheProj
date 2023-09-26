using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine;

public class Paramètre : MonoBehaviour
{
    public AudioMixer audioMixer;

    [SerializeField]
    Slider levelMaster;

    public Dropdown resolutionDrop;

    Resolution[] resolutions;

    public void Start()
    {
        resolutions = Screen.resolutions;
        resolutionDrop.ClearOptions();
        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDrop.AddOptions(options);
        resolutionDrop.value = currentResolutionIndex;
        resolutionDrop.RefreshShownValue();
    }
    public void SetVolume()
    {
        audioMixer.SetFloat("Volume", levelMaster.value);
    }

    public void SetPleinEcran(bool isPleinEcran)
    {
        Screen.fullScreen = isPleinEcran;
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}
