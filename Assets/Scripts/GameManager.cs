using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Script.interfaces;
using UnityEngine.SceneManagement;
using Assets.Script.globalVar;
using Assets.Script.state;
using UnityEngine.InputSystem;
using System.IO;
using UnityEngine.Networking;

// Main Monster Enemy will be 10, after each 5 round 1 main monster enemy will be appeared
// So the Total round will be 50;
// Initially There will be 3 Enemy, after first main round, in each round 2 extra enemy will be introduced,
// So, Total enemy will be 11
// So 11 small Enemy  and 5 BIG Monster enemy  

public class GameManager : MonoBehaviour
{
    public delegate void StartTouchEvent(UnityEngine.Vector2 position);
    public event StartTouchEvent OnStartTouch;
    public delegate void EndTouchEvent(UnityEngine.Vector2 position);
    public event EndTouchEvent OnEndTouch;

    public GlobalData globalVariable;
    private GameState activeState;
    private static GameManager instanceRef;

    public MainTouchInput TouchInput;

    public bool Bool = false;

    string saveFilePath;

    IEnumerator GetUpdateRequest(string url){
        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();
        if(request.result == UnityWebRequest.Result.Success){
            string responseTex = request.downloadHandler.text;
            globalVariable.gameUpdateVersion = responseTex;
            StopCoroutine(GetUpdateRequest(url));
        }
        else{
            Debug.Log("Error...."+request.error);
            StopCoroutine(GetUpdateRequest(url));
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        if (TouchInput != null)
            TouchInput.Enable();

    }
    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        if (TouchInput != null)
            TouchInput.Disable();
    }

    public GameState getActiveState()
    {
        return activeState;
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (PlayerPrefs.GetInt("CurrentLevel") != null)
        {
            globalVariable.CurrentLevel = PlayerPrefs.GetInt("CurrentLevel");
        }
        else
        {
            globalVariable.CurrentLevel = 0;
        }
        if (activeState == null)
        {
            activeState = new LoaderState(this, globalVariable, "Menu");
        }
        activeState.getData();
        Screen.orientation = ScreenOrientation.LandscapeLeft;
    }
    void Start()
    {

        if (globalVariable == null)
        {
            globalVariable = new GlobalData();
        }
        if (activeState == null)
        {
            activeState = new LoaderState(this, globalVariable, "Menu");
        }

        TouchInput.TouchOnScreen.Touch.started += ctx => StartTouch(ctx);
        TouchInput.TouchOnScreen.Touch.canceled += ctx => EndTouch(ctx);

        string checkUpdateUrl = "gamegambu.com/version.php";
        StartCoroutine(GetUpdateRequest(checkUpdateUrl));
    }
    private void StartTouch(InputAction.CallbackContext context)
    {
        Bool = true;
        if (activeState.getStateName() == "attackState")
        {
            if (globalVariable.selectedWeapon == "Secondary")
            {
                StartCoroutine(DragUpdate());
            }
            else
            {
                if (OnStartTouch != null) OnStartTouch(TouchInput.TouchOnScreen.TouchPosition.ReadValue<UnityEngine.Vector2>());
            }
        }

    }
    void EndTouch(InputAction.CallbackContext context)
    {
        Bool = false;
        if (OnEndTouch != null) OnEndTouch(TouchInput.TouchOnScreen.TouchPosition.ReadValue<UnityEngine.Vector2>());
    }
    void Awake()
    {
        //Getting loader
        //loader.gameObject.SetActive(true);
        if (instanceRef == null)
        {
            instanceRef = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            if (this.gameObject != null)
                DestroyImmediate(this.gameObject);
        }
        if (globalVariable == null)
        {
            globalVariable = new GlobalData();
        }

        TouchInput = new MainTouchInput();
    }

    public void UnlockRound(int CurrentLevel)
    {
        PlayerPrefs.SetInt("CurrentLevel", CurrentLevel);
        PlayerPrefs.Save();
    }

    public void saveCurrentGameState(RoundGroupClass roundGroupClass)
    {
        RoundGroupClass MainRoundGroupClass = roundGroupClass;
        saveFilePath = Application.persistentDataPath + "/SavedGameData.json";
        string savePlayerData = JsonUtility.ToJson(MainRoundGroupClass);
        File.WriteAllText(saveFilePath, savePlayerData);
    }

    public RoundGroupClass getSavedGameState()
    {
        RoundGroupClass MainRoundGroupClass = globalVariable.initializeAllRound();
        saveFilePath = Application.persistentDataPath + "/SavedGameData.json";
        if (File.Exists(saveFilePath))
        {
            string loadGameData = File.ReadAllText(saveFilePath);
            MainRoundGroupClass = JsonUtility.FromJson<RoundGroupClass>(loadGameData);
        }
        return MainRoundGroupClass;
    }

    public void DeleteSaveFile()
    {
        saveFilePath = Application.persistentDataPath + "/Bappa.json";
        if (File.Exists(saveFilePath))
        {
            File.Delete(saveFilePath);
        }
        else
            Debug.Log("There is nothing to delete!");
    }

    public void switchState(GameState newState)
    {
        // loader.gameObject.SetActive(true);
        activeState = newState;
    }
    
    void OnMouseDown()
    {

    }
    void OnMouseUp()
    {
        globalVariable.isFiring = false;
    }

    void OnMouseDrag()
    {

    }
    void goForwardFunction()
    {

    }
    // Update is called once per frame
    void Update()
    {
        if (activeState != null)
        {
            activeState.stateUpdate();
        }
    }

    private IEnumerator DragUpdate()
    {
        while (Bool)
        {
            if (OnStartTouch != null) OnStartTouch(TouchInput.TouchOnScreen.TouchPosition.ReadValue<UnityEngine.Vector2>());
            // yield return new WaitForFixedUpdate();
            yield return new WaitForSeconds(0.2f);
        }
    }
}

