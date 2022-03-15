using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class clickObject : MonoBehaviour
{
  public Text txt;
  bool motelAnimation = false;
  public GameObject key;
  bool keyUp = false;
  bool keyCollected = false;
  Vector3 newKeyPos;
  Vector3 keyPos;
  string[] dialogue = new string[] {"This motel needs a refresh... that crusty old wallpaper is not working", "The burger looks fresh, someones has to be here", "A bottle of soda, must be flat by now... ew", "Can't they afford to clean a bit?", "There is no way that thing is working. This place is like a sauna", "cute plant", "This looks like the main desk. Whoever was in charge must have left, hopefully not for long", "This must be the key to the Motel rooms",
  "Might as well go find a room for the night, I'll check back later."};
  Vector3[] roomPos = new Vector3[4];
  int roomNum = 0;

    // Start is called before the first frame update
    void Start()
    {
      newKeyPos = new Vector3(6.4f, 2f, -8.5f);
      keyPos = new Vector3(2.8f, 0.15f, 0.35f);

      //initialize position array
      roomPos[0] = new Vector3(0.0f, 5.0f, 7.0f);
      roomPos[1] = new Vector3(-17.0f, 5.0f, 7.0f);
      roomPos[2] = new Vector3(-34.0f, 5.0f, 7.0f);
      roomPos[3] = new Vector3(-51.0f, 5.0f, 7.0f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
      //motel animation start up
      if(motelAnimation == true){
        transform.Translate(0, 0, 6 * Time.deltaTime, Space.World);
        float posZ = gameObject.transform.position.z;
        txt.text = "After a long day of driving you need to stop for a break and see a large motel on the side of the road. You see the lights on and decide to head in and see if there are any rooms available.";
        if (posZ >= -140){
          transform.position = new Vector3(0, 5, 7);
          motelAnimation = false;
          txt.text = "You find the main desk but there doesn't seem to be anyone here, better look around for some information.";
        }
      }
      //bring key to Camera
      // if (keyUp == true){
      //   key.transform.position = Vector3.MoveTowards(keyPos, newKeyPos, 2);
      //   if (Vector3.Distance(key.transform.position, newKeyPos) < 0.001f){
      //     keyUp = false;
      //   }
      // }

      if (Input.GetMouseButtonDown(0))
      {
        Ray theray = Camera.main.ScreenPointToRay( Input.mousePosition );
        RaycastHit rayHitInfo;
        if (Physics.Raycast( theray, out rayHitInfo ))
        {
          var objNum = rayHitInfo.collider.gameObject.tag;
          var tag = objNum.ToString();
          if (tag == "Motel"){
            motelAnimation = true;
          } else if (tag == "key") {
            Debug.Log("1");
            keyUp = !keyUp;
            if(keyUp){
              Debug.Log("");
              txt.text = dialogue[7];
              keyCollected = true;
              key.transform.position = newKeyPos;
            } else if (keyUp == false){
              txt.text = dialogue[8];
              key.transform.position = keyPos;
            }
          } else {
            var tagNum = int.Parse(tag);
            txt.text = dialogue[tagNum];
          }
          //var objRenderer = rayHitInfo.collider.gameObject.GetComponent<Renderer>();
          //objRenderer.material.SetColor("_Color", colors[color_choice]);
        }
      }
      if (Input.GetKeyDown("right") && keyCollected){
        Debug.Log(":D");
        if (roomNum <= 0){
          txt.text = "There are no rooms that way, why would I go there?";
        } else {
          roomNum-= 1;
          txt.text = "Most rooms are currently unfinished. Check back later for more content.";
          transform.position = roomPos[roomNum];
        }
      }
      if (Input.GetKeyDown("left") && keyCollected){
        Debug.Log("):");
        if (roomNum >= 3){
          txt.text = "There are no rooms that way, why would I go there?";
        } else {
          roomNum+= 1;
          txt.text = "Most rooms are currently unfinished. Check back later for more content.";
          transform.position = roomPos[roomNum];
        }
      }
    }
}
