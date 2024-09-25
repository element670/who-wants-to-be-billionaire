using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private UIController uiController;
    [SerializeField] private AudioController audioController;
    int index = 0;
    
    private ReadJson.Question[] questions;
    
    private void Start()
    {
        // Init qustions
        questions = ReadJson.createArrayOfQuestions();
        uiController.SetQuestion(questions[index]);

        // Subscribe to events A, B, C, D,  
        EventBus.Register<ButtonAction.Action>(ButtonAction.ACTION_NAME, GotAnswer);

        // Subscribe to events 50/50, call, hall
        EventBus.Register<HelpAction.Action>(HelpAction.ACTION_NAME, GetHelp);

    }
    private void GotAnswer(ButtonAction.Action action) 
    {
        StartCoroutine(CheckResult(action));
    }
   
    private void GetHelp(HelpAction.Action action)
    {
        var question = questions[index];
        switch(action)
        {
            case HelpAction.Action.Hall:
                uiController.Hall(question);
                break;
            case HelpAction.Action.PhoneCall:
                uiController.PhoneCall(question);
                break;
            case HelpAction.Action.Fifty:
                uiController.FiftyFifty(question);
                Debug.Log(questions[index]);
                break;
        }
        Debug.Log(action);
    }
    
    private IEnumerator CheckResult(ButtonAction.Action action)
    {
        Debug.LogError("handle " + action);
        var question = questions[index];
        //yellow state
        uiController.ChangeState(action, ButtonAction.UIState.Selected);
        //delay
        yield return new WaitForSeconds(1);
        if (action.ToString() == question.rightanswer)
        {

            //Green state
            uiController.ChangeState(action, ButtonAction.UIState.Passed);
            uiController.SetColorToMoney(index, questions.Length);
            index++;
            //if game is over
            yield return new WaitForSeconds(2);

            if (index == questions.Length)
            {
                //show particles
                uiController.ShowWinning();
                audioController.PlaySound();
                Debug.Log("WON!");
            }
            else
            {
                uiController.SetQuestion(questions[index]);
            }
        }
        else
        {
            //game is over
            index = 0;
            uiController.ChangeState(action, ButtonAction.UIState.Failed);
            uiController.ShowGameOver();

            yield return new WaitForSeconds(1);
            uiController.SetQuestion(questions[index]);
        }
        yield return new WaitForSeconds(1);
    
    }
}
