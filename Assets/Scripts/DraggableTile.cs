using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableTile : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector3 startPosition;
    private Transform parentToReturnTo = null;
    private Vector2 originalSize;

    public void Start()
    {
        // Save original size of tile.タイルの元のサイズを保存
        RectTransform rectTransform = GetComponent<RectTransform>();
        originalSize = rectTransform.sizeDelta;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        startPosition = this.transform.position;
        parentToReturnTo = this.transform.parent;
        this.transform.SetParent(this.transform.root); // Move tile to the front of the line.タイルを最前面に移動

        // Reset tile size to original size when drag starts.ドラッグが始まったときにタイルのサイズを元のサイズにリセット
        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.sizeDelta = originalSize;

        // Tile is removed from Raycast target while dragging.ドラッグ中はタイルをRaycast対象から外す
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        this.transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Return tile to Raycast target after end of drag.ドラッグ終了後にタイルをRaycast対象に戻す
        GetComponent<CanvasGroup>().blocksRaycasts = true;

        RaycastResult raycastResult = eventData.pointerCurrentRaycast;
        GameObject dropTarget = raycastResult.gameObject;

        Debug.Log("Raycast hit: " + raycastResult.gameObject.name);

        if (dropTarget != null && dropTarget.GetComponent<CustomDropTarget>() != null)
        {
            Debug.Log("Drop location found.: " + dropTarget.name);
            this.transform.SetParent(dropTarget.transform);
            this.transform.localPosition = Vector3.zero;  // Placed in the center of the cell.セルの中心に配置

            RectTransform droppedRect = this.GetComponent<RectTransform>();
            RectTransform targetRect = dropTarget.GetComponent<RectTransform>();

            // Adjust size to fit drop destination cell.ドロップ先のセルに合わせてサイズを調整
            droppedRect.sizeDelta = targetRect.sizeDelta;
        }
        else
        {
            Debug.Log("No valid drop destination found. Return to starting position.");
            this.transform.SetParent(parentToReturnTo);
            this.transform.position = startPosition; // return something (that has been moved) to its original position.元の位置に戻す

            // If there is no drop destination, restore the size.ドロップ先がない場合、サイズを元に戻す
            RectTransform droppedRect = this.GetComponent<RectTransform>();
            droppedRect.sizeDelta = originalSize;
        }
    }
}
