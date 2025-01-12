using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopScript : MonoBehaviour
{
    [SerializeField] Canvas _shopCanvas;

    [SerializeField] ToggleGroup _itemsToggleGroup;

    [SerializeField] List<Toggle> _purchaseToggles;

    Player _player;

    private void OnEnable()
    {
        // Ensure that all of our toggles are off
        _itemsToggleGroup.SetAllTogglesOff();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player) && _shopCanvas)
        {
            _player = player;
            _shopCanvas.gameObject.SetActive(true);
            ShopUIManager.Instance.OpenShop(_player.Diamonds);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out _) && _shopCanvas)
        {
            _player = null;
            _shopCanvas.gameObject.SetActive(false);
        }
    }

    public void BuyItems()
    {
        int totalCost = 0;
        bool flamingSword = false, flyingBoots = false, castleKey = false;
        foreach (Toggle item in _itemsToggleGroup.ActiveToggles())
        {
            switch (item.name)
            {
                case "Flaming Sword Button":
                    flamingSword = true;
                    totalCost += 200;   // TODO: Find a way to source this value
                    break;
                case "Boots of Flight Button":
                    flyingBoots = true;
                    totalCost += 400;
                    break;
                case "Castle Key Button":
                    castleKey = true;
                    totalCost += 100;
                    break;
            }
        }

        // Check to see if player has enough money. If so, we add the items
        if (_player.Diamonds >= totalCost)
        {
            _player.RemoveDiamonds(totalCost);
            
            // Buy all of the items
            if (flamingSword)
            {
                _player.FlamingSwordActive = true;
            }
            if (flyingBoots)
            {
                _player.FlyingBootsActive= true;
            }
            if (castleKey)
            {
                _player.CastleKeyActive = true;
            }

            _shopCanvas.gameObject.SetActive(false);
        } else
        {
            Debug.Log("Not enough diamonds, exiting shop.");
            _shopCanvas.gameObject.SetActive(false);
        }
    }
}
