using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ChangeScene : SimulationBehaviour, INetworkSceneManager
{
    // Start is called before the first frame update
    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
        
    }

    public void Initialize(NetworkRunner runner)
    {
        
    }

    public void Shutdown(NetworkRunner runner)
    {
      
    }

    public bool IsReady(NetworkRunner runner)
    {
        throw new System.NotImplementedException();
    }
}
