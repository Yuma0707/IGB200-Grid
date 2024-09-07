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
        startPosition = transform.position;  // Save the start position
        canvasGroup.blocksRaycasts = false; // Disable Raycast while dragging if needed
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Get the mouse position in world coordinates
        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z));

        // Keep the sprite at a fixed z-position (in front of the canvas)
        transform.position = new Vector3(worldMousePos.x, worldMousePos.y, transform.position.z);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true; // Re-enable Raycast

        GameObject dropTarget = eventData.pointerEnter;  // Obtain drop target
        if (dropTarget != null && dropTarget.GetComponent<CustomDropTarget>() != null)
        {
            // Move the tile to the drop target
            Vector3 worldPosition = dropTarget.transform.position;
            transform.position = worldPosition;

            // Change parent object to Grid Generator.親オブジェクトを Grid Generator に変更.
            GameObject gridGenerator = GameObject.Find("Grid Generator");
            transform.SetParent(gridGenerator.transform, false);
        }
        else
        {
            // Restore original position if not dropped on a valid target
            transform.position = startPosition;
        }
    }

}
