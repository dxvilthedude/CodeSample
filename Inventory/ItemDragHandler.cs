using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CanvasGroup))]
public class ItemDragHandler : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] protected ItemSlotUI itemSlotUI = null;
    [SerializeField] protected HotbarItemEvent onMouseStartHoverItem = null;
    [SerializeField] protected VoidEvent onMouseEndHoverItem = null;

    private CanvasGroup canvasGroup = null;
    private Transform originalParent = null;
    private bool isHovering = false;
    private int canvasOriginalOrder = 200;

    public ItemSlotUI ItemSlotUI => itemSlotUI;

    private void Start() => canvasGroup = GetComponent<CanvasGroup>();

    private void OnDisable()
    {
        if (isHovering)
        {
            onMouseEndHoverItem.Raise();
            isHovering = false;
        }
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            onMouseEndHoverItem.Raise();

            originalParent = transform.parent;

            transform.SetParent(transform.parent.parent);

            canvasGroup.blocksRaycasts = false;
            GetComponentInParent<Canvas>().sortingOrder = 500;
        }
    }

    public virtual void OnDrag(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            transform.position = Input.mousePosition;
        }
    }

    public virtual void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            transform.SetParent(originalParent);
            transform.localPosition = Vector3.zero;
            canvasGroup.blocksRaycasts = true;
            GetComponentInParent<Canvas>().sortingOrder = canvasOriginalOrder;

        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        onMouseStartHoverItem.Raise(ItemSlotUI.SlotItem);
        isHovering = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        onMouseEndHoverItem.Raise();
        isHovering = false;
    }
}

