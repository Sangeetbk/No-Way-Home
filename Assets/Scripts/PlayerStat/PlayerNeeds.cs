using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Unity.Collections;
public class PlayerNeeds : MonoBehaviour, IDamagable
{
    [SerializeField] Need _health;
    [SerializeField] Need _hunger;
    [SerializeField] Need _thirst;
    [SerializeField] Need _sleep;

[Space]
    public float noHungerHealthDecay;
    public float noThurstHealthDecay;

    [Space]
    public UnityEvent OnTakeDamage;

    private void Start() 
    {
        _health.CurrentValue = _health.MaxValue;
        _hunger.CurrentValue = _hunger.MaxValue;
        _sleep.CurrentValue = _sleep.MaxValue;
        _thirst.CurrentValue = _thirst.MaxValue;
    }

    void Update()
    {
        //decay needs overtime
        _hunger.Subtract(_hunger.DecayRate * Time.deltaTime);
        _thirst.Subtract(_thirst.DecayRate * Time.deltaTime);
        _sleep.Add(_sleep.RegenRate * Time.deltaTime);

        if(_hunger.CurrentValue == 0.0f)
        {
            _health.Subtract(noHungerHealthDecay * Time.deltaTime);
        }
        if(_thirst.CurrentValue == 0.0f)
        {
            _health.Subtract(noThurstHealthDecay * Time.deltaTime);
        }
        // check if player is dead
        if(_health.CurrentValue == 0.0f)
        {
            Die();
        }


        // Updating the Ui Bar
        _health.UIBar.fillAmount = _health.GetPercentage();
        _thirst.UIBar.fillAmount = _thirst.GetPercentage();
        _hunger.UIBar.fillAmount = _hunger.GetPercentage();
        _sleep.UIBar.fillAmount = _sleep.GetPercentage();
        
    }

    public void Heal(float amount)
    {
        _health.Add(amount);
    }

    public void Eat(float amount)
    {
        _hunger.Add(amount);
    }

    public void Drink(float amount)
    {
        _thirst.Add(amount);
    }

    public void Sleep(float amount)
    {
        _sleep.Subtract(amount);
    }

    public void TakePhysicalDamage(int amount)
    {
        _health.Subtract(amount);
        OnTakeDamage?.Invoke();
    }
    

    public void Die()
    {
        Debug.Log("Player Dead");
    }

}

[System.Serializable]
public class Need
{
    [HideInInspector]
    public float CurrentValue;
    public float MaxValue;
    public float StartValue;
    public float RegenRate;
    public float DecayRate;
    public Image UIBar;

    public void Add(float amount)
    {
        CurrentValue = Mathf.Min(CurrentValue + amount, MaxValue);
    }

    public void Subtract(float amount)
    {
        CurrentValue = Mathf.Max(CurrentValue - amount, 0f);
    }

    public float GetPercentage()
    {
        return CurrentValue / MaxValue;
    }
}

public interface IDamagable
{
    void TakePhysicalDamage(int amount);
}
