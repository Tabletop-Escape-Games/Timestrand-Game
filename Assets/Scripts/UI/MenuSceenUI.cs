using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class MenuController : MonoBehaviour
    {
        public void playGame()
        {
            SceneManager.LoadScene("TimeMachine");
        }
    }
}