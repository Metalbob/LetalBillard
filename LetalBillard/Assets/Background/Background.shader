// Shader created with Shader Forge v1.32 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.32;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:3138,x:32928,y:32666,varname:node_3138,prsc:2|emission-7247-RGB,alpha-9019-OUT,clip-5688-OUT;n:type:ShaderForge.SFN_Tex2d,id:7247,x:32626,y:32718,ptovrint:False,ptlb:node_7247,ptin:_node_7247,varname:node_7247,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:d240f2e1c7b9e114a826f63312e90443,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:5500,x:32227,y:32925,ptovrint:False,ptlb:node_5500,ptin:_node_5500,varname:node_5500,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:76ea811fcc98a9f41a16ef723eeee3e8,ntxv:2,isnm:False;n:type:ShaderForge.SFN_OneMinus,id:5688,x:32440,y:32942,varname:node_5688,prsc:2|IN-5500-A;n:type:ShaderForge.SFN_Time,id:5184,x:32254,y:33108,varname:node_5184,prsc:2;n:type:ShaderForge.SFN_Cos,id:5858,x:32468,y:33131,varname:node_5858,prsc:2|IN-5184-TTR;n:type:ShaderForge.SFN_Multiply,id:9019,x:32665,y:33038,varname:node_9019,prsc:2|A-5858-OUT,B-3948-OUT;n:type:ShaderForge.SFN_ValueProperty,id:3948,x:32468,y:33308,ptovrint:False,ptlb:node_3948,ptin:_node_3948,varname:node_3948,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.3;proporder:7247-5500-3948;pass:END;sub:END;*/

Shader "Shader Forge/Background" {
    Properties {
        _node_7247 ("node_7247", 2D) = "white" {}
        _node_5500 ("node_5500", 2D) = "black" {}
        _node_3948 ("node_3948", Float ) = 0.3
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform sampler2D _node_7247; uniform float4 _node_7247_ST;
            uniform sampler2D _node_5500; uniform float4 _node_5500_ST;
            uniform float _node_3948;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                float4 _node_5500_var = tex2D(_node_5500,TRANSFORM_TEX(i.uv0, _node_5500));
                clip((1.0 - _node_5500_var.a) - 0.5);
////// Lighting:
////// Emissive:
                float4 _node_7247_var = tex2D(_node_7247,TRANSFORM_TEX(i.uv0, _node_7247));
                float3 emissive = _node_7247_var.rgb;
                float3 finalColor = emissive;
                float4 node_5184 = _Time + _TimeEditor;
                return fixed4(finalColor,(cos(node_5184.a)*_node_3948));
            }
            ENDCG
        }
        Pass {
            Name "ShadowCaster"
            Tags {
                "LightMode"="ShadowCaster"
            }
            Offset 1, 1
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_SHADOWCASTER
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform sampler2D _node_5500; uniform float4 _node_5500_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
                float2 uv0 : TEXCOORD1;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                float4 _node_5500_var = tex2D(_node_5500,TRANSFORM_TEX(i.uv0, _node_5500));
                clip((1.0 - _node_5500_var.a) - 0.5);
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
