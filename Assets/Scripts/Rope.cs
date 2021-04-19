using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Crab
{
    public class Rope : MonoBehaviour
    {
        [SerializeField] private Weight weight;
        [SerializeField] private Rigidbody2D hook;
        [SerializeField] private GameObject linkPrefab;
        [SerializeField] private int links = 7;
        [SerializeField, Range(0, 1)] private float tightenFactor = 1;
        private List<GameObject> rope = new List<GameObject>();
        private void Start()
        {
            if (weight == null || hook == null || linkPrefab == null) throw new System.Exception(gameObject.name + " can't find something.");

            hook.GetComponent<HingeJoint2D>().connectedAnchor = Vector2.zero;
            GenerateRope();
        }
        private void GenerateRope()
        {
            Rigidbody2D prev = hook;
            float hookDist = Vector2.Distance(transform.position, weight.transform.position);
            float distStep = (hookDist / links) * tightenFactor;

            for (int i = 0; i < links; i++)
            {
                GameObject link = Instantiate(linkPrefab, transform);

                SpringJoint2D joint = link.GetComponent<SpringJoint2D>();
                if (i == 0) joint.connectedAnchor = Vector2.zero;
                joint.connectedBody = prev;
                joint.distance = distStep;
                if (i < links - 1) prev = link.GetComponent<Rigidbody2D>();
                else weight.ConnectRopeEnd(link.GetComponent<Rigidbody2D>());
                rope.Add(link);
            }
        }
        public void CutDelayed(GameObject _link)
        {
            rope.Remove(_link);
            Destroy(_link);
            StartCoroutine(Fade());
        }
        private IEnumerator Fade()
        {
            yield return new WaitForSeconds(0.45f);

            SpriteRenderer linkSprite = rope[rope.Count - 1].GetComponent<SpriteRenderer>();
            Color colour = linkSprite.color;
            while (colour.a > -0.1)
            {
                foreach (GameObject link in rope)
                {
                    SpriteRenderer linkSpriteF = link.GetComponent<SpriteRenderer>();
                    Color colourF = linkSpriteF.color;
                    colourF.a -= 0.0255f;
                    linkSpriteF.color = colourF;
                }
                yield return null;
            }
            StartCoroutine(CutAll());
        }
        private IEnumerator CutAll()
        {
            yield return new WaitForSeconds(0.1f);
            foreach (GameObject link in rope) Destroy(link);
            rope.Clear();
        }
    }
}