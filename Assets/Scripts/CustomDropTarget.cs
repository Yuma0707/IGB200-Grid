using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CustomDropTarget : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("Custom OnDrop triggered");
        GameObject droppedObject = eventData.pointerDrag;
        if (droppedObject != null)
        {
            Debug.Log("Dropped object: " + droppedObject.name);
            // droppedObject.transform.SetParent(this.transform);

            RectTransform droppedRect = droppedObject.GetComponent<RectTransform>();
            RectTransform targetRect = this.GetComponent<RectTransform>();

            // Set anchor point and pivot to center.アンカーポイントとピボットを中央に設定
            droppedRect.anchorMin = new Vector2(0.5f, 0.5f);
            droppedRect.anchorMax = new Vector2(0.5f, 0.5f);
            droppedRect.pivot = new Vector2(0.5f, 0.5f);

            // Reset tile size and scale.タイルのサイズとスケールをリセット
            droppedRect.sizeDelta = targetRect.sizeDelta;
            droppedRect.localScale = Vector3.one;

            // Center the tile in the center of the cell.タイルをセルの中央に配置する
            droppedRect.anchoredPosition = Vector2.zero;

            // Debug log added.デバッグログ追加
            Debug.Log("Final Rect Size: " + droppedRect.sizeDelta);
        }
    }
}


