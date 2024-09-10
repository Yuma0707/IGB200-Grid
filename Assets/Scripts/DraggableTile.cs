using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableTile : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector3 startPosition;
    private CanvasGroup canvasGroup;
    public TileSpawner tileSpawner;  // Reference to TileSpawner.TileSpawner�ւ̎Q��
    private bool isDropped = false;  // Flag if a tile has already been dropped or not.�^�C�������Ƀh���b�v����Ă��邩�ǂ����̃t���O

    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        startPosition = transform.position;  // Save the position at the start of the drag.�h���b�O�J�n���̈ʒu��ۑ�
        canvasGroup.blocksRaycasts = false;  // Disable Raycast while dragging.�h���b�O����Raycast�𖳌���
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Converts mouse position to world coordinates.�}�E�X�̈ʒu�����[���h���W�ɕϊ�
        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z));
        // Make tiles follow the mouse.�^�C�����}�E�X�ɒǏ]������
        transform.position = new Vector3(worldMousePos.x, worldMousePos.y, transform.position.z);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;  // Re-enable Raycast at end of drag.�h���b�O�I������Raycast���ėL����

        GameObject dropTarget = eventData.pointerEnter;  // Obtain the object to drop to.�h���b�v��̃I�u�W�F�N�g���擾
        if (dropTarget != null && dropTarget.GetComponent<CustomDropTarget>() != null)
        {
            // Move tile to drop destination.�h���b�v��Ƀ^�C�����ړ�
            Vector3 worldPosition = dropTarget.transform.position;
            transform.position = worldPosition;

            // Change parent object of tile to Grid Generator.�^�C���̐e�I�u�W�F�N�g��Grid Generator�ɕύX
            GameObject gridGenerator = GameObject.Find("Grid Generator");
            transform.SetParent(gridGenerator.transform, false);

            // Reduce tile count if not already dropped..�܂��h���b�v����Ă��Ȃ�������^�C���J�E���g�����炷
            if (!isDropped && tileSpawner != null)
            {
                tileSpawner.ReduceTileCount();
                isDropped = true;  // Record once dropped.��x�h���b�v���ꂽ���Ƃ��L�^
            }
        }
        else
        {
            // Restore original position if invalid drop destination.�����ȃh���b�v��̏ꍇ�͌��̈ʒu�ɖ߂�
            transform.position = startPosition;
        }
    }

}
