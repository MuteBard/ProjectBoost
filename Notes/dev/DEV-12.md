# DEV-12, Using Invoke
#### Tags: [Side stuff, Invoke]


## Level 3

+ dealt with alot of stuff here on the side for fun

Not yet started

## Invoke()

+ Dunctions similarly to setTimeout in JS. 

        Using Invode allows us to call a method so it executes after a delay of x seconds.

+ Syntax

        Invoke("MethodName", delayInSeconds);

+ Pros, Quick and easy to use
+ Cons, not as performant as using a Coroutine

## Scripts

Added file ColorHexa.cs for color management

        using System.Linq;
        using System.Collections;
        using System.Collections.Generic;
        using UnityEngine;

        public class ColorHexa : MonoBehaviour
        {   
                [SerializeField] string DefautHexColor = "";
                [SerializeField] string HexColor;
                void Start(){
                        if(!DefautHexColor.Equals("")){
                        SetDefaultColor();
                        }
                }

                private void SetHexColors(string color){
                        var shapes = GetComponentsInChildren<MeshRenderer>();
                        var lights = GetComponentsInChildren<Light>();
                        if(shapes.Length > 0){
                        var newCubes = shapes.Select(shape => { 
                                shape.material.color = ApplyHexColor(color);
                                return shape; 
                        }).ToList();
                        }
                        if(lights.Length > 0){
                        var newLights = lights.Select(light =>{ 
                                light.color = ApplyHexColor(color);
                                return light; 
                        }).ToList();
                        }
                }

                private Color ApplyHexColor(string hexcode){
                        if(hexcode[0].Equals("#") && hexcode.Length == 7){
                        return new Color(0, 0, 0, 0);
                        }
                        string color1 = hexcode.Substring(1,2);
                        string color2 = hexcode.Substring(3,2);
                        string color3 = hexcode.Substring(5,2);
                        float number1 = (float) System.Convert.ToInt32(color1, 16);
                        float number2 = (float) System.Convert.ToInt32(color2, 16);
                        float number3 = (float) System.Convert.ToInt32(color3, 16);
                        return new Color(number1, number2, number3, 0);
                }

                public void SetColor(string hexcode){
                        HexColor = hexcode;
                        SetHexColors(HexColor);
                }

                public void SetDefaultColor(){
                        SetHexColors(DefautHexColor);
                }
        }
