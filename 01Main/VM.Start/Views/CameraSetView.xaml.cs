using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using VM.Halcon;
using VM.Start.Common;
using VM.Start.Core;
using VM.Start.Dialogs.Views;
using VM.Start.Events;
using VM.Start.Models;
using VM.Start.Services;
using VM.Start.ViewModels;

namespace VM.Start.Views
{
    /// <summary>
    /// SystemParamView.xaml 的交互逻辑
    /// </summary>
    public partial class CameraSetView : MetroWindow
    {
        #region Singleton
        private static CameraSetView _instance = new CameraSetView();

        private CameraSetView()
        {
            InitializeComponent();
            this.DataContext = CameraSetViewModel.Ins;

        }
        public static CameraSetView Ins
        {
            get { return _instance; }
        }

        #endregion

        #region Prop
        public VMHWindowControl mWindowH;
        #endregion

        #region Method

        public bool IsClosed { get; set; } = true;

        protected override void OnClosing(CancelEventArgs e)
        {
            EventMgrLib.EventMgr.Ins.GetEvent<HardwareChangedEvent>().Publish();
            if (CameraSetViewModel.Ins.Timer_ContinuousAcq.Enabled)
            {
                CameraSetViewModel.Ins.Timer_ContinuousAcq.Stop();
            }
            e.Cancel = true;  // cancels the window close
            IsClosed = true;
            this.Hide();      // Programmatically hides the window
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void window_Loaded(object sender, RoutedEventArgs e)
        {
            if (mWindowH == null)
            {
                mWindowH = new VMHWindowControl();
                winFormHost.Child = mWindowH;
            }
        }
        private void cmbCameraType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbCameraType.SelectedIndex < 0) return;
           
           
                PluginsInfo pluginsInfo = PluginService.PluginDic_Camera[cmbCameraType.SelectedItem.ToString()];
                CameraBase module = (CameraBase)Activator.CreateInstance(pluginsInfo.ModuleType);
                CameraSetViewModel.Ins.CameraNos = module.SearchCameras();
                cmbCameraNo.ItemsSource = CameraSetViewModel.Ins.CameraNos;
                cmbCameraNo.SelectedIndex = 0;
           
        }
        private void dg_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if (CameraSetViewModel.Ins.SelectedCameraModel == null) return;
        }


        #endregion

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            if (cmbCameraType.SelectedItem.ToString() == "SmartOne相机")
            {

           
            var dialog = new IPInputDialog();
            dialog.Owner = this; // 设置当前窗口为弹窗的所有者

            // 显示弹窗（模式对话框）
            dialog.ShowDialog();

            // 获取输入的IP地址
            if (!string.IsNullOrEmpty(dialog.IPAddress))
            {
                MessageBox.Show($"您输入的IP地址是: {dialog.IPAddress}");
                // 在这里处理获取到的IP地址

            }
            else
            {
                MessageBox.Show("操作已取消");
            }
            }
        }
    }
}
