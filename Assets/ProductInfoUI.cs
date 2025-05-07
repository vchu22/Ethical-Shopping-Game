using TMPro;
using UnityEngine;

public class ProductInfoUI : MonoBehaviour
{
    public GameObject productInfoPanel;
    public TextMeshProUGUI productNameText;
    public TextMeshProUGUI productDescriptionText;

    private AisleProduct currentProduct;

    // Method to show the product information panel
    public void ShowProductInfo(AisleProduct product)
    {
        currentProduct = product;
        productNameText.text = product.aisleItem.name;
        productInfoPanel.SetActive(true);
    }

    // Method to hide the product information panel
    public void HideProductInfo()
    {
        currentProduct = null;
        productInfoPanel.SetActive(false);
    }
}
