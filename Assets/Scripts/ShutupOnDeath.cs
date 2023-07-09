using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShutupOnDeath : MonoBehaviour
{
   private void Update()
   {
      if (Mathf.Approximately(Time.timeScale, 0.0f))
      {
         Destroy(gameObject);
      }
   }
}
