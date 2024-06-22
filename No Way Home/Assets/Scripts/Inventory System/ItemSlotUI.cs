using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using SGS.Inventory;
using UnityEngine.EventSystems;
using SGS.InputSystem;

public class ItemSlotUI : MonoBehaviour , IPointerClickHandler
{
    public Button Button;
    public Image Icon;
    public TextMeshProUGUI QuantityText;
    [SerializeField] private ItemSlot _curItemSlot;
    private Outline _outline;

    public int Index;
    public bool Equipped;
    public bool ContainItem;

    private FrameInput _frameInput;

    private void Awake()
    {
        _outline = GetComponent<Outline>();
        if(_curItemSlot!= null)
        {
            Button.interactable = false;
            Button.onClick.AddListener(OnButtonClick);
        }

        
    }
  private void Start() 
   {
    InputsHandler.Instance.RightClickEvent += OnRightClickOnItem;
   }

    private void OnEnable()
    {
        _outline.enabled = Equipped;
        
    }


    public void Set(ItemSlot slot)
    {
        Button.interactable = true;
        _curItemSlot = slot;
        
        Icon.gameObject.SetActive(true);
        QuantityText.gameObject.SetActive(true);
        Icon.sprite = slot.ItemData.GetIcon();
        QuantityText.text = slot.Quantity > 1 ? slot.Quantity.ToString() : String.Empty;

        if (_outline != null)
        {
            _outline.enabled = Equipped;
        }
        
    }

    public void Clear()
    {
        _curItemSlot = null;
        
        Icon.gameObject.SetActive(false);
        QuantityText.text = string.Empty;
        QuantityText.gameObject.SetActive(false);
    }

    public void OnButtonClick()
    {
        Inventory.Instance.SelectItem(Index); 
    }

     public void SetSelected(bool isSelected)
    {
        if (_outline != null)
        {
            _outline.enabled = isSelected;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
            Vector2 mousepoiton = eventData.pressPosition;

        if (eventData.button == PointerEventData.InputButton.Right && Button.interactable)
        {
            Inventory.Instance.SelectItem(Index);
            if(_outline != null)
            {
                _outline.enabled = true;
                Debug.Log(eventData.pressPosition);
            }

            Inventory.Instance.ItemPopUpWindow(mousepoiton,true);

        } else if(eventData.button == PointerEventData.InputButton.Left && Button.interactable)
        {
            Inventory.Instance.ItemPopUpWindow(mousepoiton,false);
        }
            
        
    }
    private void OnRightClickOnItem()
    {
        
    }
}
