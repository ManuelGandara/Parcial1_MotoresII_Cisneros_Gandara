using System;
using System.Collections;
using UnityEngine;

public class StaminaManager : MonoBehaviour
{
    [Header("Stamina")]
    [SerializeField] private int _maxStamina;
    [SerializeField] private float _secondsToRechargeStamina;
    bool _isNotRechargingStamina;
    Stamina _stamina;

    public delegate void StaminaUpdate(Stamina stamina);

    public event StaminaUpdate OnStaminaUpdate;

    public static StaminaManager Instance;

    private void Awake()
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
        _stamina = StaminaStorage.Instance.LoadStamina(_maxStamina);

        OnStaminaUpdate(_stamina);

        StartCoroutine(AutoRechargeStamina());
    }

    public Stamina CurrentStamina { get { return _stamina; } }

    public int MaxStamina { get { return _maxStamina;  } }

    // Usar para deshabilitar el botón de jugar cuando no haya suficiente stamina
    public bool DoesNotHaveEnoughStamina(int amount)
    {
        return _stamina.Amount < amount;
    }

    public bool DoesNotHaveMaxStamina()
    {
        return DoesNotHaveEnoughStamina(_maxStamina);
    }

    // Usar cada vez que se inicie la partida
    public void ConsumeStamina(int consumedAmount)
    {
        if (DoesNotHaveEnoughStamina(consumedAmount)) return;

        UpdateStamina(-consumedAmount);
    }

    public void RechargeStamina(int rechargedAmount)
    {
        UpdateStamina(rechargedAmount);
    }

    private void UpdateStamina(int amount)
    {
        _stamina.Amount = Mathf.Clamp(_stamina.Amount + amount, 0, _maxStamina);

        if (_isNotRechargingStamina && DoesNotHaveMaxStamina())
        {
            _stamina.NextUpdateTime = DateTime.Now.AddSeconds(_secondsToRechargeStamina);

            StartCoroutine(AutoRechargeStamina());
        }

        OnStaminaUpdate(_stamina);
    }

    private IEnumerator AutoRechargeStamina()
    {
        _isNotRechargingStamina = false;

        while (DoesNotHaveMaxStamina())
        {
            DateTime currentTime = DateTime.Now;

            DateTime nextUpdateTime = _stamina.NextUpdateTime;

            bool hasRechargedStamina = false;

            while (currentTime > nextUpdateTime)
            {
                if (_stamina.Amount >= _maxStamina) break;

                RechargeStamina(1);

                hasRechargedStamina = true;

                if (_stamina.LastUpdateTime > nextUpdateTime)
                    nextUpdateTime = _stamina.LastUpdateTime;

                nextUpdateTime = nextUpdateTime.AddSeconds(_secondsToRechargeStamina);
            }

            if (hasRechargedStamina)
            {
                _stamina.NextUpdateTime = nextUpdateTime;

                _stamina.LastUpdateTime = DateTime.Now;
            }

            OnStaminaUpdate(_stamina);

            yield return new WaitForEndOfFrame();
        }

        _isNotRechargingStamina = true;
    }
}
