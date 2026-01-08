using TMPro;
using UnityEngine;

public class ScoreCalc : MonoBehaviour
{
    [Header("ScoreText")]
    [SerializeField] private TextMeshProUGUI ScoreTxt;
    [Header("Obj base tag")]
    [SerializeField] private int ObjLayer;
    [Header("BaseScore")]
    [SerializeField] private int BaseScore;
    private int Score;
    void Start()
    {
        ScoreTxt.text = "";
    }
    private void OnTriggerEnter2D(Collider2D info)
    {
        if (info.gameObject.layer == ObjLayer)
        {
            Score += BaseScore;
            ScoreTxt.text = Score.ToString("0");
        }
        if (info.tag == "SuperScore")
        {
            Score += BaseScore * 5;
            ScoreTxt.text = Score.ToString("0");
            Destroy(info.gameObject);
        }
    }
}
