using UnityEngine;
using UnityEngine.SceneManagement; 

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private GameInitializer _gameInitializer;

    private async void Start()
    {
        Application.targetFrameRate = 60;
         
        await _gameInitializer.WaitUntilInitializedAsync();
 
        SceneManager.LoadScene("GameScene");
    }
}
