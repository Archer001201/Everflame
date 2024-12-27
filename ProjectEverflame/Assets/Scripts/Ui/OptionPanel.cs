using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Ui
{
    public class OptionPanel : MonoBehaviour
    {
        public GameObject creditPanel;
        
        private void OnEnable()
        {
            Time.timeScale = 0;
        }

        private void OnDisable()
        {
            Time.timeScale = 1;
        }

        public void StartGame()
        {
            SceneManager.LoadSceneAsync("Level");
        }

        public void QuitGame()
        {
            Application.Quit();
        }

        public void Deactive()
        {
            gameObject.SetActive(false);
        }

        public void PlayCredit()
        {
            creditPanel.SetActive(true);
        }
    }
}
