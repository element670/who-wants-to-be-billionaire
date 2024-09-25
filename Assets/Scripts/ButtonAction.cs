using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonAction : MonoBehaviour
{
    public static readonly string ACTION_NAME = "ButtonAction";
    [SerializeField] private Action action;
    private Image image;
    private TextMeshProUGUI title;
    private Button button;

    private void Awake()
    {
        image = GetComponent<Image>();
        title = GetComponentInChildren<TextMeshProUGUI>();
        button = GetComponent<Button>();
        button.onClick.AddListener(() => {
            Debug.Log("emit " + action);
            EventBus.Trigger<Action>(ACTION_NAME, action);
        }); 
    }
    public Action GetAction()
    {
        return action;
    }

    public void SetText(string text)
    {
        title.text = text;
    }
    public void ChangeState(UIState state)
    {
        switch(state)
        {
            case UIState.Selected:
                image.color = Color.yellow;
                break;
            case UIState.Passed:
                image.color = Color.green;
                StartCoroutine(Blink());

                break;
            case UIState.Failed:
                image.color = Color.red;
                StartCoroutine(Blink());
                break;
            default:
                StopAllCoroutines();
                image.color = Color.white;
                break;
        }
    }
    private IEnumerator Blink()
    {
        Color color = image.color;
        while(true)
        {
            color.a = 0.4f;
            
            image.color = color;
            yield return new WaitForSeconds(0.05f);

            color.a = 1f;
            image.color = color;
            yield return new WaitForSeconds(0.05f);

        }
    }
    public enum Action
    {
        A, B, C, D
    }
    public enum UIState
    {
        Default, Selected, Passed, Failed
    }

}
