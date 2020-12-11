Shader "Custom/Outline"
{
    // Variables that you can set in unity editor
    Properties
    {
        _MainTexture("Main Texture (RBG)", 2D) = "white" {}
        _Color("Color", Color) = (1,1,1,1)
    }
    
    SubShader
    {
        Pass
        {
            CGPROGRAM // Allows talk between unity's shader lab and nvidia's C for graphics
            // function defines
            // define the building function            
            #pragma vertex vert
            
            // define the colloring function
            #pragma fragment frag
            
            // includes
            // built in shader functions
            #include "UnityCG.cginc"
            
            // structures
            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };
            
            struct v2f 
            {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
            };
            
            // imports
            float4 _Color;
            sampler2D _MainTexture;
            
            v2f vert(appdata IN)
            {
                v2f OUT;
                
                OUT.pos = UnityObjectToClipPos(IN.vertex);
                OUT.uv = IN.uv;
                
                return OUT;
            }
            
            ENDCG
        }
    }
}
