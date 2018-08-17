using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StartGame : MonoBehaviour {

    public void ChangeScene(string scene)
    {
        Application.LoadLevel(scene);
    }
}
