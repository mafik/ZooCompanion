using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Followers : MonoBehaviour
{
    public Transform center;

    public List<GameObject> followers = new List<GameObject>();
    float timer;

    void Update()
    {
        if (center.gameObject.activeInHierarchy == false)
            return;

        timer += (Time.deltaTime * 5) / (5 + followers.Count);
        for (int i = 0; i < followers.Count; ++i)
            UpdateFollower(followers[i], ((float)i) / followers.Count);
    }

    public void AddFollower(GameObject follower)
    {
        if (followers.Contains(follower)||
            !gameObject.activeInHierarchy)
            return;

        StartCoroutine(ScaleFollower(follower));
        followers.Add(follower);
    }

    void UpdateFollower(GameObject follower, float order)
    {
        float phase = (order + timer / 8) * Mathf.PI * 2;

        Vector3 desiredPos =
            center.position +
            new Vector3(Mathf.Sin(phase), 0, 0) * 0.5f +
            new Vector3(0, 0, Mathf.Cos(phase) * 0.5f);

        Vector3 nextPos = Vector3.MoveTowards(follower.transform.position, desiredPos, Time.deltaTime * 0.5f);
        nextPos.y = desiredPos.y;

        float vertOffset = (phase * 2) % 1;
        vertOffset = (vertOffset - vertOffset * vertOffset) * 0.7f;

        float sideRot = Mathf.Sin(timer * 8) * 100 * vertOffset;

        if (follower.name == "Kangaroo")
            vertOffset *= 2.5f;

        nextPos.y += vertOffset;

        follower.transform.LookAt(new Vector3(nextPos.x, follower.transform.position.y, nextPos.z));
        follower.transform.Rotate(follower.transform.forward, sideRot);
        follower.transform.position = nextPos;
    }

    IEnumerator ScaleFollower(GameObject follower)
    {
        Vector3 baseScale = follower.transform.localScale;
        float t = 0;
        while(t != 1)
        {
            t = Mathf.MoveTowards(t, 1, Time.deltaTime * 0.2f);
            follower.transform.localScale = baseScale * Mathf.Lerp(1, 0.7f, Tween.InOut(t));
            yield return null;
        }
    }
}
