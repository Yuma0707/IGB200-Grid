using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableTile : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Vector3 startPosition;
    public CanvasGroup canvasGroup;
    public TileSpawner tileSpawner;  // Reference to TileSpawner
    public bool isDropped = false;  // Flag to check if the tile has already been dropped or not.

    public AudioClip pickUpSound;   // pickUpSound
    public AudioClip putDownSound;  // putDownSound
    public AudioSource audioSource;  // Audio source components

    public void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        audioSource = GetComponent<AudioSource>();  // Get AudioSource component
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();  // If it does not exist, add the AudioSource component
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        startPosition = transform.position;  // Saves the position at the start of the drag
        canvasGroup.blocksRaycasts = false;  // Disable Raycast during dragging 

        // Play pickup sound effects 
        if (pickUpSound != null)
        {
            audioSource.clip = pickUpSound;
            audioSource.Play();
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Convert mouse position to world coordinates 
        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z));
        // Make objects follow the mouse
        transform.position = new Vector3(worldMousePos.x, worldMousePos.y, transform.position.z);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;  // Re-enable Raycast at end of drag

        GameObject dropTarget = eventData.pointerEnter;  // Get drag-and-drop target object
        if (dropTarget != null && dropTarget.GetComponent<CustomDropTarget>() != null)
        {
            // Move the object to the placement destination
            Vector3 worldPosition = dropTarget.transform.position;
            transform.position = worldPosition;

            // Change the object's parent to Grid Generator
            GameObject gridGenerator = GameObject.Find("Grid Generator");
            transform.SetParent(gridGenerator.transform, false);

            // Reduce the number of tiles if objects have not yet been placed
            if (!isDropped && tileSpawner != null)
            {
                tileSpawner.ReduceTileCount();
                isDropped = true;  // Records have been placed
            }

            // Play Drop Sound
            if (putDownSound != null)
            {
                audioSource.clip = putDownSound;
                audioSource.Play();
            }
        }
        else
        {
            // Restore original position if placement is not valid
            transform.position = startPosition;
        }
    }
}
