using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    #region Singleton Init 

    public static GameManager instance = null;
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
        InitializeLoader();
    }

    #endregion

    #region Loader Init
    GameLoader loader;
    public GameLoader Loader { get { return loader; } }
    private void InitializeLoader() {
        loader = GetComponent<GameLoader>();
        if(loader == null) Debug.LogError("Loader is null!");
    }
    #endregion
    
    #region Settings Updater

    public void ColorSetUI(int value){
        SavePrefInt("color", value);
    }
    public static void SavePrefInt(string key, int value){
        PlayerPrefs.SetInt(key, value);
        PlayerPrefs.Save();
    }

    public static void SavePrefFloat(string key, float value){
        PlayerPrefs.SetFloat(key, value);
        PlayerPrefs.Save();
    }

    public static void SavePrefString(string key, string value){
        PlayerPrefs.SetString(key, value);
        PlayerPrefs.Save();
    }

    public static int GetPrefInt(string key){
        return PlayerPrefs.GetInt(key, 0);
    }

    public static float GetPrefFloat(string key){
        return PlayerPrefs.GetFloat(key, 0);
    }

    public static string GetPrefString(string key){
        return PlayerPrefs.GetString(key, "");
    }

    #endregion

    #region Pong Colors

    public static string[] colors = {"FF0000", "FFC400", "00FF44", "0031FF", "00FBFF", "FFFFFF"};

    #endregion

    private void Start() {
        switch(SceneManager.GetActiveScene().buildIndex){
            case 0: {
                loader.LoadLevel(1);
                break;
            }
        }
    }

}
