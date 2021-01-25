#pragma warning disable IDE1006, CS0108

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Control : MonoBehaviour {

    private const int ALL_ROLL_START = -1 * 90;

    public Missions missions;
    public ShopObjects shopObjects;
    public GameObject[] Pipes;
    public GameObject PlayerCube, PlayerCubeParent;
    public BackGround backGround;

    private Rigidbody rigidbody;
    private Creating creating;
    private Transform mainCamera;
    private List<byte> randomCharacterIndex = new List<byte>();
    [HideInInspector]
    public byte nowRandomCharacterIndex;
    public static float speed = -5;
    private Coroutine turnedCor;
    private static float speedMult = 0;
    private int nowPipeNumber = 0;

    private static Vector3 typeMissionHelperY = Vector3.zero;

    public static float MouseClickPositionsX;
    public static float typeMissionHelper;
    public static bool RuningAfterLose = false;

    public static Vector3 rotateVectorPlayer;

    
    void Awake ()
    {
        PlayerCubeParent = transform.GetChild(0).gameObject;
        creating = GameObject.Find("GameManager").GetComponent<Creating>();
        mainCamera = Camera.main.transform;
        rigidbody = GetComponent<Rigidbody>();

        string _shopHasObjects = Shop.ShopHasObjects;
        int _length = _shopHasObjects.Length;
        for (byte i = 0; i < _length; i++)
            if (_shopHasObjects[i] != 'F')
                randomCharacterIndex.Add(i);
        
        nowRandomCharacterIndex = (byte)Shop.ShopHasObjects.IndexOf('S');
        ChangePlayerCube(shopObjects.Objects[nowRandomCharacterIndex]);

        GameController.mainMenuCause += AfterMainMenu;
        GameController.replayCause += AfterReplay;
        GameController.playingCause += Play;
        GameController.playHereCause += StartSpeedPP;

        typeMissionHelperY.x = transform.right.x;
    }

    public void AddRandomCharacterIndex(byte _index)
    {
        randomCharacterIndex.Add(_index);
    }

    public void ChangePlayerCube(GameObject _prefab)
    {
        Destroy(PlayerCube);
        PlayerCube = Instantiate(_prefab, PlayerCubeParent.transform);
        PlayerStats playerS = PlayerCube.GetComponent<PlayerStats>();
        playerS.SetPlayRotation();
        rotateVectorPlayer = playerS.rotationVector;
        PlayerCube.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
    }

    public void AfterReplay()
    {
        SetRandomCube();
        backGround.Starting(false, false);
        backGround.SetScore(0);
        StopTurnedCor();
        turnedCor = StartCoroutine(ToStartReplay());
        speedMult = 0;
        speed = -5;
        typeMissionHelper = GetDefaultValueFromTypeMission(Missions.typeMission);
    }

    public void AfterMainMenu()
    {
        SetRandomCube();
        backGround.SetScore(0);
        StopTurnedCor();
        turnedCor = StartCoroutine(ToStartMainMenu());
        speedMult = 0;
        speed = -5;
        typeMissionHelper = GetDefaultValueFromTypeMission(Missions.typeMission);
    }

    private void StopTurnedCor()
    {
        if (turnedCor != null)
            StopCoroutine(turnedCor);
    }

    private IEnumerator ToStartReplay()
    {
        yield return StartCoroutine(ToStartMainMenu(true));
        StartCoroutine(speedStart());
    }

    private IEnumerator ToStartMainMenu(bool replay = false)
    {
        RuningAfterLose = true;
        bool _needWhile = true;

        PlayerStats _ps = PlayerCube.GetComponent<PlayerStats>();
        Vector3 Vector360 = new Vector3(0, 360, 0);

        while (_needWhile)
        {
            bool _needRotation, _needPosition;
            if (Mathf.Abs(transform.eulerAngles.y) > 2)
            {
                if (transform.eulerAngles.y <= 180)
                transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, Vector3.zero, 0.2f);
                else
                transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, Vector360, 0.2f);
                _needRotation = true;
            }
            else
            _needRotation = false;

            _ps.SetPlayRotation();
            
            if (transform.position.y < -0.01f)
            {
                transform.position = Vector3.Lerp(transform.position, Vector3.zero, 0.2f);
                mainCamera.position = new Vector3(0, transform.position.y + 3.1f, 0);
                TurnPipes(false);
                /* int nowNum = (int)(transform.position.y - 25) / 50;
                 Pipes[0].transform.position = new Vector3(0, (nowNum - 1) * 50 - 25, 0);
                 Pipes[1].transform.position = new Vector3(0, (nowNum) * 50 - 25, 0);
                 Pipes[2].transform.position = new Vector3(0, (nowNum + 1) * 50 - 25, 0);*/
                _needPosition = true;
            }
            else
                _needPosition = false;
            
            _needWhile = (_needPosition || _needRotation);
            
            yield return 0;
        }

        if (replay)
        {
            GameController.Playing = true;
        }

        transform.position = Vector3.zero;
        RuningAfterLose = false;
    }

    private void Update()
    {
        TurnPipes();
    }

    void FixedUpdate ()
    {
        mainCamera.position =  new Vector3(0, transform.position.y + 3.1f, 0);
        Controling();
	}

    public void Play()
    {
        if(RuningAfterLose)
        {
            transform.position = Vector3.zero;
            RuningAfterLose = false;
        }
        StopTurnedCor();
        speedMult = 0;
        StartCoroutine(speedStart());
    }

    private void StartSpeedPP()
    {
        StartCoroutine(speedPP());
    }

    private IEnumerator speedStart()
    {
        while (speedMult < 1)
        {
            speedMult += 0.01f;
            yield return 0;
        }
    }

    private IEnumerator speedPP()
    {
        while (!GameController.Lose)
        {
            if (!GameController.Playing)
            {
                yield return 0;
                continue;
            }

            for(int i = 0; i < 5; i++)
            yield return 0;

            speed -= 0.01f;
        }
    }

    void Controling()
    {
        if (!GameController.Playing)
        {
            rigidbody.Sleep();
            return;
        }
        rigidbody.velocity = new Vector3(0, speed * speedMult, 0);
#if !UNITY_EDITOR
        if(Input.touchCount > 0)
            if(Input.touches[0].phase == TouchPhase.Began)
#elif UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
#endif
        {
            MouseClickPositionsX = Camera.main.ScreenToViewportPoint(Input.mousePosition).x;
        }

#if !UNITY_EDITOR
        else
#elif UNITY_EDITOR
        else if (Input.GetMouseButton(0))
#endif
        {
            float mouseClickPositionsX = Camera.main.ScreenToViewportPoint(Input.mousePosition).x;
            transform.Rotate(0, (MouseClickPositionsX - mouseClickPositionsX) * 360, 0);
            MouseClickPositionsX = mouseClickPositionsX;
            switch(Missions.typeMission)
            {
                case TypeMission.Ride: typeMissionHelper += Mathf.Abs(transform.right.x - typeMissionHelperY.x) * 90; break;
                case TypeMission.Laps: typeMissionHelper += (transform.right.x - typeMissionHelperY.x); break;
            }
            typeMissionHelperY = transform.right;
        }

        PlayerCube.transform.Rotate((speed * -90 * Time.deltaTime) * rotateVectorPlayer);
        
        // PlayerCube.transform.rotation = Quaternion.Euler(PlayerCube.transform.rotation.eulerAngles.x + speed * -90 * Time.deltaTime, 0, 0);
        // PlayerCubeParent.transform.eulerAngles = new Vector3(0, transform.eulerAngles.y * -10 - PlayerCube.transform.rotation.y, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (RuningAfterLose)
            return;
        if (other.tag == "HamperParent")
        {
            SoundManager.Play(SoundManager.SoundType.WhenScorePP);
            backGround.SetScore(++GameController.Score);
            creating.Create();
            Creating.ChangePlayerComplexity(other.GetComponent<HamperParams>().complexity);
        }
        else if (other.tag == "Crystal")
        {
            GameController.PickupGemsInGame++;
            GameManager.Money++;
            Destroy(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        StartCoroutine(DestoryAfter(other.gameObject, 1f));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (RuningAfterLose)
            return;
        if (SoundManager.IsPlaying)
        {
            backGround.SetScore(--GameController.Score);
        }

        SoundManager.Stop();

        GameController.losesCause();
    }

    private IEnumerator DestoryAfter(GameObject _go, float _time)
    {
        yield return new WaitForSeconds(_time);

        while (Time.timeScale == 0)
            yield return 0;

        if (GameController.Playing)
            Destroy(_go);
    }

    private void TurnPipes(bool down = true)
    {
        if (down)
        {
            if (transform.position.y < (nowPipeNumber * -500) - 240)
            {
                nowPipeNumber++;
                Pipes[nowPipeNumber % 3].transform.position = new Vector3(0, nowPipeNumber * -500 - 240, 0);
            }
        }
        else
        {
            while (transform.position.y > (nowPipeNumber * -500) + 240)
            {
                nowPipeNumber--;
                Pipes[nowPipeNumber % 3].transform.position = new Vector3(0, nowPipeNumber * -500 - 240, 0);
            }
        }
    }

    private void SetRandomCube()
    {
        byte _shopIndex = (byte)Shop.ShopHasObjects.IndexOf('S');
        if (Shop.RandomCharacter && randomCharacterIndex.Count > 1)
        {
            byte _randomIndex;
            do
                _randomIndex = randomCharacterIndex[Random.Range(0, randomCharacterIndex.Count)];
            while (_randomIndex == nowRandomCharacterIndex);
            nowRandomCharacterIndex = _randomIndex;

            ChangePlayerCube(shopObjects.Objects[nowRandomCharacterIndex]);
        }
        else if (nowRandomCharacterIndex != _shopIndex)
        {
            nowRandomCharacterIndex = _shopIndex;
            ChangePlayerCube(shopObjects.Objects[nowRandomCharacterIndex]);
        }
    }

    private static float GetDefaultValueFromTypeMission(TypeMission typeM)
    {
        switch (typeM)
        {
            default: return 0;
        }
    }
}
