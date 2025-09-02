using UnityEngine;

public class Wallet : MonoBehaviour
{
    [SerializeField] private int dineroInicial = 500;
    public int Dinero { get; private set; }

    public delegate void OnMoneyChanged(int nuevoValor);
    public event OnMoneyChanged MoneyChanged;

    private void Awake()
    {
        Dinero = dineroInicial;
    }

    public bool PuedePagar(int costo) => Dinero >= costo;

    public bool Pagar(int costo)
    {
        if (!PuedePagar(costo)) return false;
        Dinero -= costo;
        MoneyChanged?.Invoke(Dinero);
        return true;
    }

    public void Cobrar(int monto)
    {
        Dinero += monto;
        MoneyChanged?.Invoke(Dinero);
    }
}
