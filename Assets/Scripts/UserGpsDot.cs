using System.Collections;
using UnityEngine;
using UnityEngine.Android;

public class UserGpsDot : MonoBehaviour
{
    double lastTimestamp;
    float avgLocationDelay = 5;
    Coroutine moveCoroutine;

    public Transform target;
    public Transform corner1;
    public Transform corner2;

    private void Start()
    {
        if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation) ||
            !Input.location.isEnabledByUser)
            StartCoroutine(WaitForPermission());
        else
            StartCoroutine(TrackLocation());
    }

    IEnumerator TrackLocation()
    {
        target.gameObject.SetActive(true);
        Input.location.Start();
        yield return WaitForInitialization();
        while(true)
        {
            yield return new WaitForSeconds(1);
            if (Input.location.lastData.timestamp == lastTimestamp) 
                continue;
            //if (Input.location.lastData.horizontalAccuracy < 50)
            //    continue;

            lastTimestamp = Input.location.lastData.timestamp;
            if (moveCoroutine != null)
                StopCoroutine(moveCoroutine);

            moveCoroutine = StartCoroutine(MoveToNewPos(Input.location.lastData));
        }
    }

    IEnumerator MoveToNewPos(LocationInfo location)
    {
        float t = 0;
        Vector3 newPos = GeoLocationToVec3(location);
        Vector3 oldPos = target.position;
        while (t != 1)
        {
            t = Mathf.MoveTowards(t, 1, Time.deltaTime / avgLocationDelay);
            target.position = Vector3.Lerp(oldPos, newPos, t);
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

    IEnumerator WaitForPermission()
    {
        target.gameObject.SetActive(false);
        Permission.RequestUserPermission(Permission.FineLocation);
        while (!Input.location.isEnabledByUser &&
            !Permission.HasUserAuthorizedPermission(Permission.FineLocation))
            yield return null;

        StartCoroutine(TrackLocation());
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
