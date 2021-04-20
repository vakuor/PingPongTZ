using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUIController : MonoBehaviour
{
    Button playBtn;
    Button settingsBtn;
    [SerializeField] private GameObject[] tabs;
    int enabledTab = 0;

    private void Start() {
        //SwitchToTab("main");
    }

    string tab = "main";
    public void FadeSwitchToTab(string tab){
        this.tab = tab;
        GameManager.instance.Loader.CallWithFade(SwitchToTab);
    }
    public void SwitchToTab(){
        int localTab = -1;
        switch (tab){
            case "main": {
                localTab = 0;
                break;
            }
            case "settings": {
                localTab = 1;
                break;
            }
            case "play": {
                localTab = 2;
                break;
            }
            default: {
                Debug.LogError("Tab name is incorrect!");
                return;
            }
        }
        
        if(localTab == enabledTab) return;
        tabs[localTab].SetActive(true);
        tabs[enabledTab].SetActive(false);
        enabledTab = localTab;
    }

}
