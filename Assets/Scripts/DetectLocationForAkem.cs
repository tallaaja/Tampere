using GoogleARCore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DetectLocationForAkem : MonoBehaviour {

    private bool firstlocationPlayed, secondlocationPlayed, thirdlocationPlayed = false;

    public GameObject ARObject, exitObj;

    private bool enableByRequest = true;
    private bool running;
    public int maxWait = 10;
    private float dLatitude1 = 60.16953f, dLongitude1 = 24.93390f;
    private float dLatitude2 = 60.169599f, dLongitude2 = 24.9343199f;
    private float dLatitude3 = 60.1681299f, dLongitude3 = 24.9351198f;
    public float sLatitude, sLongitude;
    private bool ready = false;
    private float distanceFromTarget = 0.0004f;
    private float proximity = 0.001f;
    public Text text, UIText;
    private Vector2 deviceCoordinates;
    private Vector2 targetCoordinates1, targetCoordinates2, targetCoordinates3;

    
    // Use this for initialization
    void Start () {
        targetCoordinates1 = new Vector2(dLatitude1, dLongitude1);
        targetCoordinates2 = new Vector2(dLatitude2, dLongitude2);
        targetCoordinates3 = new Vector2(dLatitude3, dLongitude3);

        var firstPermission = AndroidPermissionsManager.RequestPermission("android.permission.ACCESS_FINE_LOCATION");


        if (firstPermission == null)
        {
            text.text += "null";
        }

        firstPermission.WaitForCompletion();

        StartCoroutine(getLocation());

    }
    public void startGetLocation()
    {
        StartCoroutine(getLocation());
        text.text += "GETLOCATION UUDESTAAN";
    }
	
	// Update is called once per frame
	void Update () {
		
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
            text.text = "Target Location : " + dLatitude1 + ", " + dLongitude1 + "\nMy Location: " + service.lastData.latitude + ", " + service.lastData.longitude;
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

        
        if(firstlocationPlayed == false)
        {
            proximity = Vector2.Distance(targetCoordinates1, deviceCoordinates);
            if (proximity <= distanceFromTarget)
            {
                exitObj.SetActive(true);
                StopAllCoroutines();
                ARObject.SetActive(true);
                firstlocationPlayed = true;
                Debug.Log("something");
                text.text += "first location found";
                UIText.text = "Finlayson - Väinö Linnan aukio Skotlantilaissyntyisen James Finlaysonin v. 1820 perustama puuvillatehdas on ollut kaupungin tärkeimpiä teollisuuslaitoksia.Kutomorakennus Plevnassa(1877) sytytettiin pohjoisen Euroopan ensimmäinen sähkövalo v. 1882.Nykyään Finlaysonin alueella toimii mm. museoita, yrityksiä, ravintoloita, kauppoja, Aamulehden toimitus ja elokuvakeskus.";
            }
        }

        else if (secondlocationPlayed == false)
        {
            proximity = Vector2.Distance(targetCoordinates2, deviceCoordinates);
            if (proximity <= distanceFromTarget)
            {
                exitObj.SetActive(true);
                StopAllCoroutines();
                ARObject.SetActive(true);
                secondlocationPlayed = true;
                Debug.Log("something");
                text.text += "second location found";
                UIText.text = "Tampella/Vapriikki Tampellan alueen historia alkoi v. 1844 pienestä masuunista. Vuonna 1861 silloiset konepaja ja pellavatehdas yhdistyivät yritykseksi, joka myöhemmin tuli tunnetuksi Tampellana. Tampella valmisti mm.hiomakoneita, turbiineja, laivoja ja vetureita sekä pellavatuotteita. Nykyään Tampella toimii kotina mm.Museokeskus Vapriikille, joka tarjoaa nähtävää ja koettavaa koko perheelle. Vapriikissa on aina n. 10 näyttelyä, joiden skaala ulottuu kansainvälisistä näyttelyistä pysyviin ja paikallisiin teemoihin.Vapriikissa toimii myös Suomen Pelimuseo, joka esittelee suomalaista pelikulttuuria monipuolisesti. Kävijät pääsevät kokeilemaan eri aikakausien pelejä niiden oikeissa ympäristöissä.";
            }
        }

        else if (thirdlocationPlayed == false)
        {
            proximity = Vector2.Distance(targetCoordinates3, deviceCoordinates);
            if (proximity <= distanceFromTarget)
            {
                exitObj.SetActive(true);
                StopAllCoroutines();
                ARObject.SetActive(true);
                secondlocationPlayed = true;
                Debug.Log("something");
                text.text += "second location found";
                UIText.text = "Tallipiha/Milavida/Näsinkallio/Tiitiäisen satupuisto Tallipiha on osa vanhaa Tamperetta, joka kasvoi 1800 - luvulla Finlaysonin puuvillatehtaan ympärille.Nykyään tallipiha on käsityöläispuotien ja Tallipihan Kahvilan tyyssija. Finlaysonin palatsi valmistui v. 1899 tehtaanpatruuna Nottbeckin pojan kodiksi ja toimii nykyään ravintolana. Tampereen Näsinpuistossa sijaitseva, täydellisesti entisöity Näsilinna avasi ovensa huhtikuussa 2015.Samalla avautui Museo Milavida, joka esittelee tehtaanomistaja von Nottbeckin perheen tarinaa sekä vaihtuvia muodin ja tyylin historiaan liittyviä näyttelyitä.Näsilinna toimi myös vuoden 1918 sisällissodan näyttämönä, ja rakennuksen hallinnasta käytiin useita taisteluita. Näsinpuistosta, mäen huipulta, löydät myös taiteen ja aktiviteetit yhdistävän Tiitiäisen Satupuiston.Leikkipuisto luotiin kunnianosoiteuksena runoilija Kirsi Kunnakselle, jonka satujen eläinhahmojen veistoksia on siroteltuna ympäri puistoa.";
            }
        }




    }
}
