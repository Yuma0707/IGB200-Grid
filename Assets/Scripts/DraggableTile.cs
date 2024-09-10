using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableTile : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector3 startPosition;
    private CanvasGroup canvasGroup;
    public TileSpawner tileSpawner;  // Reference to TileSpawner.TileSpawnerへの参照
    private bool isDropped = false;  // Flag if a tile has already been dropped or not.タイルが既にドロップされているかどうかのフラグ

    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        startPosition = transform.position;  // Save the position at the start of the drag.ドラッグ開始時の位置を保存
        canvasGroup.blocksRaycasts = false;  // Disable Raycast while dragging.ドラッグ中にRaycastを無効化
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Converts mouse position to world coordinates.マウスの位置をワールド座標に変換
        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z));
        // Make tiles follow the mouse.タイルをマウスに追従させる
        transform.position = new Vector3(worldMousePos.x, worldMousePos.y, transform.position.z);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;  // Re-enable Raycast at end of drag.ドラッグ終了時にRaycastを再有効化

        GameObject dropTarget = eventData.pointerEnter;  // Obtain the object to drop to.ドロップ先のオブジェクトを取得
        if (dropTarget != null && dropTarget.GetComponent<CustomDropTarget>() != null)
        {
            // Move tile to drop destination.ドロップ先にタイルを移動
            Vector3 worldPosition = dropTarget.transform.position;
            transform.position = worldPosition;

            // Change parent object of tile to Grid Generator.タイルの親オブジェクトをGrid Generatorに変更
            GameObject gridGenerator = GameObject.Find("Grid Generator");
            transform.SetParent(gridGenerator.transform, false);

            // Reduce tile count if not already dropped..まだドロップされていなかったらタイルカウントを減らす
            if (!isDropped && tileSpawner != null)
            {
                tileSpawner.ReduceTileCount();
                isDropped = true;  // Record once dropped.一度ドロップされたことを記録
            }
        }
        else
        {
            // Restore original position if invalid drop destination.無効なドロップ先の場合は元の位置に戻す
            transform.position = startPosition;
        }
    }
}
