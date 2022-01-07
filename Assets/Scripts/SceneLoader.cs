using SerV112.UtilityAI.Game.Channels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace SerV112.UtilityAI.Game.Managers
{
    public class SceneLoader : MonoBehaviour
    {


        [Header("Listening to")]
        [SerializeField] private LoadEventChannelSO _loadMenu = default;     
        [SerializeField] private LoadEventChannelSO _loadGameplayMap = default;

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

        bool notUnloadScene;
        private void LoadMenu(GameSceneSO menuToLoad, bool showLoadingScreen, bool fadeScreen)
        {
            if (_isLoading)
                return;
           
            if (_sceneToLoad == menuToLoad)
                notUnloadScene = true;

            _sceneToLoad = menuToLoad;
            _isLoading = true;
            Debug.Log("LoadMenu");

            UnloadPreviousScene();
        }

        private void LoadGameMap(GameSceneSO gameMap, bool showLoadingScreen, bool fadeScreen)
        {
            Debug.Log($"LoadGameplay {_isLoading}");
            if (_isLoading)
                return;

            if (_sceneToLoad == gameMap)
                notUnloadScene = true;

            _sceneToLoad = gameMap;
            _isLoading = true;
           

            UnloadPreviousScene();
        }

        private void LoadNewScene()
        {
            notUnloadScene = false;
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
            AsyncOperationHandle<SceneInstance> scene;
            if (_currentlyLoadedScene != null) //would be null if the game was started in Initialisation
            {
                if (_currentlyLoadedScene.sceneReference.OperationHandle.IsValid())
                {
                    //Unload the scene through its AssetReference, i.e. through the Addressable system
                    scene = _currentlyLoadedScene.sceneReference.UnLoadScene();

                    if (notUnloadScene)
                    {
                        scene.Completed += (e) => LoadNewScene();

                        return;
                    }

                }
            }

            LoadNewScene();
        }
    }

}
