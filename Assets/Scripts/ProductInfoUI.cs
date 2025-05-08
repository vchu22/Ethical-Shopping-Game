using TMPro;
using UnityEngine;

public class ProductInfoUI : MonoBehaviour
{
    public GameObject productInfoPanel;
    public GameObject ethicInfoBox;

    public TextMeshProUGUI productNameText;
    public TextMeshProUGUI productDescriptionText;
    public TextMeshProUGUI ethicInfoText;

    private AisleProduct currentProduct;

    // Method to show the product information panel
    public void ShowProductInfo(AisleProduct product)
    {
        if (GameState.currentRound == 0)
        {
            ethicInfoBox.SetActive(false);
        }
        currentProduct = product;
        productNameText.text = product.aisleItem.name;
        productDescriptionText.text = product.aisleItem.productDesciption;
        ethicInfoText.text = "";
        if (product.aisleItem.positiveEthicCategories.Length == 0 & product.aisleItem.negativeEthicCategories.Length == 0)
        {
            ethicInfoText.text = "None";
        }
        else
        {
            foreach (var item in product.aisleItem.positiveEthicCategories)
            {
                ethicInfoText.text += ("+ " + item + "\n");
            }
            foreach (var item in product.aisleItem.negativeEthicCategories)
            {
                ethicInfoText.text += ("- " + item + "\n");
            }
        }
        productInfoPanel.SetActive(true);
    }

    // Method to hide the product information panel
    public void HideProductInfo()
    {
        currentProduct = null;
        productInfoPanel.SetActive(false);
    }
}
