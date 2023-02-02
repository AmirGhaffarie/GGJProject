using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

namespace Assets.Scripts.Monkey
{
    public class Monkey : MonoBehaviour
    {

        [SerializeField] private float jumpForce;
        [SerializeField] private LayerMask collisionLayer;

        private Rigidbody2D rigidbody;
        private Collider2D collider;

        private float distToGround;

        // Start is called before the first frame update
        void Awake()
        {
            rigidbody = GetComponent<Rigidbody2D>();
            collider = GetComponent<CapsuleCollider2D>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        void Start()
        {
            // get the distance to ground
            distToGround = collider.bounds.extents.y;
        }

        bool IsGrounded()
        {
            return Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), -Vector2.up, distToGround + 0.1f, collisionLayer);
        }

        public bool Jump()
        {
            var canJump = false;
            if (IsGrounded())
                canJump = true;

            if (!canJump)
                return false;
            rigidbody.velocity += (Vector2.up * jumpForce);
            return true;
        }
    }
}