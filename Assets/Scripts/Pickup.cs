using System.Collections;
using System.Collections.Generic;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using TMPro;

public class Pickup : MonoBehaviour
{
    public StringEvent PickupEvent;
    public StringEvent DropEvent;
    public StringEvent InteractEvent;

    public FloatReference PickupDistance;
    public GameObject PickupCanvas;
    public GameObject DropCanvas;
    public GameObject InteractCanvas;

    public StringValueList Inventory;

    private TextMeshProUGUI pickupCanvasText;
    private TextMeshProUGUI dropCanvasText;
    private TextMeshProUGUI interactCanvasText;
    private Pickupable holdingItem;

    // Start is called before the first frame update
    void Start()
    {
        pickupCanvasText = PickupCanvas.GetComponentInChildren<TextMeshProUGUI>();
        dropCanvasText = DropCanvas.GetComponentInChildren<TextMeshProUGUI>();
        interactCanvasText = InteractCanvas.GetComponentInChildren<TextMeshProUGUI>();

        DropCanvas.SetActive(false);
        PickupCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        CheckForInteractable();
        CheckForPickup();
        CheckForDrop();
    }

    private void CheckForInteractable() {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, PickupDistance)) {
            if (hit.collider.CompareTag("Interactable")) {
                if (Input.GetMouseButtonDown(0)) {
                    Interactable i = hit.collider.GetComponent<Interactable>();
                    if(holdingItem == null) {
                        if (i.Interact(null)) {
                            InteractEvent.Raise(i.Name.Value);
                        }
                    }
                    else {
                        if(i.Interact(holdingItem.InventoryObject.Value)) {
                            InteractEvent.Raise(i.Name.Value);
                        }
                    }
                }

                if (!InteractCanvas.activeSelf) {
                    interactCanvasText.text = "Interact with " + hit.collider.name;
                    InteractCanvas.SetActive(true);
                }
                return;
            }
        }

        if (InteractCanvas.activeSelf) {
            InteractCanvas.SetActive(false);
        }
    }

    private void CheckForDrop() {
        if (holdingItem == null)
            return;

        if (!Input.GetMouseButtonDown(1))
            return;

        holdingItem.transform.parent = null;
        holdingItem.Dropped();
        DropEvent.Raise(holdingItem.InventoryObject.Value);

        DropCanvas.SetActive(false);
        holdingItem = null;
    }

    private void CheckForPickup() {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, PickupDistance)) {
            if (hit.collider.CompareTag("Pickupable")) {
                if (Input.GetMouseButtonDown(0)) {
                    if(holdingItem != null) {
                        holdingItem.transform.parent = null;
                        holdingItem.Dropped();
                        DropEvent.Raise(holdingItem.InventoryObject.Value);
                    }
                    Pickupable p = hit.collider.GetComponent<Pickupable>();
                    p.PickedUp();
                    holdingItem = p;
                    p.transform.parent = Camera.main.transform;
                    p.transform.localPosition = new Vector3(0.3f, -0.2f, 0.4f);
                    p.transform.localRotation = Quaternion.identity;
                    PickupEvent.Raise(p.InventoryObject.Value);
                    DropCanvas.SetActive(true);
                    dropCanvasText.text = "Drop " + hit.collider.name;
                }

                if (!PickupCanvas.activeSelf) {
                    pickupCanvasText.text = "Pick up " + hit.collider.name;
                    PickupCanvas.SetActive(true);
                }
                return;
            }
        }

        if(PickupCanvas.activeSelf) {
            PickupCanvas.SetActive(false);
        }
    }

    private void OnDestroy() {
        Inventory.Clear();
    }
}
