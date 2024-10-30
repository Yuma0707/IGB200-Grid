using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableTile : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Vector3 startPosition;
    public Transform startParent;
    private CanvasGroup canvasGroup;
    public TileSpawner tileSpawner;  // Tile Spawner Reference
    public bool isDropped = false;  // Dropped or not

    // Audio properties
    public AudioClip pickUpSound;   // Sound effect for picking up
    public AudioClip putDownSound;  // Sound effect for putting down
    private AudioSource audioSource;  // AudioSource component

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

        // Get or add AudioSource component
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();  // Add AudioSource component if it does not exist
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        startPosition = transform.position; // Save the position at the start of the drag
        StartParent = transform.parent; // Save the parent at the start of the drag
        canvasGroup.blocksRaycasts = false; // Disable Raycast while dragging

        // Play pick-up sound
        if (pickUpSound != null)
        {
            audioSource.clip = pickUpSound;
            audioSource.Play();
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Convert mouse position to world coordinates
        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
        // Make tiles follow the mouse position
        transform.position = new Vector3(worldMousePos.x, worldMousePos.y, transform.position.z);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true; // Enable Raycast at the end of a drag

        GameObject dropTarget = eventData.pointerEnter; // Obtain the object to drop onto

        if (dropTarget != null && dropTarget.GetComponent<CustomDropTarget>() != null)
        {
            CustomDropTarget dropTargetComponent = dropTarget.GetComponent<CustomDropTarget>();
            dropTargetComponent.HandleDrop(this);

            // Play put-down sound
            if (putDownSound != null)
            {
                audioSource.clip = putDownSound;
                audioSource.Play();
            }
        }
        else
        {
            // Restore original position if the drop destination is invalid
            transform.position = startPosition;
            transform.SetParent(StartParent, false);
            transform.localPosition = Vector3.zero; // Center on the original panel
        }
    }
}
