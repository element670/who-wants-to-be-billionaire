using TMPro;
using UnityEngine;

public class WinController : MonoBehaviour
{

    [SerializeField] private GameObject moneyHolder;
    
    public void SetColorToMoney(int currentQuestionIndex, int size, Color color)
    {
        if(currentQuestionIndex < size)
        {
            moneyHolder.transform.GetChild(currentQuestionIndex).GetComponent<TextMeshProUGUI>().color = color;
        }
        
    }
}
