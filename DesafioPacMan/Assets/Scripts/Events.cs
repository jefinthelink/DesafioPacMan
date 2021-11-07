using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class Events : MonoBehaviour
{
    [SerializeField] private GameObject PanelEvents;
    [SerializeField] float timeDoublePoints = 15.0f;
    [SerializeField] float timeEnemyFollowPlayer = 10.0f;
    [SerializeField] string[] bills;
    [SerializeField] int[] answers;
    [SerializeField] Button[] answersbtn;
    [SerializeField] private TMP_Text textAnswer;

    private Button btnWithTrueAnswer;
    private int indexOfbtnWithTrueAnswer;
    private int indexOfBills;
    private string currentBills;
    private float timeDoublePointsAux;
    private float timeEnemyFollowPlayerAux;
    private GameObject[] enemysInAceneObj;
    private Enemy[] enemysInAcene = new Enemy[1];
    private bool startDoublePoints = false , startFollowPlayer = false;

    private void Start()
    {
        SetValues();
    }
    private void SetValues()
    {
        timeDoublePointsAux = timeDoublePoints;
        timeEnemyFollowPlayerAux = timeEnemyFollowPlayer;
    }
    private void Update()
    {
        if (startDoublePoints)
        {
            timeDoublePoints -= Time.deltaTime;
            if (timeDoublePoints <= 0.0f)
            {
                startDoublePoints = false;
                timeDoublePoints = timeDoublePointsAux;
                ChangePoints(false);
            }
        }
        if (startFollowPlayer)
        {
            timeEnemyFollowPlayer -= Time.deltaTime;
            if (timeDoublePoints <= 0.0f)
            {
                startFollowPlayer = false;
                timeEnemyFollowPlayer = timeEnemyFollowPlayerAux;
                ChangeModes(Action.home);
            }
        }

    }
    public void StartEvent()
    {
        GameManager.instance.PauseGame();
        PanelEvents.SetActive(true);
        ChooseBills();
        ChooseBtn();
    }
    public void FinishEvent(bool win)
    {
        PanelEvents.SetActive(false);
        if (win)
        {
            VerifyEnemys();
            ChangePoints(true);
            startDoublePoints = true;
            GameManager.instance.UnpauseGame();
        }
        else
        {
            VerifyEnemys();
            startFollowPlayer = true;
            ChangeModes(Action.FollowPlayer);
            GameManager.instance.UnpauseGame();
        }
    }
    private void VerifyEnemys()
    {
        enemysInAceneObj = GameObject.FindGameObjectsWithTag("Enemy");
        
        for(int i = 0; i < enemysInAceneObj.Length; i++ )
        {
            enemysInAcene[i] = enemysInAceneObj[i].GetComponent<Enemy>();
        }
    }
    private void ChangeModes(Action action)
    {
        for (int i = 0; i < enemysInAcene.Length; i++)
        {
            enemysInAcene[i].currentAction = action;
        }
    }
    private void ChangePoints(bool double_)
    {
        if (double_)
        {
            for (int i = 0; i < enemysInAcene.Length; i++)
            {
                enemysInAcene[i].pointsValue *= 2;
            }
        }
        else 
        {
            for (int i = 0; i < enemysInAcene.Length; i++)
            {
                enemysInAcene[i].pointsValue /= 2;
            }
        }
    }

    private void ChooseBtn()
    {
        indexOfbtnWithTrueAnswer = Random.Range(0, answersbtn.Length);
        btnWithTrueAnswer = answersbtn[indexOfbtnWithTrueAnswer];
        btnWithTrueAnswer.GetComponentInChildren<TMP_Text>().text = answers[indexOfBills].ToString();
        btnWithTrueAnswer.onClick.AddListener(()=> { FinishEvent(true); });

        for (int i = 0; i < answersbtn.Length; i++)
        {
            if (i != indexOfbtnWithTrueAnswer)
            {
                answersbtn[i].onClick.AddListener(() => { FinishEvent(false); });
                answersbtn[i].GetComponentInChildren<TMP_Text>().text = Chooseanswers().ToString();
            }
        }
    }

    public void ChooseBills()
    {
        indexOfBills = Random.Range(0, bills.Length);
        currentBills = bills[indexOfBills];
        textAnswer.text = currentBills;
    }

    public int Chooseanswers()
    {
        int index;
        do
        {
        index = Random.Range(0, answers.Length);

        } while (index == indexOfBills);
        return answers[index];
    }

}
