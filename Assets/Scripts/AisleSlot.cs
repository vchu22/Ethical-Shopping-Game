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
        Destroy(aisleProduct);
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