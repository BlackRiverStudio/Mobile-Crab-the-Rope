using UnityEngine;
namespace Crab
{
    public class Pause : MonoBehaviour
    {
        [SerializeField] private GameObject pauseMenu;
        // private void Awake() => DontDestroyOnLoad(this);
        private void Start() => UnPaused();
        private void Paused()
        {
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
        }
        private void UnPaused()
        {
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
        }
        public void TogglePause(bool _toggle)
        {
            _toggle = !_toggle;
            if (_toggle) Paused();
            else UnPaused();
        }
    }
}