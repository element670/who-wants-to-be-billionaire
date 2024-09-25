using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class HelpAction : MonoBehaviour
{
    public static readonly string ACTION_NAME = "HelpClick";
    public enum Action
    {
        Fifty, PhoneCall, Hall
    }

    private Button button;
    [SerializeField] private Action action;
    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(() => {
            EventBus.Trigger<Action>(ACTION_NAME, action);
        });
    }
}
