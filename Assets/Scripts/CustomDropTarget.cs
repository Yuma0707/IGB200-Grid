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
            DraggableTile draggableTile = droppedObject.GetComponent<DraggableTile>();
            HandleDrop(draggableTile);
        }
    }

    public void HandleDrop(DraggableTile draggableTile)
    {
        Transform existingTile = transform.childCount > 0 ? transform.GetChild(0) : null;

        // 既存のタイルがある場合は入れ替える
        if (existingTile != null)
        {
            // 既存のタイルをドラッグされたタイルの元の親に移動
            Transform originalParent = draggableTile.StartParent;
            existingTile.SetParent(originalParent, false);
            existingTile.localPosition = Vector3.zero; // 元の位置に配置
            AdjustSize(existingTile); // サイズを元のパネルに調整
        }

        // ドロップされたタイルを新しいパネルに配置
        draggableTile.transform.SetParent(transform, false);
        draggableTile.transform.localPosition = Vector3.zero; // ドロップ先の中央に配置
        AdjustSize(draggableTile.transform); // ドロップ先のサイズに合わせて調整

        // ドロップカウント処理
        if (!draggableTile.isDropped && draggableTile.tileSpawner != null)
        {
            draggableTile.tileSpawner.ReduceTileCount();
            draggableTile.isDropped = true; // ドロップ済みであることを記録
        }

        // ドロップ後の親を更新
        draggableTile.StartParent = transform;
    }

    // タイルのサイズを新しいパネルのサイズに合わせるメソッド
    private void AdjustSize(Transform tile)
    {
        RectTransform tileRect = tile.GetComponent<RectTransform>();
        RectTransform parentRect = tile.parent.GetComponent<RectTransform>();

        if (tileRect != null && parentRect != null)
        {
            tileRect.sizeDelta = parentRect.sizeDelta;
        }
    }
}
