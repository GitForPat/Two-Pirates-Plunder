using UnityEngine;

public class gunShip : MonoBehaviour {
    private int health_;
    private float speed_;
    private float reloadSpeed_; //How long it takes the cannons to be reloaded

    //Handles the reload features of the enemy gun boats
    private float currentReloadTimeLeft_;
    private float currentReloadTimeRight_;
    private bool canFireRight_;
    private bool canFireLeft_;
    private bool isReloadingLeft_;
    private bool isReloadingRight_;

    //Classes/gameObjects that handle the trigger event for when the player enters firing range
    public GameObject leftGun;
    public GameObject rightGun;
    private leftGun leftFiringArea_;
    private rightGun rightFiringArea_;

    public GameObject huntThis; //Player object
    public GameObject CannonBall; //Prefab for the cannon ball

    public int Health_ { get => health_; set => health_ = value; }

    private void Awake() {
        health_ = 10;
        speed_ = 0.5f;
        reloadSpeed_ = 3.0f;
        canFireRight_ = false;
        canFireLeft_ = false;
        isReloadingLeft_ = false;
        isReloadingRight_ = false;
        leftFiringArea_ = leftGun.GetComponent<leftGun>();
        rightFiringArea_ = rightGun.GetComponent<rightGun>();
    }

    private void Update() {
        if (health_ <= 0) { //Check to see if gun boat is dead
            Destroy(this.gameObject);
        }

        if (isReloadingLeft_) { //Reload timer for the left gun
            currentReloadTimeLeft_ += Time.deltaTime;
            if(currentReloadTimeLeft_ >= reloadSpeed_) {
                currentReloadTimeLeft_ = 0.0f;
                isReloadingLeft_ = false;
                canFireLeft_ = true;
            }
        }

        if (isReloadingRight_) { //Reload timer for the right gun 
            currentReloadTimeRight_ += Time.deltaTime;
            if (currentReloadTimeRight_ >= reloadSpeed_) {
                currentReloadTimeRight_ = 0.0f;
                isReloadingRight_ = false;
                canFireRight_ = true;
            }
        }

        canFireCheck();
        fireCannons();
    }

    private void FixedUpdate() { //Move the gun boat to the player (will fix later, but should work for alpha)
        Vector3 newLocation = new Vector3(huntThis.transform.position.x + 2.0f, huntThis.transform.position.y, huntThis.transform.position.z + 2.5f);
        this.gameObject.transform.position = Vector3.Lerp(this.gameObject.transform.position, newLocation, 0.01f);
    }

    private void canFireCheck() { //Check to see if the gun boat can fire its cannons 
        if (!isReloadingLeft_ && leftFiringArea_.PlayerInRange) {
            canFireLeft_ = true;
        }

        if (!isReloadingRight_ && rightFiringArea_.PlayerInRange) {
            canFireRight_ = true;
        }
    }

    private void fireCannons() { //Gun boat firing the cannons 
        if (canFireLeft_) {
            canFireLeft_ = false;
            isReloadingLeft_ = true;
            GameObject cannonBall = Instantiate(CannonBall, leftGun.transform.position, Quaternion.identity);
            cannonBall.name = "left";
            cannonShot x = cannonBall.GetComponent<cannonShot>();
            x.StartingLocation_ = leftGun.transform.position;
            x.EndingLocation_ = huntThis.transform.position;
        }

        if (canFireRight_) {
            canFireRight_ = false;
            isReloadingRight_ = true;
            Instantiate(CannonBall, rightGun.transform.position, Quaternion.identity);
            GameObject cannonBall = Instantiate(CannonBall, rightGun.transform.position, Quaternion.identity);
            cannonBall.name = "right";
            cannonShot x = cannonBall.GetComponent<cannonShot>();
            x.StartingLocation_ = rightGun.transform.position;
            x.EndingLocation_ = huntThis.transform.position;
        }
    }
}
