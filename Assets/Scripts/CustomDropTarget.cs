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

            // Disable swapping if tiles generated from the sponer have not yet been placed on the panel
            if (draggableTile.tileSpawner != null && !draggableTile.isDropped)
            {
                // Restore a dragged tile to its original position
                draggableTile.transform.position = draggableTile.startPosition;
                draggableTile.transform.SetParent(draggableTile.StartParent, false);
                draggableTile.transform.localPosition = Vector3.zero;
                return; // End of process
            }

            HandleDrop(draggableTile);
        }
    }

    public void HandleDrop(DraggableTile draggableTile)
    {
        Transform existingTile = transform.childCount > 0 ? transform.GetChild(0) : null;

        // Replace existing tiles if any.
        if (existingTile != null)
        {
            // Move an existing tile to the original parent of the dragged tile
            Transform originalParent = draggableTile.StartParent;
            existingTile.SetParent(originalParent, false);
            existingTile.localPosition = Vector3.zero; // Place in original position
            AdjustSize(existingTile); // Adjust size to original panel
        }

        // Place dropped tiles on new panel
        draggableTile.transform.SetParent(transform, false);
        draggableTile.transform.localPosition = Vector3.zero; // Centered on drop destination
        AdjustSize(draggableTile.transform); // Adjusts to the size of the drop destination

        // Drop count processing
        if (!draggableTile.isDropped && draggableTile.tileSpawner != null)
        {
            draggableTile.tileSpawner.ReduceTileCount();
            draggableTile.isDropped = true; // Recorded as dropped
        }

        // Update parent after drop
        draggableTile.StartParent = transform;
    }

    // Method to match tile size to new panel size
    public void AdjustSize(Transform tile)
    {
        RectTransform tileRect = tile.GetComponent<RectTransform>();
        RectTransform parentRect = tile.parent.GetComponent<RectTransform>();

        if (tileRect != null && parentRect != null)
        {
            tileRect.sizeDelta = parentRect.sizeDelta;
        }
    }
}
