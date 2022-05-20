using game;
using UnityEngine;

namespace com
{
    public class StartGameService : MonoBehaviour
    {
        public static StartGameService instance;

        void Awake()
        {
            instance = this;
        }

        //checkUpdate/assetbundle
        void Start()
        {

        }

        //included in LoginFinishStartGame
        public void OnReEnterPort()
        {
            //Debug.Log("OnReEnterPort");
            //Debug.Log(GameFlowService.instance.windowState);
            GameFlowService.instance.SetWindowState(GameFlowService.WindowState.Main);

            GameFlowService.instance.SetPausedState(GameFlowService.PausedState.Normal);
            //Debug.Log(GameFlowService.instance.windowState);
        }

        //once only
        public void LoginFinishStartGame()
        {
            //Debug.Log("LoginFinishStartGame");
        }
    }
}