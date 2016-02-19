using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PathFinder : MonoBehaviour {
	
	public GameObject mapObject;
	//public GameObject layerObject; //@todo: only one layer now
	public GameObject targetObject;
	public int countX;
	public int countY;
	public float stepX;
	public float stepY;
	public float offsetX;
	public float offsetY;

	public float updateTime = 0.5f;

	IntVec2 prevTileCoords;

	int tmpCounter = 0;

	struct IntVec2
	{
		public IntVec2 (int _x, int _y) {
			x = _x;
			y = _y;
		}  

		public int x;
		public int y;
	}

	struct TileSettings
	{
		public bool isCollide;
		public Vector2 moveVector;
		public float distance;
		public bool isCalculated;
	};

	struct TmpTileParam
	{
		public IntVec2 coords;
		public IntVec2 prevCoords;
		public float distance;
	};

	TileSettings[][] tiles;

	public Vector2 getDirection(Vector3 position)
	{
		IntVec2 tileCoords = getTileCoordsByTransformCoords (position);
		return tiles [tileCoords.x] [tileCoords.y].moveVector;
	}

	// Use this for initialization
	void Start () {

		// init grid
		tiles = new TileSettings[countX][];
		for (int i = 0; i < countX; i++) {
			tiles [i] = new TileSettings[countY];
			for (int j = 0; j < countY; j++) {
				tiles [i] [j] = new TileSettings ();
			}
		}
			
		// load map, set isCollide to grid
		foreach (Transform layerObject in mapObject.transform) {
			foreach (Transform child in layerObject) {
				//Debug.Log ("child is found " + child.position.x + ", " + child.position.y);

				PolygonCollider2D coll = child.GetComponent<PolygonCollider2D> ();
				if (null == coll)
					continue;


				IntVec2 coords = getTileCoordsByTransformCoords (child.position);
				Debug.Log ("mark as collided: " + coords.x + ", " + coords.y);
				tiles [coords.x] [coords.y].isCollide = true;
			}
		}

		InvokeRepeating ("UpdatePathFinder", 0, updateTime);

	}

	IntVec2 getTileCoordsByTransformCoords(Vector3 coords)
	{
		int x = Mathf.FloorToInt ((coords.x - offsetX) / stepX);
		int y = Mathf.FloorToInt ((coords.y - offsetY) / stepY);
		return new IntVec2 (x, y);
	}


	void UpdatePathFinder()
	{
		if (!targetObject)
			return;

		IntVec2 targetTileCoords = getTileCoordsByTransformCoords (targetObject.transform.position);
		if ((targetTileCoords.x == prevTileCoords.x) &&
		   (targetTileCoords.y == prevTileCoords.y))
			return;

		RecalculateTarget (targetTileCoords);
		prevTileCoords.x = targetTileCoords.x;
		prevTileCoords.y = targetTileCoords.y;
	}

	void RecalculateTarget(IntVec2 targetTileCoords)
	{
		// clear tiles
		for (int i = 0; i < countX; i++) {			
			for (int j = 0; j < countY; j++) {				
				tiles [i] [j].moveVector.x = 0;
				tiles [i] [j].moveVector.y = 0;
				tiles [i] [j].distance = 0.0f;
				tiles [i] [j].isCalculated = false;
			}
		}

		tmpCounter = 0;

		List<TmpTileParam> openCells = new List<TmpTileParam> ();

		TmpTileParam param;
		param.prevCoords = targetTileCoords;
		param.coords = targetTileCoords;
		param.distance = 0;
		openCells.Add (param);

		Debug.Log ("RecalculateTarget " + targetTileCoords.x + ", " + targetTileCoords.y);

		while (openCells.Count > 0) {
			TmpTileParam tmp = openCells [0];
			CalculateTile (tmp.prevCoords, tmp.coords, tmp.distance, ref openCells);
			openCells.RemoveAt (0);
		}

		Debug.Log ("tmpCounter " + tmpCounter);
	}

	void CalculateTile(IntVec2 prevTileCoords, IntVec2 tileCoords, float distance, ref List<TmpTileParam> openCells)
	{
		//if (distance > 10)
		//	return;

		//TileSettings tile = tiles [tileCoords.x] [tileCoords.y];
		//TileSettings * tmp = &(tiles [tileCoords.x] [tileCoords.y]);
		if (tiles [tileCoords.x] [tileCoords.y].isCalculated)
			return;


		tmpCounter++;

		tiles [tileCoords.x] [tileCoords.y].isCalculated = true;

		if (tiles [tileCoords.x] [tileCoords.y].isCollide) {
			return;
		}

		tiles [tileCoords.x] [tileCoords.y].moveVector.x = prevTileCoords.x - tileCoords.x;
		tiles [tileCoords.x] [tileCoords.y].moveVector.y = prevTileCoords.y - tileCoords.y;
		//@todo: use distance!

		//Debug.Log (tmpCounter + ". CalculateTile " + tileCoords.x + ", " + tileCoords.y);
		if (tileCoords.x > 0) {
			
			IntVec2 left;
			left.x = tileCoords.x - 1;
			left.y = tileCoords.y;

			TmpTileParam param;
			param.prevCoords = tileCoords;
			param.coords = left;
			param.distance = distance + 1;

			openCells.Add (param);
		}
		if (tileCoords.x < (countX - 1)) {
			IntVec2 right;
			right.x = tileCoords.x + 1;
			right.y = tileCoords.y;	

			TmpTileParam param;
			param.prevCoords = tileCoords;
			param.coords = right;
			param.distance = distance + 1;

			openCells.Add (param);
		}
		if (tileCoords.y > 0) {
			IntVec2 down;
			down.x = tileCoords.x;
			down.y = tileCoords.y - 1;	

			TmpTileParam param;
			param.prevCoords = tileCoords;
			param.coords = down;
			param.distance = distance + 1;

			openCells.Add (param);
		}
		if (tileCoords.y < (countY - 1)) {
			IntVec2 up;
			up.x = tileCoords.x;
			up.y = tileCoords.y + 1;	

			TmpTileParam param;
			param.prevCoords = tileCoords;
			param.coords = up;
			param.distance = distance + 1;

			openCells.Add (param);
		}
	}
}
