using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.EventSystem;
using NavigationEvents;
using UnityEngine;
using UnityEngine.UI;

public class MainView : View {

    [Header("Cloud Generation")]
    public GameObject[] CloudVariations;
    public RectTransform SpawnOrigin;
    [Range(0,1)]
    public float SpawnVariation;
    public float SpawnFrequency;
    [Range(0, 1)]
    public float SpawnFrequencyVariation;
    public float MoveSpeed;
    [Range(0, 1)]
    public float MoveSpeedVariation;
    [Range(0, 1)]
    public float SizeVariation;


    public GameObject Content;

    public Button testButton;

    private float _cloudTimer=0;
    private float _spawnFrequency;
    private GameObject _cloudContainer;


	// Use this for initialization
	void Start () {

        _spawnFrequency = SpawnFrequency;
        _cloudContainer = new GameObject("[Cloud_Container]");
        _cloudContainer.transform.SetParent(Content.transform);


        
        var LoadScreenEvent = new LoadScreenEvent(new LoadScreenArgs("DUNGEON"));
        testButton.onClick.AddListener(delegate
        {
            EventSystem.Instance.Dispatch(LoadScreenEvent);
        });
    }
	
	// Update is called once per frame
	void Update () {
        _cloudTimer += Time.deltaTime;
        if (_cloudTimer >= _spawnFrequency)
            SpawnCloud();
	}

    private void SpawnCloud()
    {
        var spawnPos = SpawnOrigin.position;
        var spawnVariation = SpawnVariation * Screen.currentResolution.height;
        spawnPos.y -= Random.Range(0, spawnVariation);

        var newCloud = Instantiate(CloudVariations[Random.Range(0, CloudVariations.Length)],spawnPos,SpawnOrigin.rotation);
        newCloud.transform.SetParent(_cloudContainer.transform);
        newCloud.transform.localScale *= 1.0f + (1 * (Random.Range(1, SizeVariation * 10)/10));
        newCloud.GetComponent<Cloud>().Speed = MoveSpeed * (1.0f + MoveSpeedVariation);

        _spawnFrequency = SpawnFrequency + (SpawnFrequency * (Random.Range(1, SpawnFrequencyVariation*10)/10));

        _cloudTimer = 0;
    }
}
