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
            droppedObject.transform.SetParent(this.transform);

            // Match tile size to cell size.セルのサイズにタイルのサイズを合わせる
            RectTransform droppedRect = droppedObject.GetComponent<RectTransform>();
            RectTransform targetRect = this.GetComponent<RectTransform>();

            // Set anchor point and pivot to center.アンカーポイントとピボットを中央に設定
            droppedRect.anchorMin = new Vector2(0.5f, 0.5f);
            droppedRect.anchorMax = new Vector2(0.5f, 0.5f);
            droppedRect.pivot = new Vector2(0.5f, 0.5f);

            // Match tile size to cell size.タイルのサイズをセルのサイズに合わせる
            droppedRect.sizeDelta = targetRect.sizeDelta;

            // Center the tile in the center of the cell.タイルをセルの中央に配置する
            droppedRect.anchoredPosition = Vector2.zero;
        }
    }

}

