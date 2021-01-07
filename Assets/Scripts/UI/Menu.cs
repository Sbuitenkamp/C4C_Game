using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public RawImage Controls;

    private Button StartButton;
    public void Start()
    {
        StartButton = gameObject.GetComponentInChildren<Button>();
        StartButton.onClick.AddListener(StartGame);
    }

    private void StartGame()
    {
        StartCoroutine(GameStarter());
    }

    private IEnumerator GameStarter()
    {
        Controls.gameObject.SetActive(true);
        yield return new WaitForSeconds(8);
        SceneManager.LoadScene(1);
    }
}
