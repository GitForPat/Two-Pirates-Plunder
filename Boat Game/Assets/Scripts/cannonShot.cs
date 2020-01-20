using UnityEngine;

public class cannonShot : MonoBehaviour
{
    private int damageValue_;
    private float speed_;
    private float startTime_;
    private Vector3 offset_;
    private Vector3 startingLocation_;
    private Vector3 endingLocation_;

    public Vector3 StartingLocation_ { get => startingLocation_; set => startingLocation_ = value; }
    public Vector3 EndingLocation_ { get => endingLocation_; set => endingLocation_ = value; }

    private void Awake() {
        damageValue_ = 5;
        speed_ = 9.0f;
        offset_ = new Vector3(1.0f, 1.5f, -0.5f);
    }

    private void Start() {
        startTime_ = Time.time;
    }

    private void FixedUpdate() { //Move the bullet to the players last know location 
        Vector3 newVec = new Vector3(endingLocation_.x + 1.0f, endingLocation_.y + 0.5f, endingLocation_.z - 0.5f);
        float journeyLength_ = Mathf.Sqrt(Mathf.Pow((startingLocation_.x - newVec.x), 2.0f) + Mathf.Pow((startingLocation_.z - newVec.z), 2.0f));
        float distanceToCover = (Time.time - startTime_) * speed_;
        float fractionOfJourney = distanceToCover / journeyLength_;
        this.gameObject.transform.position = Vector3.Lerp(startingLocation_, newVec, fractionOfJourney);
        if (startingLocation_ != newVec) {
            Debug.Log("MISS");
            //Destroy(this.gameObject);
        }
    }

    public void OnCollisionEnter(Collision collision) {
        Debug.Log("HIT");
        if (string.Compare(collision.gameObject.tag, "Player") == 0) {
            //Decrease Player Health Here
            
            Destroy(this.gameObject);
        }
    }
}
