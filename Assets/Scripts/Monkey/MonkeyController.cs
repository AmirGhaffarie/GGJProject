using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Assets.Scripts.Monkey
{
    [RequireComponent(typeof(Monkey))]
    public class MonkeyController : MonoBehaviour
    {

        private Monkey monkey;

        // Start is called before the first frame update
        void Awake()
        {
            monkey = GetComponent<Monkey>();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                monkey.Jump();
        }
    }
}