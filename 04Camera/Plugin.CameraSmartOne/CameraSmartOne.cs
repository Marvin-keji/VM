using HalconDotNet;
using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Media3D;
using VM.Start.Attributes;
using VM.Start.Common;
using VM.Start.Common.Enums;
using VM.Start.Common.Provide;
using VM.Start.Core;
using VM.Start.Models;
using System.Timers;
using System.Threading;
using Timer = System.Timers.Timer;
namespace Plugin.CameraSmartOne
{
    [Category("相机")]
    [DisplayName("SmartOne相机")]
    [Serializable]
    public class CameraSmartOne : CameraBase
    {
        [NonSerialized]
        private Stopwatch mStopwatch = new Stopwatch();

        
        /// <summary> >= Sfnc2_0_0,说明是ＵＳＢ３的相机</summary>
        [NonSerialized]
        Version Sfnc2_0_0 = new Version(2, 0, 0);
        private Timer timer;
        HTuple hv_Socket = new HTuple();
        private Bitmap _image = null;
        CameraInfoModel mCameraInfo;

        public CameraSmartOne() : base() { }

        private void InitializeTimer()
        {
            timer = new Timer(10); // 设置间隔时间为1000毫秒（1秒）
            timer.Elapsed += OnTimedEvent; // 定时器触发时调用的事件
            timer.AutoReset = true; // 设置为true表示重复触发，false为只触发一次
            timer.Enabled = true; // 启动定时器
        }
        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            // 这里编写定时器触发时需要执行的代码
            
            OnImageGrabbed();
        }
        /// <summary>搜索相机</summary>
        public override List<CameraInfoModel> SearchCameras()
        {
            List<CameraInfoModel> _CamInfoList = new List<CameraInfoModel>();

            HOperatorSet.GenEmptyObj(out ho_ImageInt2);
            mCameraInfo = new CameraInfoModel();
                mCameraInfo.SerialNO = "0";//[CameraInfoKey.SerialNumber];
                try
                {
                    mCameraInfo.CameraIP = "192.168.0.199";//[CameraInfoKey.DeviceIpAddress];
                }
                catch
                {
                    mCameraInfo.CameraIP = "00:00:00:00";
                }
                mCameraInfo.CamName = "SmartOne" + "0";// [CameraInfoKey.SerialNumber];
                mCameraInfo.MaskName = "SmartOne" + "0";// [CameraInfoKey.SerialNumber];
                mCameraInfo.Connected = false;
                _CamInfoList.Add(mCameraInfo);
           
            return _CamInfoList;
        }
        static HObject ho_ImageInt2 = null;
        /// <summary>
        /// 连接相机
        /// </summary>
        public override void ConnectDev()
        {
            try
            {
                base.ConnectDev();
                DisConnectDev();// 如果设备已经连接先断开
                //mCamera = HOperatorSet.();
                //if (mCamera == null) { return; }
                //mCamera.Open();

                //CameraIP = mCamera.CameraInfo.Version;
                try
                {
                    hv_Socket.Dispose();
                    HOperatorSet.OpenSocketConnect("127.0.0.1", 3000, new HTuple(), new HTuple(),
                        out hv_Socket);
                }
                catch (Exception e)
                {

                    MessageBox.Show(e.ToString());
                    return;
                }

                Thread.Sleep(100);
                /* 打开Software Trigger */
               
                InitializeTimer();
                /* 设置图像格式 */


                /* 设置缓存个数为8（默认值为16） */
                //mCamera.StreamGrabber.SetBufferCount(16);

                
                /* 注册码流回调事件 */
                //mCamera.StreamGrabber.ImageGrabbed += OnImageGrabbed;
                //mCamera.StreamGrabber.GrabStarted += OnGrabStarted;
                //mCamera.StreamGrabber.GrabStoped += OnGrabStopped;
                //mCamera.ConnectionLost += OnConnectionLost;
                /* 开启码流 */
                int times = 1;
            //reOpen:
            //    try
            //    {
            //        hv_Socket.Dispose();
            //        HOperatorSet.OpenSocketConnect(mCameraInfo.CameraIP, 3000, new HTuple(), new HTuple(),
            //            out hv_Socket);
            //        InitializeTimer();
            //    }
            //    catch (Exception e)
            //    {

            //        MessageBox.Show(e.ToString());
            //        return;
            //    }

                //if (mCamera.CameraInfo.Version < Sfnc2_0_0.)
                //{


                //}
                //else
                //{
                //    CameraIP = "00:00:00:00";
                //}
                //Framerate = mCamera.
                //WidthMax = (int)mCamera.Parameters[PLCamera.Width].GetValue();
                //HeightMax = (int)mCamera.Parameters[PLCamera.Height].GetValue();
                //if (mCamera.GetSfncVersion() < Sfnc2_0_0)
                //{
                //    GainMax = mCamera.Parameters[PLCamera.GainRaw].GetMaximum();
                //    GainMin = mCamera.Parameters[PLCamera.GainRaw].GetMinimum();
                //    ExposeTimeMax = mCamera.Parameters[PLCamera.ExposureTimeRaw].GetMaximum();
                //    ExposeTimeMin = mCamera.Parameters[PLCamera.ExposureTimeRaw].GetMinimum();
                //}
                //else
                //{
                //    GainMax = (long)mCamera.Parameters[PLUsbCamera.Gain].GetMaximum();
                //    GainMin = (long)mCamera.Parameters[PLUsbCamera.Gain].GetMinimum();
                //    ExposeTimeMax = (long)mCamera.Parameters[PLUsbCamera.ExposureTime].GetMaximum();
                //    ExposeTimeMin = (long)mCamera.Parameters[PLUsbCamera.ExposureTime].GetMinimum();
                //}
                //Width = Width == 0 ? WidthMax : Width;
                //Height = Height == 0 ? HeightMax : Height;
                //Gain = Gain == 0 ? GainMax - GainMin : Gain;
                //ExposeTime = ExposeTime == 0 ? ExposeTimeMin : ExposeTime;
                Connected = true;
            }
            catch (Exception ex)
            {
                Logger.AddLog(Remarks + ":" + "OPT Connect:" + ex.Message, eMsgType.Error);
                Connected = false;
            }
        }

        /// <summary>
        /// 断开相机
        /// </summary>
        public override void DisConnectDev()
        {
            try
            {
                //if (mCamera != null && mCamera.IsOpen)
                //{
                //    //mCamera.StreamGrabber.ImageGrabbed -= OnImageGrabbed;         /* 反注册回调 */
                //    mCamera.ShutdownGrab();                                       /* 停止码流 */
                //    mCamera.Close();
                    Connected = false;/* 关闭相机 */
                //}
                //if (mCamera != null)
                //{
                //    mCamera.Close();
                //    //mCamera.Dispose();
                //    mCamera = null;

                //}
            }
            catch (Exception ex)
            {
                Logger.AddLog(Remarks + ":" + "OPT DisConnect:" + ex.Message, eMsgType.Error);
            }
        }
        /// <summary>
        /// 采集图像
        /// </summary>
        /// <param name="byHand">是否手动采图</param>
        public override bool CaptureImage(bool byHand)
        {
            try
            {
                //if (mCamera == null || mCamera.IsOpen == false)
                //{
                //    ConnectDev();
                //    //if (mCamera == null || mCamera.IsOpen == false)
                //    //{
                //    //    Logger.AddLog(Remarks + ":" + "相机采集:重连失败!" + mCamera.ToString() + " " + mCamera.IsOpen.ToString(), eMsgType.Error);
                //    //    return false;
                //    //}
                //}
                if (byHand)
                {
                    //设置触发模式
                    SetTriggerMode(eTrigMode.软触发);
                    //拍一张
                    //mCamera.ExecuteSoftwareTrigger();
                    //mCamera.StreamGrabber.Start(1, GrabStrategy.OneByOne, GrabLoop.ProvidedByStreamGrabber);
                    //还原之前的触发模式
                    SetTriggerMode(TrigMode);
                }
                else
                {
                    if (TrigMode == eTrigMode.软触发)
                    {
                        //SignalWait.WaitOne();
                        //拍一张
                        //mCamera.ExecuteSoftwareTrigger();
                        //mCamera.Parameters[PLCamera.AcquisitionMode].SetValue(PLCamera.AcquisitionMode.SingleFrame);
                        //mCamera.StreamGrabber.Start(1, GrabStrategy.OneByOne, GrabLoop.ProvidedByStreamGrabber);
                        //SignalWait.Reset();
                    }
                    else
                    {
                        GetSignalWait.Reset();
                        //SetTriggerMode(TrigMode.软触发);
                        //拍一张
                        //mCamera.ExecuteSoftwareTrigger();
                        //mCamera.Parameters[PLCamera.AcquisitionMode].SetValue(PLCamera.AcquisitionMode.SingleFrame);
                        //mCamera.StreamGrabber.Start(1, GrabStrategy.OneByOne, GrabLoop.ProvidedByStreamGrabber);
                        GetSignalWait.WaitOne(2000);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                StopGrab();
                Logger.AddLog(Remarks + ":" + "OPT Image Capture:" + ex.Message, eMsgType.Error);
                if (TrigMode == eTrigMode.软触发)
                {
                    SignalWait.Set();
                }
                else
                {
                    GetSignalWait.Set();
                }
                return false;
            }
        }
        /// <summary>
        /// 未使用
        /// </summary>
        /// <param name="filePath"></param>
        public override void LoadSetting(string filePath)
        {
            base.LoadSetting(filePath);
        }
        /// <summary>
        /// 未使用
        /// </summary>
        /// <param name="filePath"></param>
        public override void SaveSetting(string filePath)
        {
            base.SaveSetting(filePath);
        }
        /// <summary> 相机设置</summary>
        public override void SetSetting()
        {
            SetWidth();
            SetHeight();
            SetGain((long)Gain);
            SetExposureTime((long)ExposeTime);
            SetTriggerMode(TrigMode);
        }
        public override void CameraChanged(ChangType changTyp)
        {
            try
            {
                switch (changTyp)
                {
                    case ChangType.增益:
                        SetGain((long)Gain);
                        break;
                    case ChangType.曝光:
                        SetExposureTime((long)ExposeTime);
                        break;
                    case ChangType.宽度:
                        SetWidth();
                        break;
                    case ChangType.高度:
                        SetHeight();
                        break;
                    case ChangType.触发:
                        SetTriggerMode(TrigMode);
                        break;
                }
            }
            catch (Exception ex)
            {
                Logger.AddLog(Remarks + ":" + "OPT:" + ex.Message);
            }
        }
        /// <summary>设置宽度</summary>
        public void SetWidth()
        {
            if (Width > 100 & Width <= WidthMax)
            {
                return;
               // mCamera.ParameterCollection[OPTParamSet.ExposureTime];
            }
        }
        /// <summary>设置高度</summary>
        public void SetHeight()
        {
            if (Height > 100 & Height <= WidthMax)
            {
                
                return ;
                // mCamera.ParameterCollection[OPTParamSet.ExposureTime];
            }
           
        }
        /// <summary>设置增益</summary>
        public void SetGain(long gainRaw)
        {
            try
            {
                /* 设置增益 */
                
                return ;
            }
            catch (Exception ex)
            {
                Logger.AddLog(Remarks + ":" + "OPT:" + ex.Message);
            }
        }
        /// <summary>设置相机曝光时间</summary>
        public void SetExposureTime(long value)
        {
            try
            {
               
            }
            catch (Exception ex)
            {
                Logger.AddLog(Remarks + ":" + "OPT:" + ex.Message);
            }
        }
        /// <summary>设置触发</summary>
        public override bool SetTriggerMode(eTrigMode _TrigMode)
        {
            try
            {
                //if (mCamera == null) return false;
                //如果是实时采集 则先停止
                //if (mCamera.StreamGrabber.IsStart )
                {
                    //StopGrab();
                }
                switch (_TrigMode)
                {
                    case eTrigMode.内触发:   // no acquisition
                        {
                            //mCamera.TriggerSet.Open(OPTTriggerSourceEnum.);
                           // mCamera.TriggerSet.Open(OPTTriggerSourceEnum.Line1);
                            StopGrab();
                            break;
                        }
                    case eTrigMode.软触发:   // freerunning
                        {
                            //mCamera.TriggerSet.Open(OPTTriggerSourceEnum.Software);
                            StartGrab();
                            break;
                        }
                    case eTrigMode.上升沿:   // Software trigger
                        {
                           // mCamera.TriggerSet.

                            //StopGrab();
                            break;
                        }
                    case eTrigMode.下降沿:   // Software trigger
                        {

                            //StopGrab();
                            break;
                        }
                }
                return true;
            }
            catch (Exception ex)
            {
                StopGrab();
                Logger.AddLog(Remarks + ":" + "OPT TrigMode:" + ex.Message);
                return false;
            }
        }

        public double ReadParaImageW()
        {
            /* 设置曝光 */
            return 0;

        }

        public double ReadParaImageH()
        {
            /* 设置曝光 */
            return 0;

        }

        public double ReadParaExposure()
        {
            /* 设置曝光 */
            return 0;

        }

        public double ReadParaGain()
        {
            /* 设置增益 */
            return 0;
        }

        public double ReadParaGama()
        {
            /* 设置伽马 */
            return 0;
        }
        #region 相机事件
        /// <summary>
        /// 断开连接时触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnConnectionLost(Object sender, EventArgs e)
        {
            // Close the mCamera object.
            DisConnectDev();
            Logger.AddLog(Remarks + ":" + "相机采集:断开连接!");
        }
        /// <summary>
        /// 开始触发时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnGrabStarted(Object sender, EventArgs e)
        {
            // Reset the stopwatch used to reduce the amount of displayed images. The mCamera may acquire images faster than the images can be displayed.
            mStopwatch.Reset();
        }
        /// <summary>
        /// 触发结束时
        /// </summary>
        private void OnGrabStopped(Object sender, EventArgs e)
        {
            // Reset the stopwatch.
            mStopwatch.Reset();
        }
        /// <summary>
        /// 采集回调
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnImageGrabbed()
        {

            if (ho_ImageInt2 != null)
            {
                ho_ImageInt2.Dispose();
            }
            try
            {
                HOperatorSet.ReceiveImage(out ho_ImageInt2, hv_Socket);
                Image = new HImage(ho_ImageInt2);
            }
            catch (Exception)
            {

                timer.Enabled = false;
            }
            
           
        }
        #endregion
        /// <summary>
        /// 开始实时采集
        /// </summary>
        public void StartGrab()
        {
            //mCamera.StreamGrabber.Start();
            //mCamera.Parameters[PLCamera.AcquisitionMode].SetValue(PLCamera.AcquisitionMode.Continuous);
            //mCamera.StreamGrabber.Start(GrabStrategy.OneByOne, GrabLoop.ProvidedByStreamGrabber);
        }
        /// <summary>
        /// 停止实时采集
        /// </summary>
        public void StopGrab()
        {
            //mCamera.StreamGrabber.Stop();
        }
        [OnDeserializing()]
        internal void OnDeSerializingMethod(StreamingContext context)
        {
            mStopwatch = new Stopwatch();
            //mConverter = new PixelDataConverter();
        }

        public  HObject BitmapToHObjectColor(Bitmap bmp)
        {
            if (bmp == null)
            {
                return null;
            }
            HObject halcon_image = new HObject();
            HOperatorSet.GenEmptyObj(out halcon_image);
            try
            {
                Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
                BitmapData bmpData = bmp.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
                HOperatorSet.GenImageInterleaved(out halcon_image, bmpData.Scan0, "bgr", bmp.Width, bmp.Height, -1,
                    "byte", bmp.Width, bmp.Height, 0, 0, -1, 0);
                bmp.UnlockBits(bmpData);

            }
            catch (System.Exception exp)
            {
                return null;
            }

            return halcon_image;

        }
    }
}
