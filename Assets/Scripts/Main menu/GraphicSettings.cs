using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Singleton for storing current graphic configuration
public class GraphicSettings {
	public bool fullscreen;
	public int graphicsQuality;
	public int textureQuality;
	public int antialiasing;
	public int vSync;
	public int resolutionIndex;
	private static GraphicSettings graphicSettings;
	
	private GraphicSettings() {}

   public static GraphicSettings Instance
   {
      get 
      {
         if (graphicSettings == null)
         {
            graphicSettings = new GraphicSettings();
         }
         return graphicSettings;
      }
   }
}
