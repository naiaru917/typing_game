using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TypingGame : MonoBehaviour
{
    public Text wordText; // 単語を表示するためのText
    public Text feedbackText; // ユーザーに結果を表示するためのText
    public Text gameClearText;
    public Text gameOverText;
    public InputField inputField; // ユーザーが入力するためのInputField
    public GameObject RetryBtn; // リトライボタン


    private string[] words = { "ありがとう", "さようなら", "Unity", "Hello World", "ゲーム", "タイピング" }; // 出題する単語リスト
    private int currentWordIndex = 0; // 現在表示されている単語のインデックス

    public float maxTextSize = 150f;  // 最大のフォントサイズ
    public float textDuration = 20f; // フォントサイズが変化する時間

    private float startTextSize = 10f; // 初期のフォントサイズ
    private float timeElapsed = 0f;    // 経過時間

    void Start()
    {
        startTextSize = wordText.fontSize; // 初期のフォントサイズを取得
        DisplayNextWord(); // ゲーム開始時に最初の単語を表示
        RetryBtn.SetActive(false); // リトライボタンを非表示
        wordText.fontSize = Mathf.RoundToInt(startTextSize);

        gameClearText.enabled = false;
        gameOverText.enabled = false;
    }

        void Update()
    {
        // ユーザー入力の処理
        ProcessUserInput();

        // フォントサイズのアニメーション
        AnimateTextSize();

        //ゲームオーバー判定
        CheckGameOver();
    }

 
    void ProcessUserInput()
    {
        // ユーザーの入力文字列を更新
        string input = inputField.text.Trim();

        if (Input.GetKeyDown(KeyCode.Return))
        {
            CheckInput(input);
        }
    }

    //文字をだんだん大きくする処理
    void AnimateTextSize()
    {
        if (timeElapsed < textDuration)
        {
            timeElapsed += Time.deltaTime;
            float newSize = Mathf.Lerp(startTextSize, maxTextSize, timeElapsed / textDuration);
            wordText.fontSize = Mathf.RoundToInt(newSize); // フォントサイズを更新
        }
    }

    //入力した文字の正誤判定
    void CheckInput(string userInput)
    {
        if (string.Compare(userInput, words[currentWordIndex], true) == 0)
        {
            currentWordIndex++; // 正解の場合、次の単語に進む
            DisplayNextWord();
        }
        else
        {
            feedbackText.text = "不正解！もう一度入力してください。";
            inputField.text = ""; // 入力欄をクリア
        }
    }

    //次の問題へ
    void DisplayNextWord()
    {
        if (currentWordIndex < words.Length)
        {
            wordText.text = words[currentWordIndex]; // 次の単語を表示
            inputField.text = ""; // 入力欄をクリア
            feedbackText.text = ""; // フィードバックをクリア
            inputField.ActivateInputField(); // 入力フィールドをアクティブに

            // フォントサイズアニメーションをリセット
            timeElapsed = 0f;
            wordText.fontSize = Mathf.RoundToInt(startTextSize); // フォントサイズを初期値にリセット
        }
        else
        {
            inputField.text = ""; // 入力欄をクリア
            wordText.enabled = false;  // 単語リストが終了したらゲームクリア表示
            gameClearText.enabled = true;
            RetryBtn.SetActive(true); // リトライボタンを表示
            feedbackText.enabled = false; // フィードバックを非表示
        }
    }

    void CheckGameOver()
    {
        if (wordText.fontSize >= maxTextSize)
        {
            gameOverText.enabled = true; // ゲームオーバーテキストを表示
            RetryBtn.SetActive(true); // リトライボタンを表示
            wordText.enabled = false; // 単語の表示を非表示に
            feedbackText.enabled = false; // フィードバックを非表示
            gameClearText.enabled = false; // ゲームクリアテキストを非表示
        }
    }

    //リトライボタン
    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // シーンを再ロードしてゲームをリセット
    }
}
