using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class DamageIndicator : MonoBehaviour 
{
    public Image Image;
    public float FlashSpeed;

    private Coroutine _fadeAwayCor;

    public void Flash()
    {
        if(_fadeAwayCor != null)
        {
            StopCoroutine(_fadeAwayCor); 
        }

        Image.enabled = true;
        Image.color = Color.white;
        _fadeAwayCor = StartCoroutine(FadeAway());
    }

    IEnumerator FadeAway()
    {
        float a = 1f;
        while(a > 0.0f)
        {
            a -= (1.0f / FlashSpeed) * Time.deltaTime;
            Image.color = new Color(1.0f, 1.0f, 1.0f, a);
            yield return null;
        }

        Image.enabled = false;
    }
}