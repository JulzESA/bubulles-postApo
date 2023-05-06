using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

using UnitySteer2D;
using UnitySteer2D.Behaviors;

[RequireComponent(typeof(SteerForPathSimplified2D))]
public class PathFollowingController2D : MonoBehaviour 
{

	SteerForPathSimplified2D _steering;

	[SerializeField] public Transform _pathRoot;

	[SerializeField] bool _followAsSpline;

	public float progress;

	private Rigidbody2D ribody;

	// Use this for initialization
	void Start() 
	{
		ribody = GetComponent<Rigidbody2D>();
		_steering = GetComponent<SteerForPathSimplified2D>();
		_pathRoot = GameObject.Find("PathZero").transform;
		AssignPath();

		
	}

    private void Update()
    {
		progress = _steering.PathPercentTraversed;
		if (progress > 0.8f)
        {
			GetComponent<SteerForWander2D>().enabled = false;
			_steering.enabled = false;
			ribody.mass = 5f;
			ribody.gravityScale = 5f;
        }
    }


    public void AssignPath()
	{
		GetComponent<SteerForWander2D>().enabled = true;
		_steering.enabled = true;
		ribody.mass = 0.0001f;
		ribody.gravityScale = 0.1f;

		// Get a list of points to follow;
		var pathPoints = PathFromRoot(_pathRoot);
        _steering.Path = _followAsSpline ? new SplinePathway(pathPoints, 1) : new Vector2Pathway(pathPoints, 1);
	}

	static List<Vector2> PathFromRoot(Transform root)
	{
		var children = new List<Transform>();
		foreach (Transform t in root)
		{
			if (t != null)
			{
				children.Add(t);
			}
		}
        return children.OrderBy(t => t.gameObject.name).Select(t => new Vector2(t.position.x, t.position.y)).ToList();
	}


}
