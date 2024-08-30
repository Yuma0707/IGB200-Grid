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
        this.transform.SetParent(this.transform.root); // Move tile to the front of the line.�^�C�����őO�ʂɈړ�

        // Remove tile from Raycast target while dragging.�^�C�����h���b�O����Raycast�Ώۂ���O��
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        this.transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Return tile to Raycast target.�^�C����Raycast�Ώۂɖ߂�
        GetComponent<CanvasGroup>().blocksRaycasts = true;

        RaycastResult raycastResult = eventData.pointerCurrentRaycast;
        GameObject dropTarget = raycastResult.gameObject;

        Debug.Log("Raycast hit: " + raycastResult.gameObject.name);

        if (dropTarget != null && dropTarget.GetComponent<CustomDropTarget>() != null)
        {
            Debug.Log("Drop target found: " + dropTarget.name);
            this.transform.SetParent(dropTarget.transform);
            this.transform.localPosition = Vector3.zero;  // Placed in the center of the cell.�Z���̒��S�ɔz�u
        }
        else
        {
            Debug.Log("No valid drop target, returning to start position.");
            this.transform.SetParent(parentToReturnTo);
            this.transform.position = startPosition; // return something (that has been moved) to its original position.���̈ʒu�ɖ߂�
        }
    }
}
