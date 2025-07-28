using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data : MonoBehaviour
{

    public Rigidbody2D m_Rigidbody;
    public bool m_bisJumping = false;
    public bool m_bisToLeft = true;

    public float m_maxChargingTime = 2.0f;
    public float m_curChargingTime = 0.0f;

    public RectTransform m_rectTransform;

    // Start is called before the first frame update
   
    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.A))
        {
            m_Rigidbody.velocity = new Vector2(-5.0f, m_Rigidbody.velocity.y);
            m_bisToLeft = true;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            m_Rigidbody.velocity = new Vector2(5.0f, m_Rigidbody.velocity.y);
            m_bisToLeft = false;
        }

        if (Input.GetKey(KeyCode.Space) && !m_bisJumping)
        {
            m_Rigidbody.velocity = Vector3.zero;

            m_curChargingTime += Time.deltaTime;
            if(m_curChargingTime > m_maxChargingTime)
            {
                m_curChargingTime = m_maxChargingTime;
            }
        }

        if (Input.GetKeyUp(KeyCode.Space) && !m_bisToLeft)
        {
            Debug.Log("Jumping with charge time: " + m_curChargingTime);

            m_Rigidbody.AddForce(new Vector2((m_bisToLeft ? -5.0f : 5.0f) * (m_curChargingTime / m_maxChargingTime), 10.0f * (m_curChargingTime / m_maxChargingTime)), ForceMode2D.Impulse);
            m_bisJumping = true;

            m_curChargingTime = 0.0f;
        }

        m_rectTransform.localScale = new Vector3(m_curChargingTime / m_maxChargingTime, 1.0f, 1.0f);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        m_bisJumping = false ;
    }
}
