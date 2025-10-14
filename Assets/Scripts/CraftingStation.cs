using UnityEngine;

public class CraftingStation : MonoBehaviour
{
    public GameObject craftingUI;

    private void Start()
    {
        if (craftingUI != null)
            craftingUI.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            craftingUI.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            craftingUI.SetActive(false);
    }
}
