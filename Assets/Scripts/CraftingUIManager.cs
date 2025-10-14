using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CraftingUIManager : MonoBehaviour
{
    [System.Serializable]
    public class Recipe
    {
        public string result;
        public List<string> ingredients;
    }

    public List<Recipe> recipes;
    public TMP_Text[] slotTexts;
    public TMP_Text outputText;
    public Button craftButton;

    public GameObject waterPrefab;
    public GameObject ozonePrefab;
    public Transform outputPoint;

    private void Start()
    {
        craftButton.onClick.AddListener(CraftItem);
        ClearSlots();
    }

    public void OnInventoryAssign(string elementName)
    {
        for (int i = 0; i < slotTexts.Length; i++)
        {
            if (slotTexts[i].text == "[ empty ]")
            {
                slotTexts[i].text = elementName;
                return;
            }
        }
        outputText.text = "All slots full!";
    }

    private void ClearSlots()
    {
        foreach (var t in slotTexts)
            t.text = "[ empty ]";
    }

    private void CraftItem()
    {
        List<string> inputs = new List<string>();
        foreach (var t in slotTexts)
            if (t.text != "[ empty ]") inputs.Add(t.text);

        foreach (var recipe in recipes)
        {
            if (MatchRecipe(inputs, recipe.ingredients))
            {
                PlayerInventory.Instance.RemoveElements(recipe.ingredients);
                SpawnResult(recipe.result);
                outputText.text = "Crafted: " + recipe.result;
                ClearSlots();
                return;
            }
        }

        outputText.text = "Invalid combination!";
    }

    private bool MatchRecipe(List<string> given, List<string> needed)
    {
        var copy = new List<string>(given);
        foreach (var ing in needed)
        {
            if (!copy.Remove(ing)) return false;
        }
        return copy.Count == 0;
    }

    private void SpawnResult(string result)
    {
        GameObject prefab = result switch
        {
            "Water" => waterPrefab,
            "Ozone" => ozonePrefab,
            _ => null
        };

        if (prefab)
        {
            Instantiate(prefab, outputPoint.position, Quaternion.identity);
            PlayerInventory.Instance.AddElement(result);
        }
    }
}
