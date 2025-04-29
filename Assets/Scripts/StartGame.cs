using UnityEngine;

public class StartGame : MonoBehaviour
{
    [SerializeField]
    public Aisle[] aisles;

    private void Awake()
    {
        GameState.setupAisleProducts(aisles);
    }
}
