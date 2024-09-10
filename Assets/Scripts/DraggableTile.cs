using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableTile : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector3 startPosition;
    private CanvasGroup canvasGroup;
    public TileSpawner tileSpawner;  // TileSpawnerへの参照
    private bool isDropped = false;  // タイルが既にドロップされているかどうかのフラグ

    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        startPosition = transform.position;  // ドラッグ開始時の位置を保存
        canvasGroup.blocksRaycasts = false;  // ドラッグ中にRaycastを無効化
    }

    public void OnDrag(PointerEventData eventData)
    {
        // マウスの位置をワールド座標に変換
        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z));
        // タイルをマウスに追従させる
        transform.position = new Vector3(worldMousePos.x, worldMousePos.y, transform.position.z);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;  // ドラッグ終了時にRaycastを再有効化

        GameObject dropTarget = eventData.pointerEnter;  // ドロップ先のオブジェクトを取得
        if (dropTarget != null && dropTarget.GetComponent<CustomDropTarget>() != null)
        {
            // ドロップ先にタイルを移動
            Vector3 worldPosition = dropTarget.transform.position;
            transform.position = worldPosition;

            // タイルの親オブジェクトをGrid Generatorに変更
            GameObject gridGenerator = GameObject.Find("Grid Generator");
            transform.SetParent(gridGenerator.transform, false);

            // まだドロップされていなかったらタイルカウントを減らす
            if (!isDropped && tileSpawner != null)
            {
                tileSpawner.ReduceTileCount();
                isDropped = true;  // 一度ドロップされたことを記録
            }
        }
        else
        {
            // 無効なドロップ先の場合は元の位置に戻す
            transform.position = startPosition;
        }
    }
}
