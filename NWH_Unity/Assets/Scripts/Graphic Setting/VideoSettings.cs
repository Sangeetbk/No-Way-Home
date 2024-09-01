using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.Rendering;

public class VideoSettings : MonoBehaviour
{
    public Volume volume;
    private ColorAdjustments colorAdjustments;

    private Resolution[] resolutions;
    public TMP_Dropdown resolutionDropdown;
    float currentAspectRatio;
    private HDAdditionalCameraData _additionalCameraData;
    private void Start()
    {
        CalculateResolutions();
        SetASpectRation((int)currentAspectRatio);
        _additionalCameraData = Camera.main.GetComponent<HDAdditionalCameraData>();


    }
    private void CalculateResolutions()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        int currentResolutionIndex = 0;
        List<string> options = new List<string>();
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " X " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                    resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        FullScreenMode currentFullScreenMode = Screen.fullScreenMode;
        Screen.SetResolution(resolution.width, resolution.height, currentFullScreenMode);
    }
    public void ChangeRefreshRate(int refreshRateIndex)
    {

        switch (refreshRateIndex)
        {
            case 0:
                Application.targetFrameRate = 60;
                break;

            case 1:
                Application.targetFrameRate = 120;
                break;

            case 2:
                Application.targetFrameRate = 200;
                break;
        }
    }
    public void SetASpectRation(int aspectRatioIndex)
    {
        switch (aspectRatioIndex)
        {
            case 0:
                Camera.main.aspect = 16f / 9f;
                break;

            case 1:
                Camera.main.aspect = 16f / 10f;
                break;
            case 2:
                Camera.main.aspect = 4f / 3f;
                break;
        }

        currentAspectRatio = Camera.main.aspect;
        Debug.Log(currentAspectRatio);
    }
  
    public void SetAntiAliasing(int antiAliasingIndex)
    {
        if (_additionalCameraData != null)
        {

            switch (antiAliasingIndex)
            {
                case 0:
                    _additionalCameraData.antialiasing = HDAdditionalCameraData.AntialiasingMode.FastApproximateAntialiasing;
                    break;
                case 1:
                    _additionalCameraData.antialiasing = HDAdditionalCameraData.AntialiasingMode.SubpixelMorphologicalAntiAliasing;
                    break;
                case 2:
                    _additionalCameraData.antialiasing = HDAdditionalCameraData.AntialiasingMode.TemporalAntialiasing;
                    break;
            }
        }

    }

    

    public void SetWindowMode(int windowMode)
    {
        switch (windowMode)
        {
            case 0:
                Screen.fullScreenMode = FullScreenMode.Windowed;
                break;

            case 1:
                Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
                break;

            case 2:
                Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
                break;

        }
    }
    public void SetScreenResolution(int resolutionIndex)
    {
        Screen.SetResolution(Screen.resolutions[resolutionIndex].width, Screen.resolutions[resolutionIndex].height, true);
    }

    public void ApplyvSync(int vSyncCount)
    {
        // 0 = off, 2 = half, 1 = on
        QualitySettings.vSyncCount = vSyncCount;

        switch (vSyncCount)
        {
            case 0:
                QualitySettings.vSyncCount = 0;
                break;

            case 1:
                QualitySettings.vSyncCount = 1;
                break;

            case 2:
                QualitySettings.vSyncCount = 2;
                break;
        }
    }

    public void SetBrightnessContrast(float brightness)
    {
        volume.profile.TryGet(out colorAdjustments);
        colorAdjustments.postExposure.value = brightness;
        // colorAdjustments.contrast.value = Contrast;
    }

    public void ResolutionUpscaling(int resolutionIndex)
    {
        switch (resolutionIndex)
        {
            case 0:
                _additionalCameraData.allowDynamicResolution = false;
                break;

            case 1:
                Debug.Log("No FSR Detected");
                break;

            case 2:
                _additionalCameraData.allowDynamicResolution = true;
                _additionalCameraData.allowDeepLearningSuperSampling = true;
                break;
        }

    }

    public void SetUpscaledResolutionQuality(int qualityIndex)
    {
        if (_additionalCameraData.allowDeepLearningSuperSampling)
        {
            switch (qualityIndex)
            {
                case 0:
                    _additionalCameraData.deepLearningSuperSamplingQuality = 0;
                    break;

                case 1:
                    _additionalCameraData.deepLearningSuperSamplingQuality = 1;
                    break;

                case 2:
                    _additionalCameraData.deepLearningSuperSamplingQuality = 2;
                    break;

                case 3:
                    _additionalCameraData.deepLearningSuperSamplingQuality = 3;
                    break;
            }
        }
    }

}
