using UnityEngine;
using Button = UnityEngine.UI.Button;
namespace Crab
{
    public class Weight : MonoBehaviour
    {
        [SerializeField] private Button[] o = new Button[3];
        [SerializeField] private Goal goal;
        private float conAncY = 4.5f;
        private int index = 0;
        private void Start()
        {
            index = 0;
            foreach (Button button in o) button.interactable = false;
        }
        private void OnTriggerEnter(Collider _collider)
        {
            if (_collider.gameObject.CompareTag("Collectable"))
            {
                o[index].interactable = true;
                index++;
                Destroy(_collider.gameObject);
            }
            if (_collider.gameObject.CompareTag("Goal")) goal.Win();
            if (_collider.gameObject.CompareTag("Side")) goal.Loss();
        }
        public void ConnectRopeEnd(Rigidbody _ropeEnd, float _dist)
        {
            HingeJoint joint = gameObject.AddComponent<HingeJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedBody = _ropeEnd;
            joint.anchor = Vector2.zero;
            joint.connectedAnchor = new Vector2(0f, 0f);
            //joint.maxDistance = _dist;
            //joint.spring = 10000;
        }
    }
}