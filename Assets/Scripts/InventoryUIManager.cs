using UnityEngine;

public class InventoryUIManager : MonoBehaviour
{
    public static InventoryUIManager Instance;
    public GameObject inventoryUIRoot;

    private void Awake() => Instance = this;

    public void OnInventoryItemClicked(string elementName)
    {
        var craft = FindObjectOfType<CraftingUIManager>();
        if (craft != null && craft.gameObject.activeInHierarchy)
        {
            craft.OnInventoryAssign(elementName);
            return;
        }

        // If it's a usable item (e.g. Water)
        if (elementName == "Water")
        {
            var water = FindObjectOfType<WaterController>();
            if (water != null)
                water.ToggleWater();
        }
    }

    public void ToggleInventory()
    {
        inventoryUIRoot.SetActive(!inventoryUIRoot.activeSelf);
    }
}
