using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AisleProduct : MonoBehaviour
{

    public Action<AisleProduct> clicked; // The action to perform when the product is clicked

    public Action<AisleProduct> infoClicked; // The action to perform when the info button is clicked

    public AisleItem aisleItem; // The item that this product represents

    public TextMeshProUGUI productNameText;

    public Image productImage;

    public void OnInfoButtonClicked()
    {
        // Invoke the infoClicked action if it's not null
        infoClicked?.Invoke(this);
    }

    public void OnProductClicked()
    {
        // Invoke the clicked action if it's not null
        clicked?.Invoke(this);
    }

    void OnDestroy()
    {
        clicked = null;
        infoClicked = null;
    }

    public void SetAisleItem(AisleItem item)
    {
        aisleItem = item;
        productNameText.text = item.name;
    }

}
