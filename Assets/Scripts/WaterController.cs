using UnityEngine;

public class WaterController : MonoBehaviour
{
    public GameObject waterBody;

    private void Start()
    {
        if (waterBody != null)
            waterBody.SetActive(false);
    }

    public void ToggleWater()
    {
        if (waterBody != null)
            waterBody.SetActive(!waterBody.activeSelf);
    }
}
