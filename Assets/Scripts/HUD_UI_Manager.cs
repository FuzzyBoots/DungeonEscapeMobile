using TMPro;
using UnityEngine;
using UnityEngine.Assertions;

public class HUD_UI_Manager : MonoBehaviour
{
    [SerializeField] TMP_Text _gemText;
    [SerializeField] GameObject[] _lifeBarSlots;

    private static HUD_UI_Manager _instance;
    public static HUD_UI_Manager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("HUD UI Manager is null");
            }

            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        Assert.IsNotNull(_gemText);
    }

    public void SetGemCount(int count)
    {
        _gemText.text = count.ToString();
    }

    public void SetLife(int life)
    {
        if (life < 0 || life > _lifeBarSlots.Length)
        {
            Debug.LogError($"Attempted to set life to {life}, which is out of bounds");
        }

        for (int i = 0; i < _lifeBarSlots.Length; i++)
        {
            if (i < life)
            {
                _lifeBarSlots[i].SetActive(true);
            } else
            {
                _lifeBarSlots[i].SetActive(false);
            }
        }
    }
}
