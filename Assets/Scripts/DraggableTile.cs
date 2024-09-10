using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableTile : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector3 startPosition;
    private CanvasGroup canvasGroup;
    public TileSpawner tileSpawner;  // TileSpawner�ւ̎Q��
    private bool isDropped = false;  // �^�C�������Ƀh���b�v����Ă��邩�ǂ����̃t���O

    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        startPosition = transform.position;  // �h���b�O�J�n���̈ʒu��ۑ�
        canvasGroup.blocksRaycasts = false;  // �h���b�O����Raycast�𖳌���
    }

    public void OnDrag(PointerEventData eventData)
    {
        // �}�E�X�̈ʒu�����[���h���W�ɕϊ�
        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z));
        // �^�C�����}�E�X�ɒǏ]������
        transform.position = new Vector3(worldMousePos.x, worldMousePos.y, transform.position.z);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;  // �h���b�O�I������Raycast���ėL����

        GameObject dropTarget = eventData.pointerEnter;  // �h���b�v��̃I�u�W�F�N�g���擾
        if (dropTarget != null && dropTarget.GetComponent<CustomDropTarget>() != null)
        {
            // �h���b�v��Ƀ^�C�����ړ�
            Vector3 worldPosition = dropTarget.transform.position;
            transform.position = worldPosition;

            // �^�C���̐e�I�u�W�F�N�g��Grid Generator�ɕύX
            GameObject gridGenerator = GameObject.Find("Grid Generator");
            transform.SetParent(gridGenerator.transform, false);

            // �܂��h���b�v����Ă��Ȃ�������^�C���J�E���g�����炷
            if (!isDropped && tileSpawner != null)
            {
                tileSpawner.ReduceTileCount();
                isDropped = true;  // ��x�h���b�v���ꂽ���Ƃ��L�^
            }
        }
        else
        {
            // �����ȃh���b�v��̏ꍇ�͌��̈ʒu�ɖ߂�
            transform.position = startPosition;
        }
    }
}
