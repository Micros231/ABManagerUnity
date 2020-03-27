using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PanelScene : MonoBehaviour
{
    [SerializeField]
    private Text _textName;
    [SerializeField]
    private string _sceneName;

    public void Initialize(string sceneName)
    {
        _textName.text = sceneName;
        _sceneName = sceneName;
    }

    public void LoadScene()
    {

        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        SceneManager.LoadScene(_sceneName);
    }
}
