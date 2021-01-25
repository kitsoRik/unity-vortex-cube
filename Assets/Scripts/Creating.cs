#pragma warning disable IDE1006, IDE0044

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class Creating : MonoBehaviour {

    public const float A_WHEN_TRANSPARENT = 100 / 255f, START_DISTANCE = -35;
    public const int START_CREATE = 5;


    public static Color Orage = new Color(255/255f, 128/255f, 0),
        Lime = new Color(128/255f, 255/255f, 128/255f);

    public Control control;
    public RawImage backgroundImage;
    public GameObject Crystal, Parent, Effects;
    public Material HamperMaterial, CrystalMaterial;

    public GameObject ThreeUpDownCube;
    [SerializeField]
    public GameObject Semi_Circle, Semi_Circle80;
    public GameObject Cross;
    public GameObject Triangle;

    public Color toColor, toColorPipe, toColorCrystal;
    [HideInInspector]
    public float distance;
    public static int countCreated;
    public static Complexity complexityCreated { get; private set; }
    public static Complexity complexityPlayer { get; private set; }
    public List<GameObject> gameObjectsQueue = new List<GameObject>();
    public List<HamperType> hamperTypeQueue = new List<HamperType>();

    private int _lastRandHamper = -1;

    private static int toUpComplexityPlayer;

    void Start()
    {
        countCreated = 0;
        SetColors();

        GameController.replayCause += AfterReplay;
        GameController.mainMenuCause += AfterMainMenu;
        GameController.playHereCause += StartCreate;
        GameController.tryMorePlayCause += AfterTryMore;
        GameController.playingCause += AfterPlay;
    }

    public void AfterTryMore()
    {
        StartCoroutine(AfterTryMoreIE());
    }

    public void AfterPlay()
    {
        SetToColors();
    }

    private IEnumerator AfterTryMoreIE()
    {
        GameObject[] destroyGameObjects = GameObject.FindGameObjectsWithTag("CrystalParent");
        foreach (GameObject destroyGameObject in destroyGameObjects)
        {
            Destroy(destroyGameObject);
        }

        destroyGameObjects = GameObject.FindGameObjectsWithTag("HamperParent");
        foreach (GameObject destroyGameObject in destroyGameObjects)
        {
            Destroy(destroyGameObject);
            yield return new WaitForSecondsRealtime(0.2f);
        }

        StartCreate();
    }

    public void AfterReplay()
    {
        AfterMainMenu();
    }

    public void AfterMainMenu()
    {
        countCreated = 0;
        complexityPlayer = Complexity.Low;

        gameObjectsQueue.Clear();
        hamperTypeQueue.Clear();

        SetToColors();
    }

    private void Update()
    {
        if (!GameController.Playing)
            return;
        if (HamperMaterial.color != toColor)
        {
            HamperMaterial.color = Color.Lerp(HamperMaterial.color, toColor, Time.deltaTime * 2);
        }

        if (CrystalMaterial.color != toColorCrystal)
        {
            CrystalMaterial.color = Color.Lerp(CrystalMaterial.color, toColorCrystal, Time.deltaTime * 2);
        }

        if (backgroundImage.color != toColorPipe)
        {
            backgroundImage.color = Color.Lerp(backgroundImage.color, toColorPipe, Time.deltaTime * 2);
        }

        if (backgroundImage.color == toColorPipe && HamperMaterial.color == toColor && CrystalMaterial.color == toColorCrystal)
        {
            SetToColors();
        }
    }

    private void ChangeDistance()
    {
        distance -= 10;
    }

    private void SetColors()
    {
        backgroundImage.color = toColorPipe = GetRandomColor(Helper.CombiningArrays(new Color[] { toColorPipe }, control.PlayerCube.GetComponent<PlayerStats>().hasColors));
        HamperMaterial.color = toColor = Helper.InverseColor(toColorPipe);
        CrystalMaterial.color = toColorCrystal = GetRandomColor(toColor, toColorPipe);
    }

    private void SetToColors()
    {
        toColorPipe = GetRandomColor(Helper.CombiningArrays(new Color[] { toColorPipe }, control.PlayerCube.GetComponent<PlayerStats>().hasColors));
        toColor = Helper.InverseColor(toColorPipe);
        toColorCrystal = GetRandomColor(toColor, toColorPipe);
    }

    public void StartCreate()
    {
        distance = START_DISTANCE + control.transform.position.y;
        for (int i = 0; i < START_CREATE; i++)
            Create();
    }

    public void Create()
    {
        countCreated++;
        SetHampersList();

        Transform _parent = Instantiate(Parent).transform;

        _parent.position = new Vector3(0, distance, 0);

        GameObject @object = Instantiate(Helper.RemoveAndAt(gameObjectsQueue, 0), _parent);
        @object.transform.position = new Vector3(0, distance, 0);
        SetComplexityOfObject(ref @object, complexityCreated, Helper.RemoveAndAt(hamperTypeQueue, 0));

        _parent.GetComponent<HamperParams>().complexity = complexityCreated;

        ChangeDistance();

        foreach (Renderer rr in _parent.GetComponentsInChildren<Renderer>())
            rr.material = HamperMaterial;
        
        CreateCrystal();
    }

    private void CreateCrystal()
    {
        if (Random.Range(0, 5) == 0)
        {
            Transform _parent = Instantiate(Parent).transform;
            _parent.position = new Vector3(0, distance - 5, 0);
            _parent.tag = "CrystalParent";

            Transform _go = Instantiate(Crystal, _parent).transform;
            _go.RotateAround(_parent.eulerAngles, Vector3.up, Random.Range(0, 9) * 40);
            _parent.GetComponent<HamperParams>().complexity = complexityCreated;
        }
    }

    private void SetHampersList()
    {
        if (gameObjectsQueue.Count != 0)
            return;

        if (countCreated < 22)
        {
            complexityCreated = Complexity.Low;
        }
        else if (countCreated < 44)
        {
            complexityCreated = Complexity.Meddium;
        }
        else if (countCreated < 66)
        {
            complexityCreated = Complexity.High;
        }
        else
        {
            complexityCreated = Complexity.UltraHigh;
        }
        for (int i = 0; i < 5; i++)
        {
            int _rand = Random.Range(1, 4) * 2,
                _randHamper;
            do
            {
                _randHamper = Random.Range(0, 4);
            } while (_randHamper == _lastRandHamper);

            _lastRandHamper = _randHamper;

            for (int j = 0; j < _rand; j++)
            {
                gameObjectsQueue.Add(GetCreateHamper(_randHamper));
                hamperTypeQueue.Add(GetCreateHamperType(_randHamper));
            }
        }
    }

    private GameObject GetCreateHamper(int number)
    {
        switch (complexityCreated)
        {
            case Complexity.Low:
                switch (number)
                {
                    case 0: return ThreeUpDownCube;
                    case 1: return Semi_Circle;
                    case 2: return Cross;
                    case 3: return Triangle;
                    default: return ThreeUpDownCube;
                };
            case Complexity.Meddium:
                switch (number)
                {
                    case 0: return ThreeUpDownCube;
                    case 1: return Semi_Circle;
                    case 2: return Cross;
                    case 3: return Triangle;
                    default: return ThreeUpDownCube;
                }
            case Complexity.High:
                switch (number)
                {
                    case 0: return ThreeUpDownCube;
                    case 1: return Semi_Circle;
                    case 2: return Cross;
                    case 3: return Triangle;
                    default: return ThreeUpDownCube;
                }
            case Complexity.UltraHigh:
                switch (number)
                {
                    case 0: return ThreeUpDownCube;
                    case 1: return Semi_Circle80;
                    case 2: return Cross;
                    case 3: return Triangle;
                    default: return ThreeUpDownCube;
                }
        }
        return null;
    }

    private HamperType GetCreateHamperType(int number)
    {

        switch (number)
        {
            case 0: return HamperType.TDUC;
            case 1: return HamperType.Semi_circle;
            case 2: return HamperType.Cross;
            case 3: return HamperType.Triangle;
            default: return HamperType.Anything;
        }
    }

    private void SetComplexityOfObject(ref GameObject @object, Complexity _complexity, HamperType hamperType)
    {
        switch (_complexity)
        {
            case Complexity.Low:
                switch (hamperType)
                {
                    case HamperType.TDUC: @object.transform.Rotate(0, Random.Range(-90, 90f), 0); return;
                    case HamperType.Semi_circle: @object.transform.Rotate(0, Random.Range(0, 360f), 0); return;
                    case HamperType.Cross: @object.transform.Rotate(0, Random.Range(-90, 90f), 0); return;
                    case HamperType.Triangle: @object.transform.Rotate(0, Random.Range(-60, 60f), 0); return;
                    case HamperType.Anything: return;
                } return;
            case Complexity.Meddium:
                switch (hamperType)
                {
                    case HamperType.TDUC:
                        @object.transform.Rotate(0, Random.Range(-90, 90f), 0);
                        @object.AddComponent<RotatingR>(); return;
                    case HamperType.Semi_circle:
                        @object.transform.Rotate(0, Random.Range(0, 360f), 0);
                        @object.AddComponent<RotatingR>();
                        return;
                    case HamperType.Cross:
                        @object.transform.Rotate(0, Random.Range(-90, 90), 0);
                        @object.AddComponent<RotatingR>();
                        return;
                    case HamperType.Triangle:
                        @object.transform.Rotate(0, Random.Range(-60, 60), 0);
                        @object.AddComponent<RotatingR>();
                        return;
                    case HamperType.Anything: return;
                }
                return;
            case Complexity.High:
                switch (hamperType)
                {
                    case HamperType.TDUC:
                        @object.transform.Rotate(0, Random.Range(-90, 90f), 0);
                        @object.AddComponent<RotatingR>().speedRotating = countCreated % 2 == 0 ? 1 : -1; return;
                    case HamperType.Semi_circle:
                        @object.transform.Rotate(0, Random.Range(0, 360f), 0);
                        @object.AddComponent<RotatingR>().speedRotating = countCreated % 2 == 0 ? 1 : -1;
                        return;
                    case HamperType.Cross:
                        @object.transform.Rotate(0, Random.Range(-90, 90), 0);
                        @object.AddComponent<RotatingR>().speedRotating = countCreated % 2 == 0 ? 1 : -1;
                        return;
                    case HamperType.Triangle:
                        @object.transform.Rotate(0, Random.Range(-60, 60), 0);
                        @object.AddComponent<RotatingR>().speedRotating = countCreated % 2 == 0 ? 1 : -1;
                        return;
                    case HamperType.Anything: return;
                }
                return; 
            case Complexity.UltraHigh:
                switch (hamperType)
                {
                    case HamperType.TDUC:
                        @object.transform.Rotate(0, Random.Range(-90, 90f), 0);
                        @object.AddComponent<RotatingR>().speedRotating = countCreated % 2 == 0 ? 1 : -1; return;
                    case HamperType.Semi_circle:
                        @object.transform.Rotate(0, Random.Range(0, 360f), 0);
                        @object.AddComponent<RotatingR>().speedRotating = countCreated % 2 == 0 ? 1 : -1;
                        return;
                    case HamperType.Cross:
                        @object.transform.Rotate(0, Random.Range(-90, 90), 0);
                        @object.AddComponent<RotatingR>().speedRotating = countCreated % 2 == 0 ? 1 : -1;
                        return;
                    case HamperType.Triangle:
                        @object.transform.Rotate(0, Random.Range(-60, 60), 0);
                        @object.AddComponent<RotatingR>().speedRotating = countCreated % 2 == 0 ? 1 : -1;
                        return;
                    case HamperType.Anything: return; 
                }
                return; 
        }
    }

    private static Color GetRandomColor(params Color[] colors)
    {
        Color temp_color;
        do
        {
            switch (Random.Range(0, 9))
            {
                case 0: temp_color = Color.red; break;
                case 1: temp_color = Color.blue; break;
                case 2: temp_color = Color.green; break;
                case 3: temp_color = Color.yellow; break;
                case 4: temp_color = Color.magenta; break;
                case 5: temp_color = Lime; break;
                case 6: temp_color = Orage; break;
                default: temp_color = Color.cyan; break;
            }
        }
        while (CheckedIdentifyColor(temp_color, colors));
        return temp_color;
    }

    private static bool CheckedIdentifyColor(Color color, params Color[] colors)
    {
        foreach (Color _color in colors)
            if (_color == color)
                return true;
        return false;
    }

    public static string ComplexityToString(Complexity compl)
    {
        switch(compl)
        {
            case Complexity.UltraHigh: return "Ultra high";
            default: return compl.ToString();
        }
    }

    public static void ChangePlayerComplexity(Complexity complexityParent)
    {
        complexityPlayer = complexityParent;
    }
}

public enum Complexity
{
    Low,
    Meddium,
    High,
    UltraHigh
}

public enum HamperType
{
    TDUC,
    Semi_circle,
    Cross,
    Triangle,
    Anything
}
