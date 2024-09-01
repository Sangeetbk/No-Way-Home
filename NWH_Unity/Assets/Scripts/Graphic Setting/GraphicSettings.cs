using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.NVIDIA;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.Rendering;
using UnityEngine.UI;
using TMPro;
using System;

namespace SGS.SettingsMenu
{
    public class GraphicSettings : MonoBehaviour
    {
        // 4 scriptable object eg, Low, Medium, High, Ultra
        public List<Graphics_SO> graphicsList;

        private Graphics_SO _currentGraphics;

        public TMP_Dropdown textureQuality;
        public TMP_Dropdown cloudQuality;
        public TMP_Dropdown bloom;
        public TMP_Dropdown motionBlur;
       // public TMP_Dropdown filmGrain;
        public TMP_Dropdown shadowQuality;

        private void Start()
        {
            textureQuality.value = (int)_currentGraphics.textureQuality;
            cloudQuality.value = (int)_currentGraphics.cloudQuality;
            bloom.value = (int)_currentGraphics.bloomMode;
            motionBlur.value = (int)_currentGraphics.motionBlurMode;
           // filmGrain.value = (int)CurrentGraphics.filmGrain;
            shadowQuality.value = (int)_currentGraphics.shadowQuality;
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                ApplySetting();
            }
        }

        private void ApplySetting()
        {
            textureQuality.value = (int)_currentGraphics.textureQuality;
            cloudQuality.value = (int)_currentGraphics.cloudQuality;
            bloom.value = (int)_currentGraphics.bloomMode;
            motionBlur.value = (int)_currentGraphics.motionBlurMode;
            // filmGrain.value = (int)CurrentGraphics.filmGrain;
            shadowQuality.value = (int)_currentGraphics.shadowQuality;

            
        }

        public void SetQuality(int qualityIndex)
        {
            QualitySettings.SetQualityLevel(qualityIndex);
            _currentGraphics = graphicsList[qualityIndex];
        }
    }
}

