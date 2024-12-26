using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Utilities
{
    public class StartGame : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            SceneManager.LoadSceneAsync("Level");
        }
    }
}
