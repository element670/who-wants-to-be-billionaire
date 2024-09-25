using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static ButtonAction;
using UnityEngine.UI;
using static ReadJson;

public class UIController : MonoBehaviour
{
    [SerializeField] private ParticleSystem particleSystem;
    [SerializeField] private WinController winController;
    [SerializeField] private GameOver gameOver;
    [SerializeField] private Variants variants;
    [SerializeField] private TextMeshProUGUI displayAnswer;
    [SerializeField] private TextMeshProUGUI question;
    [SerializeField] private ButtonAction A;
    [SerializeField] private ButtonAction B;
    [SerializeField] private ButtonAction C;
    [SerializeField] private ButtonAction D;
    [SerializeField] private HelpAction Fifty;
    [SerializeField] private HelpAction HallHelp;
    [SerializeField] private HelpAction PhoneCallHelp;
    [SerializeField] private ProgressBar[] progressbar;

    public void SetQuestion(ReadJson.Question q)
    {
        
        displayAnswer.enabled = false;
        variants.SetActive(false);
        question.text = q.text;

        A.SetText(q.A);
        B.SetText(q.B);
        C.SetText(q.C);
        D.SetText(q.D);

        A.ChangeState(UIState.Default);
        B.ChangeState(UIState.Default);
        C.ChangeState(UIState.Default);
        D.ChangeState(UIState.Default);

        A.GetComponent<Button>().interactable = true;
        B.GetComponent<Button>().interactable = true;
        C.GetComponent<Button>().interactable = true;
        D.GetComponent<Button>().interactable = true;

     //   variants.SetActive(false);
    }
    public void ChangeState(ButtonAction.Action action, UIState state)
    {
        variants.SetActive(false);
        var selectedbutton = A;

        if(action == B.GetAction())
        {
            selectedbutton = B;
        }else if(action == C.GetAction()) 
        { 
            selectedbutton= C;
        }
        else if(action == D.GetAction())
        {
             selectedbutton = D;
        }
        
        selectedbutton.ChangeState(state);

        A.GetComponent<Button>().interactable = false;
        B.GetComponent<Button>().interactable = false;
        C.GetComponent<Button>().interactable = false;
        D.GetComponent<Button>().interactable = false;
    }
    
    #region Help
    public void FiftyFifty(ReadJson.Question q)
    {
        variants.SetActive(false) ;
        FindButtonForRightAnswer(q);
     
        var buttons = GetButtons();
        int index = 0;
        for(int i = 0; i < buttons.Count; i++)
        {
            if(index != 2 && !buttons[i].Equals(q))
            {
                buttons[i].GetComponent<Button>().interactable = false;
                index++;
            }
        }
        Fifty.GetComponent<Button>().interactable = false;
    }
    
    public void Hall(ReadJson.Question q)
    {
        variants.SetActive(true);

        int varA = Random.Range(0, 33);
        int varB = Random.Range(0, 33);
        int varC = Random.Range(0, 33);

        int varD = 100 - (varA + varB + varC);

        List<int> values = new List<int> { varA, varB, varC, varD };
        values.Shuffle();

        variants.A.text = "A " + values[0].ToString() + "%";
        progressbar[0].IncrementProgress((float)values[0]/100);

        variants.B.text = "B " + values[1].ToString() + "%";
        progressbar[1].IncrementProgress((float)values[1]/100);

        variants.C.text = "C " + values[2].ToString() + "%";
        progressbar[2].IncrementProgress((float)values[2]/100);

        variants.D.text = "D " + values[3].ToString() + "%";
        progressbar[3].IncrementProgress((float)values[3]/100);

        HallHelp.GetComponent<Button>().interactable = false;
    }
    public void PhoneCall(ReadJson.Question q)
    {
        List<char> buttons = new List<char>() { 'A', 'B', 'C', 'D' };

        buttons.Shuffle();
        string rightAnswer = "Answer is: " + buttons[0].ToString();
        
        displayAnswer.text = rightAnswer;
        displayAnswer.enabled = true;
        PhoneCallHelp.GetComponent<Button>().interactable = false;
        
        
    }
    #endregion
    
    public void ShowGameOver()
    {
        PhoneCallHelp.GetComponent<Button>().interactable = true;
        gameOver.gameObject.SetActive(true);
        
    }
    public void ShowWinning()
    {
        particleSystem.gameObject.SetActive(true);
        particleSystem.Play();
    }
    public void SetColorToMoney(int index, int questionLength)
    {
        winController.SetColorToMoney(index, questionLength, Color.yellow);
        winController.SetColorToMoney(++index, questionLength, Color.green);
    }

    private ButtonAction FindButtonForRightAnswer(ReadJson.Question q)
    {
        string rightAnswer = q.rightanswer;
        if (A.GetAction().ToString().Equals(rightAnswer))
        {
            return A;
        }
        if (B.GetAction().ToString().Equals(rightAnswer))
        {
            return B;
        }
        if (C.GetAction().ToString().Equals(rightAnswer))
        {
            return C;
        }
        return D;
    }

    private List<ButtonAction> GetButtons()
    {
       return new List<ButtonAction> 
        {
            A,
            B,
            C,
            D
        };
    }
    
    

    [System.Serializable]
    public struct Variants
    {
        public TextMeshProUGUI A, B, C, D;
        public readonly void SetActive(bool isActive)
        {
            A.gameObject.SetActive(isActive);
            B.gameObject.SetActive(isActive);
            C.gameObject.SetActive(isActive);
            D.gameObject.SetActive(isActive);

        }
    }
    

}

