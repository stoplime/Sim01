using UnityEngine;
using System.Collections;

public class GenerateField : MonoBehaviour {

	public GameObject FloorTile;
	public Vector2 TileSize;

	public GameObject R_GroundRobot;
	public GameObject G_GroundRobot;

	private Vector2 topLeft;

	// Use this for initialization
	void Start () {
		topLeft = TileSize * (-9.5f);

		for (int i = 0; i < 20; i++) {
			for (int j = 0; j < 20; j++) {
				CreateTile(FloorTile, new Vector2 (topLeft.x+(i*TileSize.x), topLeft.y+(j*TileSize.y)), TileSize);
			}
		}
		int numbOfRobots = 10;
		int numbOfRed = numbOfRobots / 2;
		float radius = 1;
		for (int i = 0; i < numbOfRobots; i++) {
			float angle = i*Mathf.PI*2/numbOfRobots;
			Vector2 pos = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle))*radius;

			if (numbOfRed > 0 && i+numbOfRed<10 ) {
				if (Random.value >= 0.5f) {
					numbOfRed--;
					CreateRobot(R_GroundRobot, pos, TileSize, angle, "Red Robot");
				}else{
					CreateRobot(G_GroundRobot, pos, TileSize, angle, "Green Robot");
				}
			}else if(numbOfRed > 0){
				numbOfRed--;
				CreateRobot(R_GroundRobot, pos, TileSize, angle, "Red Robot");
			}else{
				CreateRobot(G_GroundRobot, pos, TileSize, angle, "Green Robot");
			}
		}

	}

	public void CreateTile (GameObject preFabFloor, Vector2 pos, Vector2 size)
	{
		GameObject f = (GameObject)Instantiate (preFabFloor, new Vector3 (pos.x, 0, pos.y), Quaternion.identity);
		f.transform.parent = transform;
		f.transform.localScale = new Vector3 (size.x, 1, size.y) / 10f;
		f.gameObject.name = "Grid Tile";
	}

	public void CreateRobot (GameObject robot, Vector2 pos, Vector2 size, float angle, string name)
	{
		GameObject r = (GameObject)Instantiate (robot, new Vector3 (pos.x, 0.046f, pos.y), Quaternion.identity);
		r.transform.parent = transform;
		r.transform.Rotate (new Vector3 (0, -angle*180/Mathf.PI, 0));
		r.transform.localScale.Scale (new Vector3(size.x, 1, size.y));
		GRMotion m = r.GetComponent<GRMotion> ();
		m.InitialRot = angle;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
