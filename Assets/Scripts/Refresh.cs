using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Refresh : MonoBehaviour
{
    public void RefreshScene()
    {
        SceneManager.LoadScene("FocusCircle");
    }
}
