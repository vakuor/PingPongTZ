using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class GameStateController : MonoBehaviour
{
    #region Singleton Init 

    public static GameStateController instance = null;
    private void Awake() {
        if (instance == null) {
	    instance = this;
	    } else if(instance != this) {
            Destroy(gameObject);
            return;
	    }
	    //DontDestroyOnLoad(gameObject);
	    InitializeManager();
    }

    private void InitializeManager(){ 
        InitializeGameState();
    }

    #endregion
    public enum GameStates
    {
        Preparing,
        Starting,
        Over,
        Terminate
    }
    
    public UnityEvent OnRocketHit = new UnityEvent();
    public UnityEvent OnWallHit = new UnityEvent();
    public GameStateUnityEvent OnGameStateChanged = new GameStateUnityEvent();
    public IntUnityEvent OnScoreChanged = new IntUnityEvent();
    private Vector2 touchWorldPosition;
    public Vector2 TouchWorldPosition { get { return touchWorldPosition; } }
    private GameStates gameState;
    public GameStates GameState { get { return gameState; } set { gameState = value; OnGameStateChanged.Invoke(gameState); } }
    [SerializeField] private PongBehaviour pong;
    [SerializeField] private TextMeshProUGUI currentScoreText;
    [SerializeField] private TextMeshProUGUI maxScoreText;
    private int score;
    private int bestScore;
    public int Score { get { return score; } set { score = value; OnScoreChanged.Invoke(score); } }
    private void InitializeGameState(){
        if(pong == null) Debug.LogError("Pong Behaviour is null!");
        if(OnGameStateChanged == null) Debug.LogError("OnGameStateChanged is null!");

        OnGameStateChanged.AddListener(OnGameStateChangedAction);
        OnRocketHit.AddListener(OnRocketHitAction);
        OnWallHit.AddListener(OnWallHitAction);
        OnScoreChanged.AddListener(OnScoreChangedAction);

        bestScore = GameManager.GetPrefInt("bestscore");

        GameState = GameStates.Preparing;
    }
    private void Start() {
        Invoke("StartGame", 2f);
    }
    private void Restart(){
        GameState = GameStates.Preparing;
        Invoke("StartGame", 1f);
    }
    private void StartGame(){
        GameState = GameStates.Starting;
    }
    public void StopGame(){
        GameState = GameStates.Terminate;
    }
    private void Update() {
        Vector2 touchPos = PlayerInputController.GetFirstTouch();
        Vector2 posProportions = new Vector2(touchPos.x / Screen.width , touchPos.y / Screen.height);
        touchWorldPosition = posProportions;
    }

    private void OnGameStateChangedAction(GameStates state){
        Debug.Log("GameState Changed to: " + state.ToString());
        switch(state){
            case GameStates.Preparing: {
                pong.Respawn();
                Score = 0;
                UpdateBestScoreText(bestScore);
                break;
            }
            case GameStates.Starting: {
                pong.Launch();
                break;
            }
            case GameStates.Over:{
                pong.Stop();
                if(score>bestScore) {
                    bestScore = score;
                    UpdateBestScore(bestScore);
                }
                Invoke("Restart", 3f);
                break;
            }
            case GameStates.Terminate:{
                CancelInvoke();
                pong.Stop();
                if(score>bestScore) {
                    bestScore = score;
                    UpdateBestScore(bestScore);
                }
                break;
            }
            default: {
                break;
            }
        }
    }
    private void UpdateBestScore(int score){
        GameManager.SavePrefInt("bestscore", score);
    }
    private void OnRocketHitAction(){
        Debug.Log("Hit!");
        Debug.Log("RocketHit score1: "+score);
        Score = Score + 1;
        Debug.Log("RocketHit score: "+score);
    }
    private void OnWallHitAction(){
        Debug.Log("Hit wall!");
        GameState = GameStates.Over;
    }
    private void OnScoreChangedAction(int score){
        Debug.Log("Score: " + score);
        currentScoreText.text = "Score: " + score;
    }
    private void UpdateBestScoreText(int bestscore){
        Debug.Log("upd best: " + bestScore);
        maxScoreText.text = "Best score: " + bestScore;
    }
}
