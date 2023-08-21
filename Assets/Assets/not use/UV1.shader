Shader "Debug/UV 1" {
SubShader {
    Pass {
        CGPROGRAM
        #pragma vertex vert
        #pragma fragment frag
        #include "UnityCG.cginc"

        // 頂点の入力: 位置、 UV
        struct appdata {
            float4 vertex : POSITION;
            float4 texcoord : TEXCOORD0;
        };

        struct v2f {
            float4 pos : SV_POSITION;
            float4 uv : TEXCOORD0;
            fixed4 color : COLOR;
        };
        
        v2f vert (appdata v) {
            v2f o;
    
            o.pos = UnityObjectToClipPos(v.vertex );
            o.uv = float4( v.texcoord.xy, 0.5, 0.5 );
            o.color = 1.0;
            return o;
        }
        
        half4 frag( v2f i ) : SV_Target {
            half4 c = frac( i.uv );
            if (any(saturate(i.uv) - i.uv))
                c.b = 1.0;
            return c;
        }
        ENDCG
    }
}
}