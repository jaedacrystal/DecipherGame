using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

public class CardInteraction : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    public CanvasGroup canvasGroup;
    private Transform cardParent = null;
    private Vector3 originalPosition;
    public bool isDragging;

    [Header("Hover/Drag Settings")]
    [SerializeField] public float initialScale;
    [SerializeField] public float finalScale;
    [SerializeField] public float transitionSpeed;

    private RectTransform transformObject;
    private int originalSiblingIndex;
    private HorizontalLayoutGroup parentLayout;
    [HideInInspector] public Vector3 originalLocalPosition;

    private bool isClicked;

    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        transformObject = GetComponent<RectTransform>();
        originalSiblingIndex = transform.GetSiblingIndex();
        parentLayout = GetComponentInParent<HorizontalLayoutGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        isDragging = false;
        canvasGroup.blocksRaycasts = false;
        cardParent = this.transform.parent;
        originalPosition = this.transform.localPosition;
        this.transform.SetParent(this.transform.parent.parent);
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
        transform.SetAsLastSibling();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Discard discardCard = GetComponent<Discard>();
        canvasGroup.blocksRaycasts = true;

        if (isDragging)
        {
            discardCard.OnDrop(eventData);
        }
        else
        {
            ReturnToOriginalPosition();
        }
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private void ReturnToOriginalPosition()
    {
        Vector3 targetPosition = cardParent.localPosition;

        transform.DOLocalMove(targetPosition, 0.3f).OnComplete(() =>
        {
            this.transform.SetParent(cardParent);
        });
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (parentLayout) parentLayout.enabled = false;

        transformObject.DOScale(new Vector3(finalScale, finalScale, finalScale), transitionSpeed);
        transform.SetAsLastSibling();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isClicked)
        {
            transformObject.DOScale(new Vector3(initialScale, initialScale, initialScale), transitionSpeed);
            transform.SetSiblingIndex(originalSiblingIndex);

            if (parentLayout) parentLayout.enabled = true;
        }
    }

    public void cardClicked()
    {
        if (isClicked)
        {
            transform.DOLocalMove(originalLocalPosition, 0.25f);
            isClicked = false;
        }
        else
        {
            originalLocalPosition = transformObject.localPosition;

            Vector3 targetPosition = new Vector3(0, 500f, 0);
            transform.DOLocalMove(targetPosition, 0.25f);
            isClicked = true;
        }
    }
}