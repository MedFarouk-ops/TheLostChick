using UnityEngine;
using TMPro;

public class ScoreTextScript : MonoBehaviour
{
    TextMeshProUGUI text;
    [SerializeField] public int coinAmount;
    public static ScoreTextScript instance;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = coinAmount.ToString();
    }

    public void ResetScore()
    {
        coinAmount = 0;
    }
}
