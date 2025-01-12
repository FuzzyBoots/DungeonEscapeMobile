using System.Collections;
using UnityEngine;

public class Diamond : MonoBehaviour
{
    [SerializeField] int _diamondValue = 1;

    bool _isActive = false;

    static WaitForSeconds _delay = new WaitForSeconds(1f);

    private void Start()
    {
        StartCoroutine(Activate());
    }

    IEnumerator Activate()
    {
        yield return _delay;

        _isActive = true;
    }

    public void SetValue(int gems)
    {
        _diamondValue = gems;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!_isActive) return;

        if (collision.TryGetComponent<Player>(out Player player))
        {
            player.AddDiamonds(_diamondValue);
            Destroy(gameObject);
        }
    }
}
