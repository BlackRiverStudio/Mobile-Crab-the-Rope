using UnityEngine;
using Button = UnityEngine.UI.Button;
namespace Crab
{
    [RequireComponent(typeof(Collider2D))]
    public class Goal : MonoBehaviour
    {
        [SerializeField] private Weight weight;
        [SerializeField] private GameObject winPanel;
        [SerializeField] private Button nextButton;
        private void Start()
        {
            winPanel.SetActive(false);
            nextButton.gameObject.SetActive(true);
            nextButton.interactable = false;
        }
        public void Win()
        {
            Destroy(weight.gameObject);
            winPanel.SetActive(transform);
            nextButton.interactable = true;
        }
        public void Loss()
        {
            Destroy(weight.gameObject);
            //StartCoroutine("Wait");
            winPanel.SetActive(true);
        }
        //IEnumerator Wait() => WaitForSeconds(0.5f);
    }
}