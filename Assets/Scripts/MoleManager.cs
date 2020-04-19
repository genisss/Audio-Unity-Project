using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoleManager : MonoBehaviour {

	List<MoleController> moles = new List<MoleController>();
	bool generate;
	public AnimationCurve maxMoles;
    PositionSpawner positionSpawner;
    public GameObject molePrefab;

    // Use this for initialization
    void Start () 
	{
        positionSpawner = new PositionSpawner();

		this.generate = false;
	}

    // Start is called before the first frame update

    public void SpawnMoles(int num)
    {
        for (int i = 0; i < num; i++)
        {
            Vector3 pos = positionSpawner.generateNewHolePos(0, 180, 30f, 100f);
            Quaternion rot = Quaternion.LookRotation(-pos, Vector3.up);
            GameObject go = GameObject.Instantiate(molePrefab);
            go.transform.parent = this.transform;
            go.transform.localPosition = new Vector3(pos.x, 0,pos.z);
            go.transform.localRotation = rot;
            moles.Add(go.transform.Find("mole").GetComponent<MoleController>());
            

        }
    }

    public void StartGenerate()
	{
		StartCoroutine ("Generate");
	}

	public void StopGenerate()
	{
		this.generate = false;
	}
		
	IEnumerator Generate()
	{
		this.generate = true;

		while (this.generate) 
		{
			// wait to generate next group
			yield return new WaitForSeconds (1.0f);

			int n = moles.Count;
			int maxNum = 100 *(int)this.maxMoles.Evaluate ( GameManager.time );

			for (int i = 0; i < maxNum; i++) 
			{
				// select mole to up
				this.moles [Random.Range (0, n)].Up ();
								
				yield return new WaitForSeconds (0.3f);
			}
		}
	}

    public void ExplodeAllMoles()
    {
        foreach (MoleController mole in moles){
            mole.Explode();
        }
    }
}
