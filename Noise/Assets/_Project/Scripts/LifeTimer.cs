using UnityEngine;
using System.Collections;

public class LifeTimer : MonoBehaviour 
{
	public delegate void OnDeathEvent();
	public event OnDeathEvent OnDeath;

	public float m_LifeDuration = 10;
	float m_Timer = 0;
	public float NormalizedLifetime { get{ return m_Timer/ m_LifeDuration; }}
	bool m_Alive = true;

	public bool m_DestroyOnEndLife = false;

	void Update () 
	{
		if( m_Alive )
			m_Timer += Time.deltaTime;

		if( m_Timer >= m_LifeDuration )
		{
			m_Alive = false;
			m_Timer = m_LifeDuration;

			if( OnDeath != null ) OnDeath();

            if (m_DestroyOnEndLife) Destroy(gameObject);
            else
                gameObject.SetActive(false);
		}
	}

    void Reset()
    {
        m_Timer = 0;
        m_Alive = true;
    }

    void OnDisable()
    {
        Reset();
    }
}
