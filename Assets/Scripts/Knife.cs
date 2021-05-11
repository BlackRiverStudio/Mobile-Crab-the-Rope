using BreadAndButter.Mobile;
using UnityEngine;
namespace Crab
{
    public class Knife : MonoBehaviour
    {
        private void Start() => MobileInput.Initialise();

        private void Update()
        {
#if (UNITY_ANDROID || UNITY_IOS) && !UNITY_EDITOR
            SwipeInput.Swipe swipe = MobileInput.GetSwipe(0);
            if (swipe != null)
            {
                RaycastHit2D hit2D = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(swipe.Position), Vector2.zero);
                if (hit2D.collider != null && hit2D.collider.CompareTag("Link")) Destroy(hit2D.collider.gameObject);
            }
#else
            if (Input.GetMouseButton(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                Physics.Raycast(ray.origin, ray.direction, out RaycastHit hit);
                if (hit.collider != null && hit.collider.CompareTag("Link"))
                {
                    Rope rope = hit.collider.GetComponentInParent<Rope>();
                    rope.CutDelayed(hit.collider.gameObject);
                }
            }
#endif
        }
    }
}