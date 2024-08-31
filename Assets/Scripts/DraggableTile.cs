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
        // Save original size of tile.�^�C���̌��̃T�C�Y��ۑ�
        RectTransform rectTransform = GetComponent<RectTransform>();
        originalSize = rectTransform.sizeDelta;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        startPosition = this.transform.position;
        parentToReturnTo = this.transform.parent;
        this.transform.SetParent(this.transform.root); // Move tile to the front of the line.�^�C�����őO�ʂɈړ�

        // Reset tile size to original size when drag starts.�h���b�O���n�܂����Ƃ��Ƀ^�C���̃T�C�Y�����̃T�C�Y�Ƀ��Z�b�g
        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.sizeDelta = originalSize;

        // Tile is removed from Raycast target while dragging.�h���b�O���̓^�C����Raycast�Ώۂ���O��
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        this.transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Return tile to Raycast target after end of drag.�h���b�O�I����Ƀ^�C����Raycast�Ώۂɖ߂�
        GetComponent<CanvasGroup>().blocksRaycasts = true;

        RaycastResult raycastResult = eventData.pointerCurrentRaycast;
        GameObject dropTarget = raycastResult.gameObject;

        Debug.Log("Raycast hit: " + raycastResult.gameObject.name);

        if (dropTarget != null && dropTarget.GetComponent<CustomDropTarget>() != null)
        {
            Debug.Log("Drop location found.: " + dropTarget.name);
            this.transform.SetParent(dropTarget.transform);
            this.transform.localPosition = Vector3.zero;  // Placed in the center of the cell.�Z���̒��S�ɔz�u

            RectTransform droppedRect = this.GetComponent<RectTransform>();
            RectTransform targetRect = dropTarget.GetComponent<RectTransform>();

            // Adjust size to fit drop destination cell.�h���b�v��̃Z���ɍ��킹�ăT�C�Y�𒲐�
            droppedRect.sizeDelta = targetRect.sizeDelta;
        }
        else
        {
            Debug.Log("No valid drop destination found. Return to starting position.");
            this.transform.SetParent(parentToReturnTo);
            this.transform.position = startPosition; // return something (that has been moved) to its original position.���̈ʒu�ɖ߂�

            // If there is no drop destination, restore the size.�h���b�v�悪�Ȃ��ꍇ�A�T�C�Y�����ɖ߂�
            RectTransform droppedRect = this.GetComponent<RectTransform>();
            droppedRect.sizeDelta = originalSize;
        }
    }
}
