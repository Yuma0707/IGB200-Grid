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
            // Move tile to this drop target location.タイルをこのドロップターゲットの位置に移動
            droppedObject.transform.position = transform.position;

            // Set parent object to Grid Generator.親オブジェクトを Grid Generator に設定
            GameObject gridGenerator = GameObject.Find("Grid Generator");
            droppedObject.transform.SetParent(gridGenerator.transform, false);
        }
    }
}
