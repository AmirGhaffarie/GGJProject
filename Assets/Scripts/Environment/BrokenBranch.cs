using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Assets.Scripts.Environment
{
    [RequireComponent(typeof(Collider2D))]
    public class BrokenBranch : MonoBehaviour
    {

        [SerializeField] private float durability = 3;

        private float stepOnTime = -1;
        private Collider2D collider;

        void Awake()
        {
            collider = GetComponentInChildren<Collider2D>();
        }

        public void StepOn()
        {
            stepOnTime = Time.time;
        }

        // Update is called once per frame
        void Update()
        {
            if (stepOnTime < 0)
                return;

            if (Time.time > stepOnTime + durability)
            {
                collider.enabled = false;
                GetComponent<Animation>().Play();
                Destroy(gameObject, 1);
            }

    }
    }
}