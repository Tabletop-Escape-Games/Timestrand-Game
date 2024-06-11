using UnityEngine;
using UnityEngine.SceneManagement;

namespace Controllers
{
    public class MenuController : MonoBehaviour
    {
        public void playGame()
        {
            SceneManager.LoadScene("TimeMachine");
        }
    }
}