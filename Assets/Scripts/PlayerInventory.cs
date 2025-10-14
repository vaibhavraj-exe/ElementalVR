using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory Instance;

    [Header("UI References")]
    public Transform inventoryContent;
    public GameObject inventoryItemPrefab;
    public GameObject inventoryUIObject;

    private List<string> collectedElements = new List<string>();

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void AddElement(string elementName)
    {
        collectedElements.Add(elementName);
        UpdateInventoryUI();
    }

    public void RemoveElements(List<string> used)
    {
        foreach (var e in used)
            collectedElements.Remove(e);
        UpdateInventoryUI();
    }

    public bool HasElements(List<string> required)
    {
        List<string> copy = new List<string>(collectedElements);
        foreach (var r in required)
        {
            if (!copy.Remove(r)) return false;
        }
        return true;
    }

    public void UpdateInventoryUI()
    {
        foreach (Transform child in inventoryContent)
            Destroy(child.gameObject);

        foreach (string element in collectedElements)
        {
            GameObject item = Instantiate(inventoryItemPrefab, inventoryContent);
            var txt = item.GetComponentInChildren<TextMeshProUGUI>();
            if (txt) txt.text = element;

            var btn = item.GetComponent<Button>();
            if (btn)
            {
                string captured = element;
                btn.onClick.RemoveAllListeners();
                btn.onClick.AddListener(() =>
                {
                    InventoryUIManager.Instance.OnInventoryItemClicked(captured);
                });
            }
        }
    }

    public List<string> GetInventoryList() => new List<string>(collectedElements);
}
