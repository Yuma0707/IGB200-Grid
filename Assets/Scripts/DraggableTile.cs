using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableTile : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector3 startPosition;
    private CanvasGroup canvasGroup;

    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        startPosition = transform.position;
        canvasGroup.blocksRaycasts = false; // Disable Raycast while dragging.�h���b�O����Raycast�𖳌��ɂ���
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition; // Follows the mouse cursor.�}�E�X�J�[�\���ɒǏ]����
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true; // Re-enable Raycast after drag ends.�h���b�O�I����� Raycast ���ėL����

        GameObject dropTarget = eventData.pointerEnter; // Obtain drop target.�h���b�v�Ώۂ��擾
        if (dropTarget != null && dropTarget.GetComponent<CustomDropTarget>() != null)
        {
            // Move a tile to the world coordinates of a cell.�^�C�����Z���̃��[���h���W�Ɉړ�����
            Vector3 worldPosition = dropTarget.transform.position;
            transform.position = worldPosition;

            // If necessary, cell positions can be saved for later use.�K�v�ɉ����āA��Ŏg�����߂ɃZ���̈ʒu��ۑ����邱�Ƃ��\
        }
        else
        {
            // Restore original position if not dropped on a valid target.�L���ȃ^�[�Q�b�g�Ƀh���b�v����Ă��Ȃ��ꍇ�A���̈ʒu�ɖ߂�
            transform.position = startPosition;
        }
    }

}
