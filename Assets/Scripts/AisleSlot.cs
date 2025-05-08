using UnityEngine;
using UnityEngine.UI;

public class AisleSlot : MonoBehaviour
{

    public AisleItem aisleItem;
    public AisleProduct aisleProduct;

    public void SetSlotItem(AisleItem item, AisleProduct gameObject)
    {
        if (aisleProduct != null)
        {
            DeleteSlotItem();
        }
        aisleItem = item;
        aisleProduct = gameObject;
    }

    public void DeleteSlotItem()
    {
        if(aisleProduct != null && aisleProduct.gameObject != null)
        {
            Destroy(aisleProduct.gameObject);
        }
        aisleItem = null;
        aisleProduct = null;
    }

    // Draw gizmo
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 0.5f);
    }

}