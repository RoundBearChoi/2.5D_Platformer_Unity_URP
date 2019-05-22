// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:1,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:2,bsrc:0,bdst:0,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:True,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:True,fnsp:True,fnfb:True,fsmp:False;n:type:ShaderForge.SFN_Final,id:4795,x:33564,y:32717,varname:node_4795,prsc:2|emission-2393-OUT;n:type:ShaderForge.SFN_Tex2d,id:6074,x:32254,y:32581,ptovrint:False,ptlb:Main Texutre,ptin:_MainTexutre,varname:_MainTexutre,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:10ff1f268ba06f7429f974521abe4b42,ntxv:0,isnm:False|UVIN-902-OUT;n:type:ShaderForge.SFN_Multiply,id:2393,x:33149,y:32831,varname:node_2393,prsc:2|A-9735-OUT,B-2053-RGB,C-4032-OUT,D-9248-OUT,E-9633-OUT;n:type:ShaderForge.SFN_VertexColor,id:2053,x:32235,y:32772,varname:node_2053,prsc:2;n:type:ShaderForge.SFN_Color,id:797,x:32235,y:32930,ptovrint:True,ptlb:Color,ptin:_TintColor,varname:_TintColor,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:0.5342799,c3:0.1764706,c4:1;n:type:ShaderForge.SFN_Vector1,id:9248,x:32235,y:33087,varname:node_9248,prsc:2,v1:2;n:type:ShaderForge.SFN_Time,id:9160,x:31342,y:33199,varname:node_9160,prsc:2;n:type:ShaderForge.SFN_ValueProperty,id:4567,x:31144,y:33377,ptovrint:False,ptlb:Gradient U Speed,ptin:_GradientUSpeed,varname:_GradientUSpeed,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:-0.2;n:type:ShaderForge.SFN_ValueProperty,id:4497,x:31144,y:33449,ptovrint:False,ptlb:Gradient V Speed,ptin:_GradientVSpeed,varname:_GradientVSpeed,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:-0.2;n:type:ShaderForge.SFN_Append,id:8618,x:31342,y:33377,varname:node_8618,prsc:2|A-4567-OUT,B-4497-OUT;n:type:ShaderForge.SFN_Multiply,id:9458,x:31597,y:33281,varname:node_9458,prsc:2|A-9160-T,B-8618-OUT;n:type:ShaderForge.SFN_Tex2d,id:4069,x:32235,y:33223,ptovrint:False,ptlb:Gradient,ptin:_Gradient,varname:_Gradient,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:28c7aad1372ff114b90d330f8a2dd938,ntxv:0,isnm:False|UVIN-6774-OUT;n:type:ShaderForge.SFN_Multiply,id:9633,x:32742,y:33138,varname:node_9633,prsc:2|A-6074-A,B-7884-OUT,C-73-OUT;n:type:ShaderForge.SFN_Slider,id:1571,x:31210,y:33036,ptovrint:False,ptlb:Noise Amount,ptin:_NoiseAmount,varname:_NoiseAmount,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-1,cur:0.1144851,max:1;n:type:ShaderForge.SFN_Lerp,id:405,x:31604,y:32900,varname:node_405,prsc:2|A-8661-UVOUT,B-2788-R,T-1571-OUT;n:type:ShaderForge.SFN_TexCoord,id:3523,x:30781,y:32637,varname:node_3523,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Tex2d,id:2788,x:31185,y:32819,ptovrint:False,ptlb:Distortion,ptin:_Distortion,varname:_Distortion,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:82f4b06147155c54da475b309b9e24fa,ntxv:0,isnm:False|UVIN-2765-OUT;n:type:ShaderForge.SFN_Add,id:6774,x:31956,y:33217,varname:node_6774,prsc:2|A-2493-OUT,B-9458-OUT;n:type:ShaderForge.SFN_SwitchProperty,id:9664,x:31955,y:32596,ptovrint:False,ptlb:Distort Main Texture,ptin:_DistortMainTexture,varname:_DistortMainTexture,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,on:True|A-8661-UVOUT,B-2493-OUT;n:type:ShaderForge.SFN_Multiply,id:4009,x:30781,y:32820,varname:node_4009,prsc:2|A-7842-T,B-9706-OUT;n:type:ShaderForge.SFN_Append,id:9706,x:30570,y:32903,varname:node_9706,prsc:2|A-8025-OUT,B-7472-OUT;n:type:ShaderForge.SFN_Time,id:7842,x:30570,y:32722,varname:node_7842,prsc:2;n:type:ShaderForge.SFN_ValueProperty,id:8025,x:30372,y:32903,ptovrint:False,ptlb:Distortion U Speed,ptin:_DistortionUSpeed,varname:_DistortionUSpeed,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.2;n:type:ShaderForge.SFN_ValueProperty,id:7472,x:30372,y:32975,ptovrint:False,ptlb:Distortion V Speed,ptin:_DistortionVSpeed,varname:_DistortionVSpeed,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_Add,id:2765,x:30981,y:32819,varname:node_2765,prsc:2|A-3523-UVOUT,B-4009-OUT;n:type:ShaderForge.SFN_Lerp,id:2493,x:31858,y:32885,varname:node_2493,prsc:2|A-7303-UVOUT,B-405-OUT,T-4254-R;n:type:ShaderForge.SFN_Tex2d,id:4254,x:31692,y:33068,varname:_DistortionMask,prsc:2,tex:03e4ee8d0b5f45045bb12e2930ed4058,ntxv:0,isnm:False|UVIN-2765-OUT,TEX-2184-TEX;n:type:ShaderForge.SFN_TexCoord,id:7303,x:31681,y:32740,varname:node_7303,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_TexCoord,id:8661,x:31185,y:32621,varname:node_8661,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Power,id:7884,x:32442,y:33343,varname:node_7884,prsc:2|VAL-4069-R,EXP-2998-OUT;n:type:ShaderForge.SFN_Slider,id:2998,x:32058,y:33479,ptovrint:False,ptlb:Gradient Power,ptin:_GradientPower,varname:_GradientPower,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:2.214298,max:50;n:type:ShaderForge.SFN_Slider,id:2349,x:32383,y:32990,ptovrint:False,ptlb:Color Multiplier,ptin:_ColorMultiplier,varname:_ColorMultiplier,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:100;n:type:ShaderForge.SFN_Multiply,id:4032,x:32742,y:32915,varname:node_4032,prsc:2|A-2349-OUT,B-797-RGB,C-2390-RGB;n:type:ShaderForge.SFN_Tex2d,id:1383,x:32254,y:32392,varname:_DistortionMask_copy,prsc:2,tex:03e4ee8d0b5f45045bb12e2930ed4058,ntxv:2,isnm:False|TEX-2184-TEX;n:type:ShaderForge.SFN_Tex2dAsset,id:2184,x:31678,y:32353,ptovrint:False,ptlb:Mask,ptin:_Mask,varname:_Mask,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:03e4ee8d0b5f45045bb12e2930ed4058,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:9735,x:32653,y:32320,varname:node_9735,prsc:2|A-1383-RGB,B-6074-RGB;n:type:ShaderForge.SFN_FaceSign,id:3573,x:32742,y:33456,varname:node_3573,prsc:2,fstp:0;n:type:ShaderForge.SFN_Tex2d,id:2390,x:32620,y:32655,ptovrint:False,ptlb:Color Ramp,ptin:_ColorRamp,varname:_ColorRamp,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:789405d2635550c4c9df257b8e484c62,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:8020,x:31829,y:32038,varname:node_8020,prsc:2|A-9939-T,B-1465-OUT;n:type:ShaderForge.SFN_Time,id:9939,x:31574,y:31956,varname:node_9939,prsc:2;n:type:ShaderForge.SFN_Append,id:1465,x:31574,y:32134,varname:node_1465,prsc:2|A-3080-OUT,B-7985-OUT;n:type:ShaderForge.SFN_ValueProperty,id:7985,x:31376,y:32206,ptovrint:False,ptlb:Main Texture V Speed,ptin:_MainTextureVSpeed,varname:_MainTextureVSpeed,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_ValueProperty,id:3080,x:31376,y:32134,ptovrint:False,ptlb:Main Texture U Speed,ptin:_MainTextureUSpeed,varname:_MainTextureUSpeed,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_Add,id:902,x:32067,y:32245,varname:node_902,prsc:2|A-8020-OUT,B-9664-OUT;n:type:ShaderForge.SFN_Clamp,id:73,x:32742,y:33302,varname:node_73,prsc:2|IN-3573-VFACE,MIN-988-OUT,MAX-6926-OUT;n:type:ShaderForge.SFN_Vector1,id:6926,x:32566,y:33525,varname:node_6926,prsc:2,v1:1;n:type:ShaderForge.SFN_ValueProperty,id:988,x:32566,y:33469,ptovrint:False,ptlb:DoubleSided,ptin:_DoubleSided,varname:_DoubleSided,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;proporder:797-2390-2349-3080-7985-6074-9664-2998-4567-4497-4069-1571-8025-7472-2788-2184-988;pass:END;sub:END;*/

Shader "GAP/AdditiveMobileDistortionScroll" {
    Properties {
        _TintColor ("Color", Color) = (1,0.5342799,0.1764706,1)
        _ColorRamp ("Color Ramp", 2D) = "white" {}
        _ColorMultiplier ("Color Multiplier", Range(0, 100)) = 0
        _MainTextureUSpeed ("Main Texture U Speed", Float ) = 0
        _MainTextureVSpeed ("Main Texture V Speed", Float ) = 0
        _MainTexutre ("Main Texutre", 2D) = "white" {}
        [MaterialToggle] _DistortMainTexture ("Distort Main Texture", Float ) = 0
        _GradientPower ("Gradient Power", Range(0, 50)) = 2.214298
        _GradientUSpeed ("Gradient U Speed", Float ) = -0.2
        _GradientVSpeed ("Gradient V Speed", Float ) = -0.2
        _Gradient ("Gradient", 2D) = "white" {}
        _NoiseAmount ("Noise Amount", Range(-1, 1)) = 0.1144851
        _DistortionUSpeed ("Distortion U Speed", Float ) = 0.2
        _DistortionVSpeed ("Distortion V Speed", Float ) = 0
        _Distortion ("Distortion", 2D) = "white" {}
        _Mask ("Mask", 2D) = "white" {}
        _DoubleSided ("DoubleSided", Float ) = 1
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
            "PreviewType"="Plane"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="LightweightForward"
            }
            Blend One One
            Cull Off
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            //#define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma multi_compile_fog
			#pragma exclude_renderers d3d11_9x
            //#pragma only_renderers d3d11 glcore gles gles3 metal 
            #pragma target 2.0
            uniform sampler2D _MainTexutre; uniform float4 _MainTexutre_ST;
            uniform float4 _TintColor;
            uniform float _GradientUSpeed;
            uniform float _GradientVSpeed;
            uniform sampler2D _Gradient; uniform float4 _Gradient_ST;
            uniform float _NoiseAmount;
            uniform sampler2D _Distortion; uniform float4 _Distortion_ST;
            uniform fixed _DistortMainTexture;
            uniform float _DistortionUSpeed;
            uniform float _DistortionVSpeed;
            uniform float _GradientPower;
            uniform float _ColorMultiplier;
            uniform sampler2D _Mask; uniform float4 _Mask_ST;
            uniform sampler2D _ColorRamp; uniform float4 _ColorRamp_ST;
            uniform float _MainTextureVSpeed;
            uniform float _MainTextureUSpeed;
            uniform float _DoubleSided;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 vertexColor : COLOR;
                UNITY_FOG_COORDS(1)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
////// Lighting:
////// Emissive:
                float4 _DistortionMask_copy = tex2D(_Mask,TRANSFORM_TEX(i.uv0, _Mask));
                float4 node_9939 = _Time;
                float4 node_7842 = _Time;
                float2 node_2765 = (i.uv0+(node_7842.g*float2(_DistortionUSpeed,_DistortionVSpeed)));
                float4 _Distortion_var = tex2D(_Distortion,TRANSFORM_TEX(node_2765, _Distortion));
                float4 _DistortionMask = tex2D(_Mask,TRANSFORM_TEX(node_2765, _Mask));
                float2 node_2493 = lerp(i.uv0,lerp(i.uv0,float2(_Distortion_var.r,_Distortion_var.r),_NoiseAmount),_DistortionMask.r);
                float2 node_902 = ((node_9939.g*float2(_MainTextureUSpeed,_MainTextureVSpeed))+lerp( i.uv0, node_2493, _DistortMainTexture ));
                float4 _MainTexutre_var = tex2D(_MainTexutre,TRANSFORM_TEX(node_902, _MainTexutre));
                float4 _ColorRamp_var = tex2D(_ColorRamp,TRANSFORM_TEX(i.uv0, _ColorRamp));
                float4 node_9160 = _Time;
                float2 node_6774 = (node_2493+(node_9160.g*float2(_GradientUSpeed,_GradientVSpeed)));
                float4 _Gradient_var = tex2D(_Gradient,TRANSFORM_TEX(node_6774, _Gradient));
                float3 emissive = ((_DistortionMask_copy.rgb*_MainTexutre_var.rgb)*i.vertexColor.rgb*(_ColorMultiplier*_TintColor.rgb*_ColorRamp_var.rgb)*2.0*(_MainTexutre_var.a*pow(_Gradient_var.r,_GradientPower)*clamp(isFrontFace,_DoubleSided,1.0)));
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG_COLOR(i.fogCoord, finalRGBA, fixed4(0.5,0.5,0.5,1));
                return finalRGBA;
            }
            ENDCG
        }
    }
    CustomEditor "ShaderForgeMaterialInspector"
}
