using System;
using UnityEngine;

public class StaminaStore : MonoBehaviour
{
    [Header("Player Prefs")]
    [SerializeField] private string _staminaPref = "Stamina";
    [SerializeField] private string _nextStaminaTimePref = "NextStaminaTime";
    [SerializeField] private string _lastStaminaTimePref = "LastStaminaTime";

    public static StaminaStore Instance;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;

            DontDestroyOnLoad(this);
        }
    }

    void Start()
    {
        StaminaManager.Instance.OnStaminaUpdate += PersistStamina;
    }

    public Stamina LoadStamina(int maxStamina = 0)
    {
        return new Stamina
        {
            Amount = PlayerPrefs.GetInt(_staminaPref, maxStamina),
            NextUpdateTime = StringToDateTime(PlayerPrefs.GetString(_nextStaminaTimePref)),
            LastUpdateTime = StringToDateTime(PlayerPrefs.GetString(_lastStaminaTimePref))
        };
    }

    public void PersistStamina(Stamina stamina)
    {
        PlayerPrefs.SetInt(_staminaPref, stamina.Amount);

        PlayerPrefs.SetString(_nextStaminaTimePref, stamina.NextUpdateTime.ToString());

        PlayerPrefs.SetString(_lastStaminaTimePref, stamina.LastUpdateTime.ToString());

        PlayerPrefs.Save();
    }

    public void DeletePersistedStamina()
    {
        PlayerPrefs.DeleteKey(_staminaPref);

        PlayerPrefs.DeleteKey(_nextStaminaTimePref);

        PlayerPrefs.DeleteKey(_lastStaminaTimePref);

        PlayerPrefs.Save();

        StaminaManager.Instance.OnStaminaUpdate -= PersistStamina;
    }

    private DateTime StringToDateTime(string date)
    {
        return string.IsNullOrEmpty(date) ? DateTime.Now : DateTime.Parse(date);
    }
}
