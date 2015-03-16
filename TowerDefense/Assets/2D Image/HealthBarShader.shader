Shader "3A/HealthBarShader" {
	Properties{
		_Width("Healthbar Width", Float) = 1
		_Height("Healthbar Height", Float) = 1
		_MaxHP("Healthbar MaxHP", Float) = 10
		_HP("HealthBar HP", Float) = 5
	}
    SubShader {
        Pass {

            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

			float _Width;
			float _Height;
			float _MaxHP;
			float _HP;

            struct v2f {
                float4 pos : SV_POSITION;
                fixed3 color : COLOR0;
            };

            v2f vert (appdata_base v)
            {
                v2f o;
				float4 worldv = mul(UNITY_MATRIX_MVP, float4(0,0,0,1));
				o.pos = worldv + float4(v.vertex.x * _Width * (_HP/_MaxHP), v.vertex.y * _Height, v.vertex.z, v.vertex.w);
                o.color = float4(0,1,0,1);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                return fixed4 (i.color, 1);
            }
            ENDCG

        }
    }
}