using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopScript : MonoBehaviour
{
    [SerializeField] Canvas _shopCanvas;

    [SerializeField] RectTransform _selectionObjectTransform;

    [SerializeField] Vector3 _selectionObjectOffset = new Vector3(-8f, -20f, 0f);

    [SerializeField] GameObject _diamond;

    [SerializeField] GameObject _particlePrefab;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _particlePrefab.GetComponent<ParticleSystem>().name = "New Particle System";
        Destroy(_particlePrefab.GetComponent<ParticleSystem>());
        // Destroy(_particlePrefab);

        if (collision.TryGetComponent<Player>(out Player player))
        {
            _shopCanvas.gameObject.SetActive(true);
            ShopUIManager.Instance.OpenShop(player.Diamonds);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _shopCanvas.gameObject.SetActive(false);
        }
    }

    public void SelectItem(int item)
    {
        // Highlight the item in question
        switch (item)
        {
            case 1:
                // Select Flame Sword
                break;
            case 2:
                // Select Boots of Flight
                break;
            case 3:
                // Select Castle Key
                break;
        }
    }
}
