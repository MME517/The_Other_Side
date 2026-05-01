using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
using static System.Net.Mime.MediaTypeNames;

public class UIManager : MonoBehaviour
{
	[Header("Lobby Canvases")]
	public GameObject mainMenuCanvas;
	public GameObject roomSelectCanvas;

	public void LoadAncientRoom()
	{
		SceneManager.LoadScene("AncientRoom");
	}

	public void LoadArcadeLobby()
	{
		SceneManager.LoadScene("ArcadeLobby");
	}

	public void ExitGame()
	{
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
		Application.Quit();
#endif
	}

	public void OpenRoomSelect()
	{
		if (mainMenuCanvas != null) mainMenuCanvas.SetActive(false);
		if (roomSelectCanvas != null) roomSelectCanvas.SetActive(true);
	}

	public void CloseRoomSelect()
	{
		if (roomSelectCanvas != null) roomSelectCanvas.SetActive(false);
		if (mainMenuCanvas != null) mainMenuCanvas.SetActive(true);
	}

	public void RestartRoom()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
}
