using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropTarget : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("Custom OnDrop triggered");
        GameObject droppedObject = eventData.pointerDrag;
        if (droppedObject != null)
        {
            Debug.Log("Dropped object: " + droppedObject.name);
            droppedObject.transform.SetParent(this.transform);

            // Match tile size to cell size.セルのサイズにタイルのサイズを合わせる
            RectTransform droppedRect = droppedObject.GetComponent<RectTransform>();
            RectTransform targetRect = this.GetComponent<RectTransform>();
            droppedRect.sizeDelta = targetRect.sizeDelta;

            droppedObject.transform.localPosition = Vector3.zero;  // Placed in the center of the cell.セルの中心に配置
        }
    }

}
