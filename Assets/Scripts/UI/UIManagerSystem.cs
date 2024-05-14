using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManagerSystem : MonoBehaviour
{

    [Header("Main Menu")]
    [SerializeField] GameObject MaimMenuGameobject;
    public Button Playbtn;
    [SerializeField] Button Exitbtn;
    [SerializeField] Button Shopbtn;
    [SerializeField] Button Settingbtn;


    [Header("Game Play")]
    [SerializeField] GameObject GamePlayGameobject;
    [SerializeField] Button PlayGamebtn;
    [SerializeField] Button ComeBackHomebtn;
    [SerializeField] Button ExitGamePlaybtn;//on click exit then will return mainmenu
    


    [Header("Setting Menu")]
    [SerializeField] GameObject SettingMenuGameobject;
    [SerializeField] Button Homebtn;
    [SerializeField] Button DeActiveSettingMenubtn;
    [SerializeField] Button Stopbtn;
    [SerializeField] Button ContinueBtn;
    [SerializeField] Button TurnOnMusicbtn;
    [SerializeField] Button OffMusicbtn;
    [SerializeField] Button Reload;

    [Header("Game Win")]
    [SerializeField] GameObject GojbWin;
    [SerializeField] Button NewContinuePlaybtn;
    [SerializeField] Button ExitGameWin;

    [Header("Game Lose")]
    [SerializeField] GameObject GojbLose;
    [SerializeField] Button RePlay;
    [SerializeField] Button ExitGameLose;


    [Header("ActiveMenuSetiing && LevelPanel")]
    [SerializeField] Button ActiveSettingMenubtn;
    public TextMeshProUGUI _textLevel;


    //delegate
    public delegate void DelegatePlayGame();
    public static DelegatePlayGame PlayGameEvent;

    public delegate void DelegateWinGame();
    public static DelegatePlayGame WinGameEvent;

    public delegate void DelegateLoseGame();
    public static DelegatePlayGame LoseGameEvent;

    //[Header("Game Loses")]
    private void Start()
    {
        Time.timeScale = 0;
        MaimMenuGameobject.SetActive(true);

        //treatment Main Menu
        Playbtn.onClick.AddListener(ActiveGamePlayAndeactiveMainMenu);
        Exitbtn.onClick.AddListener(QuitGame);
        Settingbtn.onClick.AddListener(ActiveSettingMenu);


        //treatment Game Play
        ExitGamePlaybtn.onClick.AddListener(QuitGame);
        ComeBackHomebtn.onClick.AddListener(DeActiveGamePlayAndActiveMainMenu);
        PlayGamebtn.onClick.AddListener(PlayGame);


        //treatment Setting menu
        DeActiveSettingMenubtn.onClick.AddListener(DeActiveSettingMenu);
        Stopbtn.onClick.AddListener(StopGame);
        ContinueBtn.onClick.AddListener(ContinueGame);
        TurnOnMusicbtn.onClick.AddListener(IsTurnOnMusic);
        OffMusicbtn.onClick.AddListener(IsOffMusic);

        //treatment Game Win
        NewContinuePlaybtn.onClick.AddListener(DeActiveGameWin);
        //PlayReload.onClick.AddListener();

        //treatment Game Lose
        RePlay.onClick.AddListener(RePlayAfterLose);

        //treatment ActiveMenuSetiing && LevelPanel
        ActiveSettingMenubtn.onClick.AddListener(ActiveSettingMenu);
    }
    private void OnEnable()
    {
        GameController.WinEvent += ActiveGameWin;
        GameController.GetvalueLevelEvent += SetlevelText;
        BotAi.LoseEvent += ActiveLoseGame;
    }
    private void OnDisable()
    {
        GameController.WinEvent -= ActiveGameWin;
        GameController.GetvalueLevelEvent += SetlevelText;
    }
    private void SetlevelText()
    {
        int a = LevelManager.Instance.valuesLevel + 1;
        _textLevel.text = "Level: "+ a.ToString();
    }
    private void QuitGame()
    {
        Application.Quit();
    }
    private void ActiveSettingMenu()
    {
        SettingMenuGameobject.SetActive(true);
    }
    private void ActiveLoseGame()
    {

        GojbLose.SetActive(true);
        
    }
    private void RePlayAfterLose()
    {
        GojbLose.SetActive(false);
        Time.timeScale = 1;
        LoseGameEvent?.Invoke();
    }
    private void StopGame()
    {
        Time.timeScale = 0;
        Stopbtn.gameObject.SetActive(false);
        ContinueBtn.gameObject.SetActive(true);
    }

    private void ContinueGame()
    {
        Time.timeScale = 1;
        Stopbtn.gameObject.SetActive(true);
        ContinueBtn.gameObject.SetActive(false);
    }
    private void IsTurnOnMusic()
    {
        TurnOnMusicbtn.gameObject.SetActive(false);
        OffMusicbtn.gameObject.SetActive(true);
    }
    private void IsOffMusic()
    {
        TurnOnMusicbtn.gameObject.SetActive(true);
        OffMusicbtn.gameObject.SetActive(false);
        
    }
    private void PlayGame()
    {
        
        GamePlayGameobject.SetActive(false);
        LevelManager.Instance.PLayGameLevel1();
        PlayGameEvent?.Invoke();
        Time.timeScale = 1;
    }
    private void ActiveGameWin()
    {
       GojbWin.SetActive(true);
    }
    private void DeActiveGameWin()//khi chien thang goi và an tiep tuc thi se goi den 1 event de sang man thu 2
    {
        GojbWin.SetActive(false);
        Time.timeScale = 1;
        WinGameEvent?.Invoke();
    }
    private void DeActiveSettingMenu()
    {
        SettingMenuGameobject.SetActive(false);
    }
    private void ActiveGamePlayAndeactiveMainMenu()
    {
        MaimMenuGameobject.SetActive(false);
        GamePlayGameobject.SetActive(true);
        
    }
    private void DeActiveGamePlayAndActiveMainMenu()
    {
        MaimMenuGameobject.SetActive(true);
        GamePlayGameobject.SetActive(false);
    }
}
