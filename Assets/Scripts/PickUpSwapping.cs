using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float pickupRange = 2f;
    public LayerMask pickupLayer;

    private GameObject currentObject; // Currently held object
    private GameObject[] weapons; // Array of weapons
    private int currentWeaponIndex = 0; // Index of currently equipped weapon

    void Update()
    {
        // Object pickup with 'E' key
        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, pickupRange, pickupLayer))
            {
                // Check if the object can be picked up
                if (hit.collider.CompareTag("Pickup"))
                {
                    PickUpObject(hit.collider.gameObject);
                }
            }
        }

        // Weapon swapping with 'Q' key
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SwapWeapon();
        }
    }

    void PickUpObject(GameObject obj)
    {
        // Pick up the object
        obj.SetActive(false);
        currentObject = obj;
        Debug.Log("Picked up: " + obj.name);
    }

    void SwapWeapon()
    {
        // Deactivate current weapon
        weapons[currentWeaponIndex].SetActive(false);

        // Increment weapon index or loop back to 0 if at the end
        currentWeaponIndex = (currentWeaponIndex + 1) % weapons.Length;

        // Activate new weapon
        weapons[currentWeaponIndex].SetActive(true);

        Debug.Log("Swapped to weapon: " + weapons[currentWeaponIndex].name);
    }
}
