using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace a
{
    public class MinePlayer : MonoBehaviour
    {

        //GAMEBJECTS
        public GameObject minePrefab2; //mine object

        //TRANSFORMS
        public Transform leavePoint2; //leave point

        //SCRIPTS
        public UI ui;//UI Script

        //VARIABLES
        int weapon; //weapon id
        public int mineCount2; //mine count

        //BOOLS
        public bool ableToMine2;

        void Start()
        {
            ui.MineAmount(mineCount2); //show mine amount on UI      
        }

        void Update()
        {
            weapon = PlayerPrefs.GetInt("selectedWeapon"); //get current weapon id
                                                           //KNK İSTERSEN BUNU FİXED ALABİLİRİZ YA DA AYRI Bİ VOİD İLE ÇAĞIRABİLİRİZ AMA SAHNELER ARASI ZOR OLUR DİYE BÖYLE YAPTIM ŞİMDİLİK

            if (Input.GetButtonDown("Fire1") && mineCount2 > 0 && weapon == 1 && ableToMine2) // press "T" for leave a mine
            {
                GameObject mine = Instantiate(minePrefab2, leavePoint2.position, leavePoint2.rotation); //leave mine

                mineCount2--; //decrease mine count
                ui.MineAmount(mineCount2); //send mine count to UI Script
            }
        }

    }
}
