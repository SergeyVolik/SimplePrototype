using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace SerV112.UtilityAI.Game
{
    public class SceneLoader : MonoBehaviour
    {

        [SerializeField]
        private GamePlaySceneSO m_GamePlayScene;

        [Header("Listening to")]
        [SerializeField] private LoadEventChannelSO _loadMenu = default;     
        [SerializeField] private LoadEventChannelSO _loadGameplayMap = default;

        [SerializeField] private InputReader m_InputReader;

        private bool _isLoading = false; //To prevent a new loading request while already loading a new scene

        private GameSceneSO _sceneToLoad;
        private GameSceneSO _currentlyLoadedScene;

        private AsyncOperationHandle<SceneInstance> _loadingOperationHandle;

        private void OnEnable()
        {
            _loadMenu.OnLoadingRequested += LoadMenu;
            _loadGameplayMap.OnLoadingRequested += LoadGameMap;
        }

        private void OnDisable()
        {
            _loadMenu.OnLoadingRequested -= LoadMenu;
            _loadGameplayMap.OnLoadingRequested -= LoadGameMap;
        }

        private void LoadMenu(GameSceneSO menuToLoad, bool showLoadingScreen, bool fadeScreen)
        {
            if (_isLoading)
                return;

            _sceneToLoad = menuToLoad;
            _isLoading = true;
            Debug.Log("LoadMenu");

            UnloadPreviousScene();
        }

        private void LoadGameMap(GameSceneSO gameMap, bool showLoadingScreen, bool fadeScreen)
        {
            if (_isLoading)
                return;

            _sceneToLoad = gameMap;
            _isLoading = true;
            Debug.Log("LoadGameplay");

            UnloadPreviousScene();
        }

        private void LoadNewScene()
        {

            _loadingOperationHandle = _sceneToLoad.sceneReference.LoadSceneAsync(LoadSceneMode.Additive, true, 0);
            _loadingOperationHandle.Completed += OnNewSceneLoaded;
        }

        private void OnNewSceneLoaded(AsyncOperationHandle<SceneInstance> obj)
        {
            //Save loaded scenes (to be unloaded at next load request)
            _currentlyLoadedScene = _sceneToLoad;

            Scene s = obj.Result.Scene;
            SceneManager.SetActiveScene(s);
            LightProbes.TetrahedralizeAsync();

            _isLoading = false;

        }
        private void UnloadPreviousScene()
        {
           

            if (_currentlyLoadedScene != null) //would be null if the game was started in Initialisation
            {
                if (_currentlyLoadedScene.sceneReference.OperationHandle.IsValid())
                {
                    //Unload the scene through its AssetReference, i.e. through the Addressable system
                    _currentlyLoadedScene.sceneReference.UnLoadScene();
                }
#if UNITY_EDITOR
                else
                {
                    //Only used when, after a "cold start", the player moves to a new scene
                    //Since the AsyncOperationHandle has not been used (the scene was already open in the editor),
                    //the scene needs to be unloaded using regular SceneManager instead of as an Addressable
                    SceneManager.UnloadSceneAsync(_currentlyLoadedScene.sceneReference.editorAsset.name);
                }
#endif
            }

            LoadNewScene();
        }
    }

}
