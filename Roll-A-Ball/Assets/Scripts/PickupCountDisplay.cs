using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PickupCountDisplay : MonoBehaviour
{
    public int pickupCount = 0;
    [SerializeField]
    private TextMeshProUGUI pickupText;

    private void Start()
    {
        pickupText = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        pickupText.text = pickupCount.ToString();
    }
}
