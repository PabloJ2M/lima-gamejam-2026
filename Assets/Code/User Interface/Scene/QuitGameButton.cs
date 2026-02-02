using UnityEngine.UI;

namespace UnityEngine.SceneManagement
{
    [RequireComponent(typeof(Button))]
    public class QuitGameButton : MonoBehaviour
    {
        private void Awake() => GetComponent<Button>().onClick.AddListener(QuitGame);
        private void QuitGame() => Application.Quit();
    }
}