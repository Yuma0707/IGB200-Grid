using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableTile : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Vector3 startPosition;
    public Transform startParent;
    private CanvasGroup canvasGroup;
    public TileSpawner tileSpawner;  // Tile Sponer Reference
    public bool isDropped = false;  // Dropped or not

    // Properties of startParent
    public Transform StartParent
    {
        get { return startParent; }
        set { startParent = value; }
    }

    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        startParent = transform.parent; // Save initial parent object
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        startPosition = transform.position; // Save the position at the start of the drag
        StartParent = transform.parent; // Save the parent at the start of the drag
        canvasGroup.blocksRaycasts = false; // Disable Raycast while dragging
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Converts mouse position to world coordinates
        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
        // Make tiles follow the mouse position
        transform.position = new Vector3(worldMousePos.x, worldMousePos.y, transform.position.z);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true; // Return Raycast to active at the end of a drag

        GameObject dropTarget = eventData.pointerEnter; // Obtain the object to drop to

        if (dropTarget != null && dropTarget.GetComponent<CustomDropTarget>() != null)
        {
            CustomDropTarget dropTargetComponent = dropTarget.GetComponent<CustomDropTarget>();
            dropTargetComponent.HandleDrop(this);
        }
        else
        {
            // Restore original position if invalid drop destination
            transform.position = startPosition;
            transform.SetParent(StartParent, false);
            transform.localPosition = Vector3.zero; // Centered on the original panel
        }
    }
}