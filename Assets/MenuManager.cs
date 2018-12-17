using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuManager : MonoBehaviour {
	public GameObject MainMenu;
	public GameObject LevelSelection;
	public GameObject LoadingScreen;
	// Use this for initialization
	void Start () {
	
	}
	
	public void OnStartButton()
	{
		MainMenu.SetActive (false);
		LevelSelection.SetActive (true);
	}
	public void OnBackButton()
	{
		MainMenu.SetActive (true);
		LevelSelection.SetActive (false);
	}
	public void SelectLevelInfinity()
	{
		LoadingScreen.SetActive (true);		
		LevelSelection.SetActive (false);

		SceneManager.LoadScene ("InfiniteEnemiesEvaluationScene");
	}
	public void SelectLevelHeli()
	{
		LoadingScreen.SetActive (true);
		LevelSelection.SetActive (false);
		SceneManager.LoadScene ("HeliEnemiesEvaluationScene");
	}
}
