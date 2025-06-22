Shader "Custom/SoftOutline"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _OutlineColor ("Outline Color", Color) = (0, 0, 0, 1)
        _OutlineWidth ("Outline Width", Range(0, 0.1)) = 0.01
    }

    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _OutlineColor;
            float _OutlineWidth;

            v2f vert(appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                fixed4 outline = fixed4(0, 0, 0, 0);

                // Calculate alpha based on distance from outline
                float2 center = float2(0.5, 0.5);
                float distance = length(i.uv - center);
                float alpha = smoothstep(1.0 - _OutlineWidth, 1.0, distance);

                outline.rgb = _OutlineColor.rgb;
                outline.a = _OutlineColor.a * alpha;

                col = lerp(col, outline, outline.a);

                return col;
            }
            ENDCG
        }
    }
}
