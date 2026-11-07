using System;
using Fusion;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DefaultNamespace
{
    public class RVL : MonoBehaviour
    {
        private NetworkRunner networkRunner;

        private void Start()
        {
            networkRunner = gameObject.AddComponent<NetworkRunner>();

            var sceneRef = SceneRef.FromIndex(SceneManager.GetActiveScene().buildIndex);
            NetworkSceneInfo sceneInfo = new NetworkSceneInfo();
            sceneInfo.AddSceneRef(sceneRef, LoadSceneMode.Single);
            StartGameArgs args = new StartGameArgs();
            args.Scene = sceneInfo;
            args.GameMode = GameMode.Shared;
            args.SessionName = "RVL";
            args.GameMode = GameMode.AutoHostOrClient;
            networkRunner.StartGame(args);

        }
    }
}