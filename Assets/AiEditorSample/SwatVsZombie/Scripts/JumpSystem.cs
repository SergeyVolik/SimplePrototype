using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SerV112.UtilityAI.Game
{
    [RequireComponent(typeof(Rigidbody))]
    public class JumpSystem : MonoBehaviour
    {
        [SerializeField]
        private float m_JumpForce;
        private bool m_Grounded;

        [SerializeField]
        private float m_CharacterHeight = 2.5f;
        Rigidbody Rigidbody;

        [SerializeField]
        LayerMask GroudMask;

        // Start is called before the first frame update
        private void Start()
        {
            Rigidbody = GetComponent<Rigidbody>();
        }

        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                print("key pressed!");
                m_Grounded = Physics.Raycast(new Vector2(transform.position.x, transform.position.y + 1f), Vector3.down, m_CharacterHeight, GroudMask);
                
               // raycast down to look for ground is not detecting ground? only works if allowing jump when grounded = false; // return "Ground" layer as layer
                Debug.DrawRay((new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z)), Vector3.down* m_CharacterHeight, Color.green, 5);

                if (m_Grounded == true)
                {
                    print("grounded!");
                    Rigidbody.AddForce(Vector3.up* m_JumpForce);

                }
                else if (m_Grounded == false)
                {
                    print("Can't Jump - Not Grounded");
                }
            }
        }

    }

}
