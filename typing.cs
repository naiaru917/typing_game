using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TypingGame : MonoBehaviour
{
    public Text wordText; // �P���\�����邽�߂�Text
    public Text feedbackText; // ���[�U�[�Ɍ��ʂ�\�����邽�߂�Text
    public Text gameClearText;
    public Text gameOverText;
    public InputField inputField; // ���[�U�[�����͂��邽�߂�InputField
    public GameObject RetryBtn; // ���g���C�{�^��


    private string[] words = { "����", "�C�b�e�c", "����", "�炪�悢", "������", "������GO�I" }; // �o�肷��P�ꃊ�X�g
    private int currentWordIndex = 0; // ���ݕ\������Ă���P��̃C���f�b�N�X

    public float maxTextSize = 150f;  // �ő�̃t�H���g�T�C�Y
    public float textDuration = 20f; // �t�H���g�T�C�Y���ω����鎞��

    private float startTextSize = 10f; // �����̃t�H���g�T�C�Y
    private float timeElapsed = 0f;    // �o�ߎ���

    void Start()
    {
        startTextSize = wordText.fontSize; // �����̃t�H���g�T�C�Y���擾
        DisplayNextWord(); // �Q�[���J�n���ɍŏ��̒P���\��
        RetryBtn.SetActive(false); // ���g���C�{�^�����\��
        wordText.fontSize = Mathf.RoundToInt(startTextSize);

        gameClearText.enabled = false;
        gameOverText.enabled = false;
    }

        void Update()
    {
        // ���[�U�[���͂̏���
        ProcessUserInput();

        // �t�H���g�T�C�Y�̃A�j���[�V����
        AnimateTextSize();

        //�Q�[���I�[�o�[����
        CheckGameOver();
    }

 
    void ProcessUserInput()
    {
        // ���[�U�[�̓��͕�������X�V
        string input = inputField.text.Trim();

        if (Input.GetKeyDown(KeyCode.Return))
        {
            CheckInput(input);
        }
    }

    //���������񂾂�傫�����鏈��
    void AnimateTextSize()
    {
        if (timeElapsed < textDuration)
        {
            timeElapsed += Time.deltaTime;
            float newSize = Mathf.Lerp(startTextSize, maxTextSize, timeElapsed / textDuration);
            wordText.fontSize = Mathf.RoundToInt(newSize); // �t�H���g�T�C�Y���X�V
        }
    }

    //���͂��������̐��딻��
    void CheckInput(string userInput)
    {
        if (string.Compare(userInput, words[currentWordIndex], true) == 0)
        {
            currentWordIndex++; // �����̏ꍇ�A���̒P��ɐi��
            DisplayNextWord();
        }
        else
        {
            feedbackText.text = "�s�����I������x���͂��Ă��������B";
            inputField.text = ""; // ���͗����N���A
        }
    }

    //���̖���
    void DisplayNextWord()
    {
        if (currentWordIndex < words.Length)
        {
            wordText.text = words[currentWordIndex]; // ���̒P���\��
            inputField.text = ""; // ���͗����N���A
            feedbackText.text = ""; // �t�B�[�h�o�b�N���N���A
            inputField.ActivateInputField(); // ���̓t�B�[���h���A�N�e�B�u��

            // �t�H���g�T�C�Y�A�j���[�V���������Z�b�g
            timeElapsed = 0f;
            wordText.fontSize = Mathf.RoundToInt(startTextSize); // �t�H���g�T�C�Y�������l�Ƀ��Z�b�g
        }
        else
        {
            inputField.text = ""; // ���͗����N���A
            wordText.enabled = false;  // �P�ꃊ�X�g���I��������Q�[���N���A�\��
            gameClearText.enabled = true;
            RetryBtn.SetActive(true); // ���g���C�{�^����\��
            feedbackText.enabled = false; // �t�B�[�h�o�b�N���\��
        }
    }

    void CheckGameOver()
    {
        if (wordText.fontSize >= maxTextSize)
        {
            gameOverText.enabled = true; // �Q�[���I�[�o�[�e�L�X�g��\��
            RetryBtn.SetActive(true); // ���g���C�{�^����\��
            wordText.enabled = false; // �P��̕\�����\����
            feedbackText.enabled = false; // �t�B�[�h�o�b�N���\��
            gameClearText.enabled = false; // �Q�[���N���A�e�L�X�g���\��
        }
    }

    //���g���C�{�^��
    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // �V�[�����ă��[�h���ăQ�[�������Z�b�g
    }
}
