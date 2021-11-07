using TMPro;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField] private TMP_Text textPoints;
    [SerializeField] private TMP_Text textPointsRecord;
    [SerializeField] private GameObject content;

    private void Start()
    {
        transform.gameObject.SetActive(false);
    }
    private void SetTextValues()
    {
        textPoints.text = GameManager.instance.points.ToString();
        textPointsRecord.text = GameManager.instance.pointsRecord.ToString();
    }

    public void ShowPanel()
    {
        content.SetActive(true);
        GameManager.instance.VerifyRecord();
        SetTextValues();
    }
}
