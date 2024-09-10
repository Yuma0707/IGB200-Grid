using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CustomDropTarget : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        GameObject droppedObject = eventData.pointerDrag;
        if (droppedObject != null && droppedObject.GetComponent<DraggableTile>() != null)
        {
            // Move tile to this drop target location.�^�C�������̃h���b�v�^�[�Q�b�g�̈ʒu�Ɉړ�
            droppedObject.transform.position = transform.position;

            // Set parent object to Grid Generator.�e�I�u�W�F�N�g�� Grid Generator �ɐݒ�
            GameObject gridGenerator = GameObject.Find("Grid Generator");
            droppedObject.transform.SetParent(gridGenerator.transform, false);
        }
    }
}
