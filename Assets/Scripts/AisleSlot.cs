using UnityEngine;
using UnityEngine.UI;

public class AisleSlot : MonoBehaviour
{

    public AisleItem aisleItem;
    public GameObject aisleItemObject;

    public void SetAisleItem(AisleItem item, GameObject gameObject)
    {
        aisleItem = item;
        aisleItemObject = gameObject;
    }

    public void DeleteAisleItem()
    {
        Destroy(aisleItemObject);
        aisleItem = null;
        aisleItemObject = null;
    }

}