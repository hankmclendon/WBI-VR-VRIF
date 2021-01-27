using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void LoadMainScene()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadWaitingRoomScene()
    {
        SceneManager.LoadScene(0);
    }
}
