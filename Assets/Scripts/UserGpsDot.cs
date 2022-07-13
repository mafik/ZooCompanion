using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserGpsDot : MonoBehaviour
{
    double lastTimestamp;
    float avgLocationDelay = 5;
    Coroutine moveCoroutine;

    Text t1;
    Text t2;


    private void Start()
    {
        if (!Input.location.isEnabledByUser)
        {
            gameObject.SetActive(false);
            t1.text = "error";
            return;
        }

        Input.location.Start();
        StartCoroutine(TrackLocation());
    }

    IEnumerator TrackLocation()
    {
        yield return WaitForInitialization();
        while(true)
        {
            yield return new WaitForSeconds(1);
            if (Input.location.lastData.timestamp == lastTimestamp) 
                continue;

            lastTimestamp = Input.location.lastData.timestamp;
            if (moveCoroutine != null)
                StopCoroutine(moveCoroutine);

            moveCoroutine = StartCoroutine(MoveToNewPos(Input.location.lastData));
        }
    }

    IEnumerator MoveToNewPos(LocationInfo location)
    {
        float t = 0;
        while (t != 1)
        {
            t = Mathf.MoveTowards(t, 1, Time.deltaTime / 5);
            transform.position = GeoLocationToVec3(location);
            yield return null;
        }
    }

    IEnumerator WaitForInitialization()
    {
        while (Input.location.status == LocationServiceStatus.Initializing)
            yield return new WaitForSeconds(1);

        if (Input.location.status == LocationServiceStatus.Failed)
        {
            print("Unable to determine device location");
            t1.text = "Unable to determine device location";
        }
    }

    Vector3 GeoLocationToVec3(LocationInfo location)
    {
        return new Vector3();
        Vector2 scale = new Vector2(
            51.763653f - 51.758048f,
            19.416828f - 19.408652f);

        float lattitude = location.latitude - 51.758048f;
        float lattitudeNormalized = lattitude;
        
    }
}
