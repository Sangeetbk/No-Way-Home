using UnityEngine;
using UnityEngine.NVIDIA;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.UI;



public class GraphicSettings : MonoBehaviour
{
    [Header("Brightness & Contrast")]
    public float Brightness = 1.0f;
    public float Contrast = 1.0f;


    [Header("Video Settings")]
    public int qualitySetting;

   

    public bool Use_DLSS;
    public bool use_FSR;

    // Post process setting demo
    public bool use_Bloom;
    public bool use_DepthofField;
    public bool use_MotionBlur;
    public bool use_FilmGrain;
    public bool use_Vignette;
    public bool use_SSR;
   // public bool use_SSGI;
    public bool use_SSAO;

    public Volume volume;
    private ColorAdjustments colorAdjustments;
    private Bloom bloom;
    private FilmGrain grain;
    private Vignette vignette;
    private DepthOfField dof;
    private MotionBlur motion;
    private ScreenSpaceAmbientOcclusion SSAO;
    private ScreenSpaceReflection SSR;
    

    private DLSSQuality dlssQuality;

    private void Update()
    {
      
        SetQualityLevel(qualitySetting);

        Debug.Log(QualitySettings.GetQualityLevel().ToString());

        SetBrightnessContrast();

        // ResolutionUpscaling();
        PostProcessSettings();

    }

    public void ApplyvSync(int vSyncCount)
    {
        // 0 = off, 2 = half, 1 = on
        QualitySettings.vSyncCount = vSyncCount;
    }

    public void SetQualityLevel(int level)
    {
        switch (level)
        {
            case 0:
                QualitySettings.SetQualityLevel(0);
                break;

            case 1:
                QualitySettings.SetQualityLevel(1);
                break;

            case 2:
                QualitySettings.SetQualityLevel(2);
                break;

            case 3:
                QualitySettings.SetQualityLevel(3);
                break;

            case 4:
                QualitySettings.SetQualityLevel(4);
                break;
        }
    }

    public void SetBrightnessContrast()
    {
        volume.profile.TryGet(out colorAdjustments);
       colorAdjustments.postExposure.value = Brightness;
        colorAdjustments.contrast.value = Contrast;

    }

    public void ResolutionUpscaling()
    {
        if(use_FSR)
        {
            Camera.main.allowDynamicResolution = true;
            
        }
        else
        {
            Camera.main.allowDynamicResolution = false;
        }
       
    }

    public void PostProcessSettings()
    {
        volume.profile.TryGet(out bloom);
        volume.profile.TryGet(out SSAO);
        volume.profile.TryGet(out SSR);
        volume.profile.TryGet(out motion);
        volume.profile.TryGet(out grain);
        volume.profile.TryGet(out dof);
        volume.profile.TryGet(out vignette);

        if (use_Bloom)  // 1
        {
            bloom.active = true;
        }
        else
        {
            bloom.active = false;
        }

        if(use_SSAO)   // 2 
        {
            SSAO.active = true;
        }
        else
        {
            SSAO.active = false;
        }

        if (use_SSR)  // 3
        {
            SSR.active = true; 
        }
        else
        {
            SSR.active = false;
        }

        if (use_MotionBlur)  // 4
        {
            motion.active = true;
        } 
        else 
        { 
            motion.active = false;
        }

        if (use_FilmGrain)  // 5
        {
            grain.active = true;
        }
        else 
        { 
            grain.active = false; 
        }

        if (use_Vignette) // 6
        { 
            vignette.active = true;
        } 
        else 
        { 
            vignette.active = false; 
        }


        if(use_DepthofField) // 7
        { 
            dof.active = true; 
        } 
        else 
        { 
            dof.active = false; 
        }

    }
}
