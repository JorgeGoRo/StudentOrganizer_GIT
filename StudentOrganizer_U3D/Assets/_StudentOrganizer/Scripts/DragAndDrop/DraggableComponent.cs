using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableComponent : MonoBehaviour, IInitializePotentialDragHandler, IBeginDragHandler, IDragHandler, IEndDragHandler {
    [FoldoutGroup("Dynamic-Debug")] public event Action<PointerEventData> OnBeginDragHandler;
    [FoldoutGroup("Dynamic-Debug")] public event Action<PointerEventData> OnDragHandler;
    [FoldoutGroup("Dynamic-Debug")] public event Action<PointerEventData, bool> OnEndDragHandler;
    [FoldoutGroup("Dynamic-Debug")] public bool FollorCursor { get; set; } = true;
    [FoldoutGroup("Dynamic-Debug")] public bool CanDrag { get; set; } = true;
    [FoldoutGroup("Dynamic-Debug")] public Vector3 startPosition;
    [FoldoutGroup("Dynamic-Debug")] public Transform startParent;

    [FoldoutGroup("Dynamic-Debug")] [SerializeField]
    protected RectTransform rectTransform;

    [FoldoutGroup("Dynamic-Debug")] [SerializeField]
    protected Canvas canvas;

    protected virtual void Awake() {
        rectTransform = GetComponent<RectTransform>();
        canvas = transform.root.GetComponent<Canvas>();
    }

    public void OnBeginDrag(PointerEventData eventData) {
        if (!CanDrag) {
            return;
        }

        transform.SetParent(canvas.transform);
        OnBeginDragHandler?.Invoke(eventData);
    }

    public void OnDrag(PointerEventData eventData) {
        if (!CanDrag) {
            return;
        }

        OnDragHandler?.Invoke(eventData);
        if (FollorCursor) {
            rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        }
    }

    public void OnEndDrag(PointerEventData eventData) {
        if (!CanDrag) {
            return;
        }

        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        DropArea dropArea = null;
        foreach (var result in results) {
            dropArea = result.gameObject.GetComponent<DropArea>();
            if (dropArea != null) {
                break;
            }
        }

        if (dropArea != null) {
            if (dropArea.Accepts(this)) {
                dropArea.Drop(this);
                OnEndDragHandler?.Invoke(eventData, true);
                return;
            }
        }

        transform.SetParent(startParent);
        rectTransform.anchoredPosition = startPosition;
        OnEndDragHandler?.Invoke(eventData, false);
    }

    public void OnInitializePotentialDrag(PointerEventData eventData) {
        startPosition = rectTransform.anchoredPosition;
        startParent = transform.parent;
    }
}