using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ElementCollectable : MonoBehaviour
{
    public string elementName;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInventory.Instance.AddElement(elementName);
            Destroy(gameObject);
        }
    }
}
