using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Villager : MonoBehaviour 
{

	public enum Job
	{
		Villager,
		WoodCutter
	}

	public enum State
	{
		Idle,
		Walking,
		Felling,
		Chopping
	}

	[SerializeField]
	private Job m_Job;

	[SerializeField]
	private State m_State;

	[SerializeField, Range( 0, 1 )]
	private float m_WorkEthic;

	[SerializeField, Range( 0, 1 )]
	private float m_Faith = 0;

	private NavMeshAgent m_Agent;
	public NavMeshAgent Agent
	{
		get
		{
			if ( !m_Agent )
			{
				m_Agent = GetComponentInChildren<NavMeshAgent>();
			}
			return m_Agent;
		}
		set
		{
			m_Agent = value;
		}
	}


	private float m_IdleTimeout;
	private float m_IdleTimer;


	private void Update ()
	{
		switch ( m_State )
		{
			case State.Idle:
				m_IdleTimer += Time.deltaTime;
				if ( m_IdleTimer >= m_IdleTimeout )
				{
					m_IdleTimeout = 10 - Random.Range( m_WorkEthic * 5, m_WorkEthic * 10 );
					m_IdleTimer = 0;
					DoJob();
				}
				break;
			case State.Walking:
				if ( Vector3.Distance( transform.position, Agent.destination ) <= Agent.stoppingDistance )
				{
					ChangeState( State.Idle );
				}
				break;
			case State.Felling:
				break;
			case State.Chopping:
				break;
		}
	}


	private void ChangeState ( State state )
	{
		switch ( state )
		{
			case State.Idle:
				break;
			case State.Walking:
				break;
			case State.Felling:
				break;
			case State.Chopping:
				break;
		}

		m_State = state;
	}

	private void DoJob ()
	{
		switch ( m_Job )
		{
			case Job.WoodCutter:
				FindTree();
				break;
			case Job.Villager:
				break;
		}
	}



	[RuntimeButton]
	private void FindTree ()
	{
		Tree[] trees = FindObjectsOfType<Tree>();
		if ( trees == null || trees.Length <= 0 ) return;

		Tree closestTree = trees[0];
		float distanceToClosestTree = Vector3.Distance( transform.position, trees[0].transform.position );
		for ( int i = 0; i < trees.Length; i++ )
		{
			float distance = Vector3.Distance( transform.position, trees[i].transform.position );
			if ( distance < distanceToClosestTree )
			{
				closestTree = trees[i];
				distanceToClosestTree = distance;
			}
		}

		Agent.SetDestination( closestTree.transform.position );
		ChangeState( State.Walking );
	}



}
