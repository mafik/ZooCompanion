using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserGpsDot : MonoBehaviour
{
    double lastTimestamp;
    float avgLocationDelay = 5;
    Coroutine moveCoroutine;

    public Transform corner1;
    public Transform corner2;

    private void Start()
    {
        if (!Input.location.isEnabledByUser)
        {
            gameObject.SetActive(false);
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
            t = Mathf.MoveTowards(t, 1, Time.deltaTime / avgLocationDelay);
            transform.position = GeoLocationToVec3(location);
            yield return null;
        }
    }

    IEnumerator WaitForInitialization()
    {
        while (Input.location.status == LocationServiceStatus.Initializing)
            yield return new WaitForSeconds(1);

        if (Input.location.status == LocationServiceStatus.Failed)
            print("Unable to determine device location");
    }

    Vector3 GeoLocationToVec3(LocationInfo location)
    {
        Vector2 high = new Vector3(51.763975f, 19.419389f);
        Vector2 low = new Vector2(51.7577306f, 19.406842f);
        Vector2 scale = high - low;

        float lattitudeNormalized = (location.latitude - low.x) / scale.x;
        float longitudeNormalized = (location.longitude - low.y) / scale.y;

        return new Vector3(
            Mathf.LerpUnclamped(corner2.transform.position.x, corner1.transform.position.x, longitudeNormalized),
            0,
            Mathf.LerpUnclamped(corner2.transform.position.z, corner1.transform.position.z, lattitudeNormalized));
    }
}
