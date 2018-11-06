using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboController : MonoBehaviour
{
    private Animator m_animator;

    private int m_numOfClicks;
    private bool m_canClick;

	// Use this for initialization
	void Start ()
    {
        m_animator = GetComponent<Animator>();

        m_numOfClicks = 0;
        m_canClick = true;
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(Input.GetKeyDown(KeyCode.X))
        {
            ComboStarter();
        }
	}

    private void ComboStarter()
    {
        if(m_canClick)
        {
            m_numOfClicks++;
        }

        if(m_numOfClicks == 1)
        {
            m_animator.SetInteger("animation", 1);
        }
    }

    // This is function is called whenever there is an animation event occurs
    public void ComboCheck()
    {
        m_canClick = false;

        if(m_animator.GetCurrentAnimatorStateInfo(0).IsName("Slash 1") && m_numOfClicks == 1)
        {
            // If the first animation is still playing and only 1 click has happened, return to idle
            m_animator.SetInteger("animation", 0);
            m_canClick = true;
            m_numOfClicks = 0;
        }

        else if(m_animator.GetCurrentAnimatorStateInfo(0).IsName("Slash 1") && m_numOfClicks >= 2)
        {
            // If the first animation is still playing and at least 2 clicks have happened, continue the combo
            m_animator.SetInteger("animation", 2);
            m_canClick = true;
        }

        else if(m_animator.GetCurrentAnimatorStateInfo(0).IsName("Slash 2") && m_numOfClicks == 2)
        {
            // If the second animation is still playing and only 2 clicks have happened, return to idle
            m_animator.SetInteger("animation", 0);
            m_canClick = true;
            m_numOfClicks = 0;
        }

        else if(m_animator.GetCurrentAnimatorStateInfo(0).IsName("Slash 2") && m_numOfClicks >= 3)
        {
            // If the animation is still playing and at least 3 clicks have happened, continue the combo
            m_animator.SetInteger("animation", 3);
            m_canClick = true;
        }

        else if(m_animator.GetCurrentAnimatorStateInfo(0).IsName("Thrust 3"))
        {
            // Since this is the third and last animation, return to idle
            m_animator.SetInteger("animation", 0);
            m_canClick = true;
            m_numOfClicks = 0;
        }
    }
}
