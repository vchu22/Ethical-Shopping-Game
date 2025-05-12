using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CartProduct : MonoBehaviour
{
    public AisleItem aisleItem;
    
    public TextMeshProUGUI productNameText;
    
    public Image productImage;
    
    public void SetItem(AisleItem item)
    {
        aisleItem = item;
        productNameText.text = item.name;
        productImage.sprite = item.image;
    }
}
