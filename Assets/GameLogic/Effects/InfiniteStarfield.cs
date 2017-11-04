using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.GameLogic.Effects
{
    [RequireComponent(typeof(ParticleSystem))]
    public class InfiniteStarfield : MonoBehaviour
    {
        //==============================================================================
        //
        //                                    CONSTANTS
        //
        //==============================================================================



        /// <summary>
        /// What method this starfield should use for coloring the star particles
        /// </summary>
        public enum StarColorGenerationMethod
        {
            /// <summary>
            /// All stars are white
            /// </summary>
            AllWhite,

            /// <summary>
            /// Stars are a random color, saturation, and value
            /// </summary>
            RandomColor,

            /// <summary>
            /// Stars are colored a specific Hue, Saturation, and Value
            /// </summary>
            SpecificColor
        }




        //==============================================================================
        //
        //                                    PROPERTIES
        //
        //==============================================================================



        [Header("Starfield Configuration")]
        [Range(0, 5000)]
        public int Maximum_Stars = 500;



        /// <summary>
        /// The physical size of a star particle
        /// </summary>
        [Range(0.1f, 25f)]
        public float Star_Size = 3.5f; //ToDo: Old scene had this as default 60



        /// <summary>
        /// The maximum distance a star is allowed to be rendered from the camera before it is culled.
        /// </summary>
        [Range(1f, 2000f)]
        public float Max_Distance_From_Camera = 10f;



        /// <summary>
        /// How close the particles can get to the camera before being clipped (hidden)
        /// </summary>
        [Range(1f, 2000f)]
        public float Camera_Clip_Distance = 2f;



        /// <summary>
        /// How far a star can stretch when moving fast
        /// </summary>
        public float Maximum_Star_Stretch_Multiplier = 250f;



        /// <summary>
        /// Which method of coloring the stars is used when they are first spawned.
        /// </summary>
        public StarColorGenerationMethod Star_Color_Generation_Method = StarColorGenerationMethod.RandomColor;



        #region Particle System

        /// <summary>
        /// Particle System that manages all of the star particles
        /// </summary>
        protected ParticleSystem _ParticleSystem = null;



        /// <summary>
        /// The actual particles being used in the particle system
        /// </summary>
        protected ParticleSystem.Particle[] _Particles;



        /// <summary>
        /// Stores the physical size of each star particle
        /// </summary>
        protected float[] _ParticleSizes;

        #endregion




        //==============================================================================
        //
        //                                UNITY LIFECYCLE
        //
        //==============================================================================



        // Use this for initialization
        public void TestInit()
        {
            #region Determine Weather to render the Particle System or not

            //ToDo: Adjust star amounts based on graphics capabilities

            if (Maximum_Stars <= 0)
            {
                Disable();
            }

            #endregion


            #region Setup Particle System

            this._ParticleSystem = this.GetComponent<ParticleSystem>();



            #endregion

            _SpawnStars(StarColorGenerationMethod.RandomColor);
        }





        //==============================================================================
        //
        //                                    METHODS
        //
        //==============================================================================



        public void Enable()
        {
            this.gameObject.SetActive(true);
        }



        public void Disable()
        {
            this.gameObject.SetActive(false);
        }




        public void UpdateStarfield()
        {
            if (!_CameraExists())
                return;

            //Cached magnitutde
            float ShipVelocityMagnitude = GameLoop.MAIN.CameraManager.CurrentCameraController.Velocity_Current.magnitude;

            //The inverted percentage of 1m/s of velocity that the camera is currently traveling
            float ZeroToOne = 1f- Mathf.Clamp(ShipVelocityMagnitude / 1f, 0f, 0.9f);

            //The scale at which each star will stretch based on the current velocity
            //ToDo: Clamp max stretch length
            float StarStretchLength = (ShipVelocityMagnitude * Maximum_Star_Stretch_Multiplier * Star_Size) + 1f;

            if (this._Particles != null && this._Particles.Length >= Maximum_Stars)
            {
                for(int i = 0; i < Maximum_Stars; i++)
                {
                    //Move particles in the opposite direction that the camera is moving
                    this._Particles[i].position -= GameLoop.MAIN.CameraManager.CurrentCameraController.Velocity_Current;

                    //Cull particles outside of camera distance and respawn them
                    if ((this._Particles[i].position - GameLoop.MAIN.CameraManager.Camera_Current.transform.position).magnitude > Max_Distance_From_Camera)
                    {
                        //ToDo: only spawn on forwards direction
                        _Particles[i].position = (Random.insideUnitSphere * Max_Distance_From_Camera) + GameLoop.MAIN.CameraManager.Camera_Current.transform.position;
                    }

                    //Billboard particles to the camera
                    if (ShipVelocityMagnitude > 0.001f)
                        this._Particles[i].rotation3D = Quaternion.LookRotation(GameLoop.MAIN.CameraManager.CurrentCameraController.Velocity_Current, GameLoop.MAIN.CameraManager.CurrentCameraController.gameObject.transform.up).eulerAngles;

                    //Use animation principals of squash and stretch on the Particle size according to the velocity
                    _Particles[i].startSize3D = new Vector3(_ParticleSizes[i] * ZeroToOne, _ParticleSizes[i] * ZeroToOne, _ParticleSizes[i] * StarStretchLength);

                    //Hide particles close to the camera
                    /*if ((_Particles[i].position - GameLoop.MAIN.CameraManager.Camera_Current.transform.position).sqrMagnitude <= Camera_Clip_Distance)
                    {
                        float perc = (_Particles[i].position - GameLoop.MAIN.CameraManager.Camera_Current.transform.position).sqrMagnitude / Camera_Clip_Distance;

                        //this.Particles[i].startColor = new Color(this.Particles[i].startColor.r, 1, 1, 1);

                        var t = _Particles[i].startColor;
                        t.a = (byte)System.Math.Round(perc * byte.MaxValue);
                        _Particles[i].startColor = t;

                        //this.Particles[i].startSize = perc * StarSize;
                        _Particles[i].startSize3D = new Vector3(Star_Size * perc, Star_Size * perc, (Star_Size + MaxStarLength) * perc);
                    }
                    else
                    {
                        //Scale the Particle size according to the velocity
                        _Particles[i].startSize3D = new Vector3(Star_Size, Star_Size, Star_Size * MaxStarLength);
                    }*/
                }

                //This is the LAST THING we do, which is to update the particle system with the new data we set in the particles this frame
                this._ParticleSystem.SetParticles(this._Particles, this._Particles.Length);
            }
        }



        /// <summary>
        /// Check if the camera exists in the scene, and display an error if it does not
        /// </summary>
        /// <returns>Returns false if there is any problem getting a reference to the camera</returns>
        protected bool _CameraExists()
        {
            if (GameLoop.MAIN == null || GameLoop.MAIN.CameraManager == null || GameLoop.MAIN.CameraManager.Camera_Current == null)
            {
                Debug.Log("Cannot create Starfield; The current camera could not be retrieved from the camera manager in the scene.");
                return false;
            }

            return true;
        }



        /// <summary>
        /// Spawn a new set of stars in the starfield
        /// </summary>
        /// <param name="inColorGenMethod"></param>
        protected void _SpawnStars(StarColorGenerationMethod inColorGenMethod = StarColorGenerationMethod.RandomColor)
        {
            if (!_CameraExists())
                return;

            if(GameLoop.MAIN == null || GameLoop.MAIN.CameraManager == null || GameLoop.MAIN.CameraManager.Camera_Current == null)
            {
                Debug.Log("Cannot create Starfield; The current camera could not be retrieved from the camera manager in the scene.");
                return;
            }

            if (Maximum_Stars < 1)
            {
                Debug.LogError("Cannot create Starfield; Need at least a value of 1 for Maximum_Stars in the system.  Current number of maximum stars: " + Maximum_Stars);
                return;
            }

            this._Particles = new ParticleSystem.Particle[Maximum_Stars];
            this._ParticleSizes = new float[Maximum_Stars];

            Color StarColor = new Color(1,1,1,0.5f);

            if(inColorGenMethod == StarColorGenerationMethod.SpecificColor)
                Debug.LogWarning("Specific Coloring of Stars is not yet supported, defaulting to white.");

            float RandSize = 0f;

            for (int i=0;i<Maximum_Stars;i++)
            {
                _Particles[i].position = (Random.insideUnitSphere * Max_Distance_From_Camera) + GameLoop.MAIN.CameraManager.Camera_Current.transform.position;

                switch (inColorGenMethod)
                {
                    case StarColorGenerationMethod.RandomColor:
                        //StarColor = new Color(Random.value, Random.value, Random.value);
                        StarColor = new Color(1f - (Random.value * 0.5f), 1f - (Random.value * 0.5f), 1f - (Random.value * 0.5f));
                        break;

                    case StarColorGenerationMethod.SpecificColor:
                        //ToDo: StarColor = inStarColor;
                        break;

                    case StarColorGenerationMethod.AllWhite:
                    default:
                        break;
                }

                _Particles[i].startColor = StarColor;

                //Generate a random star size using this curve: https://www.desmos.com/calculator/zh1cbauj4y
                RandSize = -Mathf.Pow(((Random.value * 3f) - 1), 3f) + 8f;

                _ParticleSizes[i] = RandSize;
                _Particles[i].startSize3D = new Vector3(RandSize, RandSize, RandSize);
            }
        }
    }
}