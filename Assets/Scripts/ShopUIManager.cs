using TMPro;
using UnityEngine;

public class ShopUIManager : MonoBehaviour
{
    private static ShopUIManager _instance;
    [SerializeField] TMP_Text _gemCountText;

    public static ShopUIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                throw new UnityException("No Shop UI Manager exists");
            }
            else
            {
                return _instance;
            }
        }
    }

    public void OpenShop(int gemCount)
    {
        _gemCountText.text = $"{gemCount} G";
    }

    private void Awake()
    {
        _instance = this;
    }
}
