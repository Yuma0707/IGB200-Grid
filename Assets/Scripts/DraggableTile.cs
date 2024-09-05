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
        canvasGroup.blocksRaycasts = false; // Disable Raycast while dragging.ドラッグ中はRaycastを無効にする
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition; // Follows the mouse cursor.マウスカーソルに追従する
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true; // Re-enable Raycast after drag ends.ドラッグ終了後に Raycast を再有効化

        GameObject dropTarget = eventData.pointerEnter; // Obtain drop target.ドロップ対象を取得
        if (dropTarget != null && dropTarget.GetComponent<CustomDropTarget>() != null)
        {
            // Move a tile to the world coordinates of a cell.タイルをセルのワールド座標に移動する
            Vector3 worldPosition = dropTarget.transform.position;
            transform.position = worldPosition;

            // If necessary, cell positions can be saved for later use.必要に応じて、後で使うためにセルの位置を保存することも可能
        }
        else
        {
            // Restore original position if not dropped on a valid target.有効なターゲットにドロップされていない場合、元の位置に戻す
            transform.position = startPosition;
        }
    }

}
