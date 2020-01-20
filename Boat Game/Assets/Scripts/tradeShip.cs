using UnityEngine;

public class tradeShip : MonoBehaviour
{
    #region Class Fields
    private int health_;
    private float speed_;

    //Lerp variables
    private float waitTime_;
    private float currentWaitTime_;
    private bool waitDone_;
    private bool startLerp_;
    private float startTime_;
    private float journeyLength_;
    public Transform[] patrolLocations;
    private Vector3 startingLocation_;
    private Vector3 endingLocation_;
    #endregion

    #region Gets and Sets
    public int Health { get => health_; set => health_ = value; }
    #endregion

    private void Awake() {
        health_ = 5;
        speed_ = 3.0f;
        waitTime_ = 5.0f;
        waitDone_ = true;
        startLerp_ = true;
    }

    private void Start() {
        startingLocation_ = this.gameObject.transform.position;
        pickRandomLocation();
        journeyLength_ = Mathf.Sqrt(Mathf.Pow((startingLocation_.x - endingLocation_.x),2.0f) + Mathf.Pow((startingLocation_.z - endingLocation_.z), 2.0f)); //Calculate the distance for first lerp equation
    }

    private void Update() {
        if(health_ <= 0) { //Check to see if the player is dead
            Destroy(this.gameObject);
        }
    }

    private void FixedUpdate() {
        if (startLerp_) {
            startTime_ = Time.time; //Get the time the lerp started
            startLerp_ = false;
        }

        if (waitDone_) { //The boat needs to wait for a few seconds before lerping to another location
            moveBoat();

            if(this.gameObject.transform.position == endingLocation_) { //Lepr is finished
                waitDone_ = false;
            }
        }
        else { //Wait and once done pick new location to lerp to
            currentWaitTime_ += Time.deltaTime;
            if(currentWaitTime_ >= waitTime_) {
                waitDone_ = true;
                startLerp_ = true;
                currentWaitTime_ = 0.0f;
                startingLocation_ = this.gameObject.transform.position;
                pickRandomLocation();
                journeyLength_ = Mathf.Sqrt(Mathf.Pow((this.gameObject.transform.position.x - endingLocation_.x), 2.0f) + Mathf.Pow((this.gameObject.transform.position.z - endingLocation_.z), 2.0f));
            }
        }
    }

    private void pickRandomLocation() { //Pick a random location to lerp to
        int randomNumber = Random.Range(0, (patrolLocations.Length-1));
        if(patrolLocations[randomNumber].position != this.gameObject.transform.position) {
            endingLocation_ = patrolLocations[randomNumber].position;
        }
        else {
            if(randomNumber == (patrolLocations.Length-1)) {
                randomNumber -= 1;
            }
            else if(randomNumber == 0) {
                randomNumber += 1;
            }
            else {
                randomNumber += 1;
            }
        }
        endingLocation_ = patrolLocations[randomNumber].position;
    }

    private void moveBoat() {
        float distanceToCover = (Time.time - startTime_) * speed_;
        float fractionOfJourney = distanceToCover / journeyLength_;
        this.gameObject.transform.position = Vector3.Lerp(startingLocation_, endingLocation_, fractionOfJourney);
    }

    public void OnCollisionEnter(Collision collision) { //If the boat is shot remove health
        Debug.Log("Hit");
    }
}
