using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class DetectLocationOwnRoute : MonoBehaviour
{
    public GameObject map, locationprovider;
    private bool running;
    private bool enableByRequest = true;
    private int maxWait = 10;
    public Text console;
    private bool ready = false;
    private float proximity = 0.001f;
    private Vector2 deviceCoordinates;
    private float distanceFromTarget = 0.0004f;

    public List<int> numberOfThisLocation = new List<int>();
    //public Button[] locationbuttons;

    public List<GameObject> locations = new List<GameObject>();
    public Button exitButton;
    public float sLatitude, sLongitude;
    private int number;
    private int i = 0;
    private int k = 1;
    //public AddLocationsToList addlocationsToList;

    public static List<Vector2> listofCoordinates = new List<Vector2>();


    // Use this for initialization
    void Start()
    {
        exitButton.onClick.AddListener(ExitButtonPressed);   

    }


    void ExitButtonPressed()
    {
        StartCoroutine(getLocation());
        shutAllLocacations();
    }

    void shutAllLocacations()
    {
        foreach(GameObject item in locations)
        {
            item.SetActive(false);
        }
    }

    public void letsStartGps()
    {
        /*for(int j = 0; j<addlocationsToList.locations.Count; j++)
        {
            listofCoordinates.Add(addlocationsToList.returnCoordinates(addlocationsToList.locations[j]));
            Debug.Log(listofCoordinates[j].ToString("F6"));
        }*/

        
        StartCoroutine(getLocation());
    }


   
    // Update is called once per frame
    void Update()
    {

    }
    

    

    IEnumerator getLocation()
    {
        running = true;

        LocationService service = Input.location;
        if (!enableByRequest && !service.isEnabledByUser)
        {
            Debug.Log("Location Services not enabled by user");
            yield break;
        }

        service.Start();


        while (service.status == LocationServiceStatus.Initializing && maxWait > 3)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }
        if (maxWait < 1)
        {
            Debug.Log("Timed out");
            yield break;
        }
        if (service.status == LocationServiceStatus.Failed)
        {
            Debug.Log("Unable to determine device location");
            yield break;
        }
        else
        {
            //console.text ="\nMy Location: " + service.lastData.latitude + ", " + service.lastData.longitude;
            sLatitude = service.lastData.latitude;
            sLongitude = service.lastData.longitude;
        }
        //service.Stop();
        ready = true;
        startCalculate();

        //Debug.Log("wait" + sLatitude + sLongitude);
        yield return new WaitForSeconds(1);
        StartCoroutine(getLocation());

    }




    public void startCalculate()
    {
        
        deviceCoordinates = new Vector2(sLatitude, sLongitude);

        if (listofCoordinates.Count == 0) Debug.Log("ei oo yhtään");
        //locations[addlocationsToList.locations[0]-1].SetActive(true);

        else
        {
            Debug.Log(listofCoordinates[0]);
            //targetcoordinates = addlocationsToList.returnCoordinates(addlocationsToList.locations[j]);
            console.text += "calculating " + (listofCoordinates[0].ToString("F6"));

            proximity = Vector2.Distance(listofCoordinates[0], deviceCoordinates);

            console.text = proximity.ToString() + " || " + distanceFromTarget.ToString();

            if (proximity <= distanceFromTarget)
            {
                console.text = "perskeles";
                StopAllCoroutines();
                // TODO 
                //
                //
                locations[numberOfThisLocation[i]].SetActive(true);
                map.SetActive(false);
                locationprovider.SetActive(false);
                console.text = "now we are in " + k + " location" + listofCoordinates[0].ToString("F6");
                listofCoordinates.Remove(listofCoordinates[0]);
                k++;
                i++;
                //numberOfThisLocation++;

            }
        }

    }
}