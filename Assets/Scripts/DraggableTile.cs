using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableTile : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector3 startPosition;
    private Transform startParent;
    private CanvasGroup canvasGroup;
    public TileSpawner tileSpawner;  // タイルスポナーの参照
    public bool isDropped = false;  // ドロップ済みかどうか

    // startParentのプロパティ
    public Transform StartParent
    {
        get { return startParent; }
        set { startParent = value; }
    }

    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        startParent = transform.parent; // 初期の親オブジェクトを保存
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        startPosition = transform.position; // ドラッグ開始時の位置を保存
        StartParent = transform.parent; // ドラッグ開始時の親を保存
        canvasGroup.blocksRaycasts = false; // ドラッグ中はRaycastを無効にする
    }

    public void OnDrag(PointerEventData eventData)
    {
        // マウスの位置をワールド座標に変換
        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
        // タイルをマウスの位置に追従させる
        transform.position = new Vector3(worldMousePos.x, worldMousePos.y, transform.position.z);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true; // ドラッグ終了時にRaycastを有効に戻す

        GameObject dropTarget = eventData.pointerEnter; // ドロップ先のオブジェクトを取得

        if (dropTarget != null && dropTarget.GetComponent<CustomDropTarget>() != null)
        {
            CustomDropTarget dropTargetComponent = dropTarget.GetComponent<CustomDropTarget>();
            dropTargetComponent.HandleDrop(this);
        }
        else
        {
            // 無効なドロップ先の場合は元の位置に戻す
            transform.position = startPosition;
            transform.SetParent(StartParent, false);
            transform.localPosition = Vector3.zero; // 元のパネルの中央に配置
        }
    }
}
