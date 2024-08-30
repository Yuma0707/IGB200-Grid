using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableTile : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector3 startPosition;
    private Transform parentToReturnTo = null;

    public void OnBeginDrag(PointerEventData eventData)
    {
        startPosition = this.transform.position;
        parentToReturnTo = this.transform.parent;
        this.transform.SetParent(this.transform.root); // Move tile to the front of the line.タイルを最前面に移動

        // Remove tile from Raycast target while dragging.タイルをドラッグ中はRaycast対象から外す
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        this.transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Return tile to Raycast target.タイルをRaycast対象に戻す
        GetComponent<CanvasGroup>().blocksRaycasts = true;

        RaycastResult raycastResult = eventData.pointerCurrentRaycast;
        GameObject dropTarget = raycastResult.gameObject;

        Debug.Log("Raycast hit: " + raycastResult.gameObject.name);

        if (dropTarget != null && dropTarget.GetComponent<CustomDropTarget>() != null)
        {
            Debug.Log("Drop target found: " + dropTarget.name);
            this.transform.SetParent(dropTarget.transform);
            this.transform.localPosition = Vector3.zero;  // Placed in the center of the cell.セルの中心に配置
        }
        else
        {
            Debug.Log("No valid drop target, returning to start position.");
            this.transform.SetParent(parentToReturnTo);
            this.transform.position = startPosition; // return something (that has been moved) to its original position.元の位置に戻す
        }
    }
}
