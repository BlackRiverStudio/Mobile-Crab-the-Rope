using System.Collections.Generic;
using System.Collections;
using UnityEngine;
namespace Crab
{
    public class Rope : MonoBehaviour
    {
        [SerializeField] private Weight weight;
        [SerializeField] private Rigidbody2D hook;
        [SerializeField] private GameObject linkPrefab;
        [SerializeField] private int links = 7;
        private List<GameObject> rope = new List<GameObject>();
        private void Start()
        {
            if (weight == null || hook == null || linkPrefab == null)
            {
                Debug.LogError(gameObject.name + " can't find something.");
                return;
            }
            GenerateRope();
        }
        private void GenerateRope()
        {
            Rigidbody2D prev = hook;
            for (int i = 0; i < links; i++)
            {
                GameObject link = Instantiate(linkPrefab, transform);
                HingeJoint2D joint = link.GetComponent<HingeJoint2D>();
                joint.connectedBody = prev;
                if (i < links - 1) prev = link.GetComponent<Rigidbody2D>();
                else weight.ConnectRopeEnd(link.GetComponent<Rigidbody2D>());
                rope.Add(link);
            }
        }
        public void CutDelayed(GameObject _object)
        {
            rope.Remove(_object);
            Destroy(_object);
            StartCoroutine(CutAll());
        }
        private IEnumerator CutAll()
        {
            yield return new WaitForSeconds(0.5f);
            foreach (GameObject link in rope) Destroy(link);
            rope.Clear();
        }
    }
}