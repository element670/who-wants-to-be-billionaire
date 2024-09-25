using UnityEngine;
using UnityEngine.UI;


public class GameOver : MonoBehaviour
{
    private Button tryAgain;

    private void Awake()
    {
        tryAgain = gameObject.GetComponentInChildren<Button>();
        tryAgain.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
        });
    }

   
}
