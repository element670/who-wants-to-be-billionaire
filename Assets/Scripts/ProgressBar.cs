using UnityEngine;
using UnityEngine.UI;


public class ProgressBar : MonoBehaviour
{
    private Slider slider;
   
    private void Awake()
    {
        slider = gameObject.GetComponent<Slider>();
    }
    private void Start()
    {
        IncrementProgress(0.25f);
    }
    

    public void IncrementProgress(float setValue)
    {
        slider.value = setValue;
    }
}
