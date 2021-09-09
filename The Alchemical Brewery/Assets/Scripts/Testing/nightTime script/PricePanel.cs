using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PricePanel : MonoBehaviour
{
    public TextMeshProUGUI priceText;

    private void Update()
    {
        priceText.text = "$ " + PlayerProfile.cashTotal;
    }
}
