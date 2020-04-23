using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main_menu : MonoBehaviour
{
    public void Load_Learn_about_instruments()
    {
        SceneManager.LoadScene("Learn_about_instruments");
    }
    public void Load_parts_of_instrument()
    {
        SceneManager.LoadScene("parts_of_violin");
    }
    public void Load_how_to_hold()
    {
        SceneManager.LoadScene("how_to_hold_violin");
    }
    public void Load_hear_the_violin()
    {
        SceneManager.LoadScene("hear_the_violin");
    }
    public void Load_learn_about_violin()
    {
        SceneManager.LoadScene("learn_about_violin");
    }
    public void Load_play_the_instrument()
    {
        SceneManager.LoadScene("play_the_violin");
    }

    public void Load_Accuracy_Note_Game()
    {
        SceneManager.LoadScene("accuracy_based_note_game");
    }

    public void QuitGame()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }
}
