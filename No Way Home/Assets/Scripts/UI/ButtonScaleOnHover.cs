using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonScaleOnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Vector3 _hoverScale = new Vector3(1.1f, 1.1f, 1.1f);
    [SerializeField] private float _duration = 0.2f;

    private Vector3 _originalScale;
    private Button _button;


    private void Awake() 
    {
        _originalScale = transform.localScale;
        _button = GetComponent<Button>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(_button.interactable == false) return;
        transform.DOScale(_hoverScale, _duration);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.DOScale(_originalScale, _duration);
    }

}