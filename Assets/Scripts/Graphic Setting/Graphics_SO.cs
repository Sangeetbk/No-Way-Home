using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SGS.SettingsMenu
{

    [CreateAssetMenu(fileName = "New Graphic Settings", menuName = "SGS/GraphicSetting")]
    public class Graphics_SO : ScriptableObject
    {
        public TextureQuality textureQuality;
        public ShadowQuality shadowQuality;
        public CloudQualtiy cloudQuality;
        public BloomMode bloomMode;
        public MotionBlurMode motionBlurMode;
        public WaterQuality waterQuality;

    }

    [System.Serializable]
    public enum TextureQuality
    {
        Low, Medium, High, 
    }

    [System.Serializable]
    public enum ShadowQuality
    {
        Low, Medium, High, 
    }

    [System.Serializable]
    public enum CloudQualtiy
    {
        Low, Medium, High, 
    }

    [System.Serializable]
    public enum BloomMode
    {
        Off, Low, Medium, High,
    }

    [System.Serializable]
    public enum MotionBlurMode
    {
        None, Medium, High, 
    }

    [System.Serializable]
    public enum WaterQuality
    {
        Low, Medium, High, 
    }

}