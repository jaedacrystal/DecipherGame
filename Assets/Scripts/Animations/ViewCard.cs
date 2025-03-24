using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ViewCard : MonoBehaviour
{
    public float scale;
    public float transitionSpeed;
    public float initialScale;
    private int originalSiblingIndex;

    private RectTransform transformObject;
    [HideInInspector] public Vector3 originalLocalPosition;

    private bool isClicked;

    private void Start()
    {
        transformObject = GetComponent<RectTransform>();
        originalSiblingIndex = transform.GetSiblingIndex();
    }

    public void cardClicked()
    {
        if (isClicked)
        {
            transform.DOLocalMove(originalLocalPosition, transitionSpeed);
            transform.DOScale(initialScale, transitionSpeed);

            transform.SetSiblingIndex(originalSiblingIndex);
            isClicked = false;
        }
        else
        {
            originalLocalPosition = transformObject.localPosition;

            Vector3 targetPosition = new Vector3(0, 600f, 0);
            transform.DOScale(scale, transitionSpeed);
            transform.DOLocalMove(targetPosition, transitionSpeed);

            transform.SetAsLastSibling();
            isClicked = true;
        }

    }
}
