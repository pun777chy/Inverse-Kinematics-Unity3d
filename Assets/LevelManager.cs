using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelManager : MonoBehaviour {
	public GameObject FireButton;
	public GameObject _Player;
	public GameObject GameOverPanel;
	public GameObject GameCompletePanel;
	public GameObject Enemy1;
	public GameObject Enemy2;
	public GameObject Enemy3;
	public GameObject Enemy4;
	// Use this for initialization
	void Start () {
		#if UNITY_EDITOR
		
		FireButton.SetActive(false);
		#else
		FireButton.SetActive(true);
		
		
		#endif
	}
	void LateUpdate()
	{
		if (PlayerControl._IsDead) {
			Time.timeScale = 0;
			GameOverPanel.SetActive(true);

		} else {

			Time.timeScale = 1;
			GameOverPanel.SetActive(false);
			if (SceneManager.GetActiveScene ().buildIndex == 2) {
				if(Enemy1==null&&Enemy2==null&&Enemy3==null&&Enemy4==null)
				{
					Time.timeScale = 0;
					GameCompletePanel.SetActive(true);
				}
			}
		}

		#if !UNITY_EDITOR
		if(PlayerControl._State==Animation_State.dead)
			FireButton.SetActive(false);
		else
			FireButton.SetActive(true);
			#endif
	}
	public void Restart()
	{ 
		
		SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex);
	}
	public void MainMenu()
	{
		SceneManager.LoadScene ("MainMenu");
	}
	

}
