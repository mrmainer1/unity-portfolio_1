Shader "Projector/Light" {
  Properties {
  	  _Color ("Main Color", Color) = (1,1,1,1)   	
  }
  Subshader {
     Pass {
        ZWrite off
        Fog { Color (0, 0, 0) }
        Color [_Color]
        ColorMask RGB
        Blend DstColor One
		Offset -1, -1
     }
  }
}