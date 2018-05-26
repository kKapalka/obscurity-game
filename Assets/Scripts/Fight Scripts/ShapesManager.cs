using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


public class ShapesManager : MonoBehaviour
{
    public Text ScoreText;
    public ShapesArray shapes;
	public GameObject player,enemy;
	public GameObject enemyData;
    private int score;
	private string turn;
    public Vector2 BottomRight = new Vector2(-2.67f, -2.27f);
    public readonly Vector2 CandySize = new Vector2(0.7f, 0.7f);
	public Text turntag,hintText;
    private GameState state = GameState.None;

    private Vector2[] SpawnPositions;
    public GameObject[] CandyPrefabs;
    public GameObject[] ExplosionPrefabs;
    public GameObject[] BonusPrefabs;

	public GameObject[] MarkerPrefabs;

	public GameObject[] EffectPrefabs;

	public static bool gameOver=false;

	int playerHP, enemyHP;
	public GameObject EndFight;

	public string[] GemTypes;

	public GameState getState(){
		return state;
	}
	void Update(){
		
		if (state == GameState.None && !gameOver) {
			player.GetComponent<PlayerScript> ().RefreshSelection ();
			playerHP= player.GetComponent<CharacterStats> ().currentHP;
			enemyHP = enemy.GetComponent<CharacterStats> ().currentHP;
			if (playerHP <= 0 || enemyHP<=0) {
				gameOver = true;

				EndFight.SetActive (true);
				EndFight.GetComponent<EndFightPanelScript>().EndOfFight (playerHP >= enemyHP);

				DestroyAllCandy ();
                
			}
		}
		if(Input.GetKey(KeyCode.Escape)){
			//onEscape();
			Application.Quit();
		} else if(Input.GetKey(KeyCode.LeftAlt)){
			//onEscape();
		}
	}


	// Use this for initialization
    void Start()
    {
		state = GameState.None;
		gameOver = false;
		GemTypes = new string[CandyPrefabs.Length];
        InitializeTypesOnPrefabShapesAndBonuses();
        InitializeCandyAndSpawnPositions();
		turn = "Player";

		if(EnemySelection.created)
			EnemySelection.Instance.LoadIntoCharacter(enemy);
		//playerData.GetComponent<PlayerDataScript> ().Assign (player.GetComponent<CharacterStats> (), player.GetComponent<PlayerScript> ().getWeapon ());
		enemyData.GetComponent<PlayerDataScript> ().Assign (enemy.GetComponent<CharacterStats> (), enemy.GetComponent<PlayerScript> ().getWeapon (),enemy.GetComponent<PlayerScript>().weaponType);

    }

    /// <summary>
    /// Initialize shapes
    /// </summary>
    private void InitializeTypesOnPrefabShapesAndBonuses()
    {
		int i = 0;
		//just assign the name of the prefab
		foreach (var item in CandyPrefabs)
		{
			item.GetComponent<Shape>().Type = item.name;
			GemTypes [i++] = item.name;
		}
    }

    public void InitializeCandyAndSpawnPositions()
    {
        if (shapes != null)
            DestroyAllCandy();

        shapes = new ShapesArray();
        SpawnPositions = new Vector2[Constants.Columns];

        for (int row = 0; row < Constants.Rows; row++)
        {
            for (int column = 0; column < Constants.Columns; column++)
            {

                GameObject newCandy = GetRandomCandy();
				//check if two previous horizontal are of the same type
                while ((column >= 2 && shapes[row, column - 1].GetComponent<Shape>()
                    .IsSameType(newCandy.GetComponent<Shape>())
                    && shapes[row, column - 2].GetComponent<Shape>().IsSameType(newCandy.GetComponent<Shape>()))
					||
					(row >= 2 && shapes[row - 1, column].GetComponent<Shape>()
						.IsSameType(newCandy.GetComponent<Shape>())
						&& shapes[row - 2, column].GetComponent<Shape>().IsSameType(newCandy.GetComponent<Shape>()))
				)
                {
                    newCandy = GetRandomCandy();
                }

                InstantiateAndPlaceNewCandy(row, column, newCandy);

            }
        }

        SetupSpawnPositions();
    }



    private void InstantiateAndPlaceNewCandy(int row, int column, GameObject newCandy)
    {
        GameObject go = Instantiate(newCandy,
			BottomRight + new Vector2((column * CandySize.x), ((row * CandySize.y))), Quaternion.identity)
            as GameObject;
		go.transform.parent = transform;
        //assign the specific properties
        go.GetComponent<Shape>().Assign(newCandy.GetComponent<Shape>().Type, row, column);
        shapes[row, column] = go;
    }

    private void SetupSpawnPositions()
    {
        //create the spawn positions for the new shapes (will pop from the 'ceiling')
        for (int column = 0; column < Constants.Columns; column++)
        {
            SpawnPositions[column] = BottomRight
				+ new Vector2((column * CandySize.x), (Constants.Rows * CandySize.y));
        }
    }



    private void DestroyAllCandy()
    {
        for (int row = 0; row < Constants.Rows; row++)
        {
            for (int column = 0; column < Constants.Columns; column++)
            {
                Destroy(shapes[row, column]);
            }
        }
    }
   
    public void FixSortingLayer(GameObject hitGo, GameObject hitGo2)
    {
        SpriteRenderer sp1 = hitGo.GetComponent<SpriteRenderer>();
        SpriteRenderer sp2 = hitGo2.GetComponent<SpriteRenderer>();
        if (sp1.sortingOrder <= sp2.sortingOrder)
        {
            sp1.sortingOrder = 3;
            sp2.sortingOrder = 2;
        }
    }
		
    /// <summary>
    /// Spawns new candy in columns that have missing ones
    /// </summary>
    /// <param name="columnsWithMissingCandy"></param>
    /// <returns>Info about new candies created</returns>
    private AlteredCandyInfo CreateNewCandyInSpecificColumns(IEnumerable<int> columnsWithMissingCandy)
    {
        AlteredCandyInfo newCandyInfo = new AlteredCandyInfo();

        //find how many null values the column has
        foreach (int column in columnsWithMissingCandy)
        {
            var emptyItems = shapes.GetEmptyItemsOnColumn(column);
            foreach (var item in emptyItems)
            {
                var go = GetRandomCandy();
                GameObject newCandy = Instantiate(go, SpawnPositions[column], Quaternion.identity)
                    as GameObject;
				newCandy.transform.parent = transform;
                newCandy.GetComponent<Shape>().Assign(go.GetComponent<Shape>().Type, item.Row, item.Column);

                if (Constants.Rows - item.Row > newCandyInfo.MaxDistance)
                    newCandyInfo.MaxDistance = Constants.Rows - item.Row;

                shapes[item.Row, item.Column] = newCandy;
                newCandyInfo.AddCandy(newCandy);
            }
        }

        return newCandyInfo;
    }

    /// <summary>
    /// Animates gameobjects to their new position
    /// </summary>
    /// <param name="movedGameObjects"></param>
    private void MoveAndAnimate(IEnumerable<GameObject> movedGameObjects, int distance)
    {
        foreach (var item in movedGameObjects)
        {
            item.transform.positionTo(Constants.MoveAnimationMinDuration * distance, BottomRight +
                new Vector2(item.GetComponent<Shape>().Column * CandySize.x, item.GetComponent<Shape>().Row * CandySize.y));
        }
    }

    /// <summary>
    /// Destroys the item from the scene and instantiates a new explosion gameobject
    /// </summary>
    /// <param name="item"></param>
    private void RemoveFromScene(GameObject item)
    {
        GameObject explosion = GetRandomExplosion();
        var newExplosion = Instantiate(explosion, item.transform.position, Quaternion.identity) as GameObject;
        Destroy(newExplosion, Constants.ExplosionDuration);
        Destroy(item);
    }

    /// <summary>
    /// Get a random candy
    /// </summary>
    /// <returns></returns>
    private GameObject GetRandomCandy()
    {
        return CandyPrefabs[UnityEngine.Random.Range(0, CandyPrefabs.Length)];
    }

    
    /// <summary>
    /// Get a random explosion
    /// </summary>
    /// <returns></returns>
    private GameObject GetRandomExplosion()
    {
        return ExplosionPrefabs[UnityEngine.Random.Range(0, ExplosionPrefabs.Length)];
    }
		
	//This function
	//Breaks all matched shapes
	//And triggers gravity
	public IEnumerator BreakMatchesAndGravity(IEnumerable<GameObject> totalMatches){

		int sequence = 1;
		while (totalMatches.Count () >= Constants.MinimumMatches) {

			int[] matches = new int[CandyPrefabs.Length];
			for (int i = 0; i < matches.Length; i++) {
				matches [i] = 0;
			}
			foreach (GameObject match in totalMatches) {
				matches [System.Array.IndexOf (GemTypes, match.GetComponent<Shape>().Type)]++;
			}
			for (int i = 0; i < CandyPrefabs.Length; i++) {
				if (turn == "Player")
					player.GetComponent<PlayerScript>().DealDamage (matches [i], i, enemy, sequence);
				else
					enemy.GetComponent<PlayerScript>().DealDamage (matches [i], i, player, sequence);
			}
			foreach (var item in totalMatches) {
				shapes.Remove (item);
				RemoveFromScene (item);
			}
			var columns = totalMatches.Select(go => go.GetComponent<Shape>().Column).Distinct();

			//the order the 2 methods below get called is important!!!
			//collapse the ones gone
			var collapsedCandyInfo = shapes.Collapse(columns);
			//create new ones
			var newCandyInfo = CreateNewCandyInSpecificColumns(columns);

			int maxDistance = Mathf.Max(collapsedCandyInfo.MaxDistance, newCandyInfo.MaxDistance);

			MoveAndAnimate(newCandyInfo.AlteredCandy, maxDistance);
			MoveAndAnimate(collapsedCandyInfo.AlteredCandy, maxDistance);


			yield return new WaitForSeconds(Constants.MoveAnimationMinDuration * maxDistance);

			totalMatches = shapes.GetMatches(collapsedCandyInfo.AlteredCandy).
				Union(shapes.GetMatches(newCandyInfo.AlteredCandy)).Distinct();
			
			sequence++;
		}
		if (sequence > 4 && turn == "Player")
			player.GetComponent<EquipmentManager> ().AddModifiersOfType ("combo");
	}

	public void setState(GameState state){
		this.state=state;
	}
	public void processEndOfTurn(){
		
		StartCoroutine(WaitForEnemyAttack());
		state=GameState.None;
		if (turn == "Enemy") {
			turn = "Player";
			player.GetComponent<EquipmentManager> ().AddModifiersOfType ("startofturn");
			if(playerHP>=0)
				player.GetComponent<CharacterStats> ().Regenerate ();
		}
		else if (turn == "Player") {
			turn = "Enemy";
			if(enemyHP>=0)
				enemy.GetComponent<CharacterStats> ().Regenerate ();
		}
		turntag.text = turn + " Turn";
	}
	private IEnumerator WaitForEnemyAttack(){
		while(state!=GameState.None)
			yield return new WaitForSeconds (1.0f);
	}

	public void Restart(){
		state = GameState.None;
		player.GetComponent<CharacterStats> ().ResetHP ();
		enemy.GetComponent<CharacterStats> ().ResetHP ();
		turn = "Player";
		turntag.text = turn + " Turn";
	}
	public void ClearMarkes(){
		var markers = player.GetComponent<PlayerScript> ().getWeapon ().getMarkers();
		foreach (GameObject marker in markers) {
			Destroy (marker);
		}
		markers.Clear ();
		markers = enemy.GetComponent<PlayerScript> ().getWeapon ().getMarkers();
		foreach (GameObject marker in markers) {
			Destroy (marker);
		}
		markers.Clear ();
	}

	/*	void onEscape(){
		string fullText="";
			for(int i=0;i<damageData.Length;i++){
			for (int j = 0; j < damageData [0].Length; j++) {
				for (int k = 0; k < damageData [0] [0].Length; k++) {
					PlayerPrefs.SetInt (attackTypes [i] + attackData [j]+attacker[k], damageData [i] [j][k]);
					fullText += string.Format ("{0}{1}{2} = {3}{4}", attackTypes [i], attackData [j],attacker[k], damageData [i] [j][k], Environment.NewLine);
				}
			}
			}
		System.Text.UnicodeEncoding encode = new System.Text.UnicodeEncoding ();
		byte[] data = encode.GetBytes (fullText);
		System.IO.FileStream file = null;
		file = new System.IO.FileStream ("data.txt", System.IO.FileMode.Create);
		file.Write (data, 0, data.Length);
		file.Close ();
		}

	public void ClearDamageData(){
		for(int i=0;i<damageData.Length;i++){
			for (int j = 0; j < damageData [0].Length; j++) {
				for (int k = 0; k < damageData [0] [0].Length; k++) {
					damageData [i] [j] [k] = 0;
				}
			}
		}
	}*/

	public IEnumerator StartSpriteEffectCoroutine(GameObject go, Vector3 position, Quaternion rotation){
		GameObject go2 = Instantiate (go, position, rotation) as GameObject;
		for (float alpha = 0.7f; alpha > 0.1f; alpha-=0.2f) {
			Color c = go2.GetComponent<SpriteRenderer>().color;
			c.a = alpha;
			go2.GetComponent<SpriteRenderer>().color = c;
			yield return new WaitForSeconds(Constants.OpacityAnimationFrameDelay);
		}

		Destroy (go2);
	}

	public void RandomizeCharacter(GameObject character){
		CharacterStats stats = character.GetComponent<CharacterStats> ();
		string[] weaponTypes = new string[]{ "basic", "dragndrop", "dragthrough", "hammer", "doubleattack", "scattershot" };
		float temp;
		for (int i = 0; i < stats.resistances.Length; i++) {
			temp = UnityEngine.Random.Range (-70, 100);
			stats.resistances [i].value = Mathf.RoundToInt((temp/(100+temp))*20)*5;
		}
		stats.dodge.value = UnityEngine.Random.Range (0, 6) * 5;
		stats.regeneration.value = UnityEngine.Random.Range (-5, 5) * 5;

		temp = UnityEngine.Random.Range (-70, 100);
		stats.damageMultiplier.value = Mathf.RoundToInt((temp/(100+temp))*20)*-5;
		stats.strength.value = UnityEngine.Random.Range (0, 15);

		character.GetComponent<PlayerScript> ().LoadWeapon (weaponTypes [UnityEngine.Random.Range (0, weaponTypes.Length-1)]);
	}

	public void SaveProgress(){
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file;
		List<string> encountersDefeated = ReadScript.Read<List<string>> ("Progress");
		if (encountersDefeated == default(List<string>))
			encountersDefeated = new List<string> ();
		if(EnemySelection.created)
			encountersDefeated.Add (EnemySelection.Instance.encounterName);
			
		file = File.Create(Application.persistentDataPath + "/Progress.dat");
		bf.Serialize (file,encountersDefeated);

		file.Close ();
	}
}
