using UnityEngine;
using UnityEngine.SceneManagement; 

//Loads the different scenes in the game

public class ScenesManager : MonoBehaviour
{
 
    public void LoadLevel0()
    {
        SceneManager.LoadScene("Level 0");
    }

    
    public void LoadLevel1()
    {
        SceneManager.LoadScene("Level 1");
    }

    
    public void LoadLevel2()
    {
        SceneManager.LoadScene("Level 2");
    }

    
    public void LoadLooser()
    {
        SceneManager.LoadScene("Looser");
    }

    
    public void LoadWinner()
    {
        SceneManager.LoadScene("Winner");
    }
}
