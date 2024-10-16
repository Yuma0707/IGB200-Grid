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
            // タイルをセルの位置に移動
            droppedObject.transform.position = transform.position;

            // タイルの親オブジェクトをGrid Generatorに設定
            GameObject gridGenerator = GameObject.Find("Grid Generator");
            droppedObject.transform.SetParent(gridGenerator.transform, false);

            // タイルのサイズをセルのサイズに合わせる
            RectTransform droppedRect = droppedObject.GetComponent<RectTransform>();
            RectTransform targetRect = GetComponent<RectTransform>();
            droppedRect.sizeDelta = targetRect.sizeDelta;
        }
    }

}
