using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


namespace VM.Start.Dialogs.Views
    {
        public partial class IPInputDialog : Window
        {
            public IPInputDialog()
            {
                InitializeComponent();
                DataContext = new IPInputViewModel();

                // 设置关闭窗口的事件处理
                (DataContext as IPInputViewModel).RequestClose += (s, e) => Close();
            }

            public string IPAddress
            {
                get { return (DataContext as IPInputViewModel)?.IPAddressStr; }
            }
        }

        public class IPInputViewModel : ViewModelBase
        {
            private string _ipAddress;
            private string _validationError;

            public string IPAddressStr
            {
                get { return _ipAddress; }
                set
                {
                    _ipAddress = value;
                    ValidateIPAddress();
                    OnPropertyChanged(nameof(IPAddressStr));
                }
            }

            public string ValidationError
            {
                get { return _validationError; }
                set
                {
                    _validationError = value;
                    OnPropertyChanged(nameof(ValidationError));
                    OnPropertyChanged(nameof(IsValid));
                }
            }

            public bool IsValid => string.IsNullOrEmpty(ValidationError);

            public ICommand OKCommand { get; }
            public ICommand CancelCommand { get; }

            public event EventHandler RequestClose;

            public IPInputViewModel()
            {
                OKCommand = new RelayCommand(OnOK, CanOK);
                CancelCommand = new RelayCommand(OnCancel);
            }

            private void ValidateIPAddress()
            {
                if (string.IsNullOrWhiteSpace(IPAddressStr))
                {
                    ValidationError = "IP地址不能为空";
                    return;
                }
               
            if (!IPAddress.TryParse(IPAddressStr, out _))
                {
                    ValidationError = "请输入有效的IP地址";
                    return;
                }

                ValidationError = null;
            }

            private bool CanOK(object parameter)
            {
                return IsValid;
            }

            private void OnOK(object parameter)
            {
                RequestClose?.Invoke(this, EventArgs.Empty);
            }

            private void OnCancel(object parameter)
            {
                // 清空IP地址表示取消操作
                IPAddressStr = null;
                RequestClose?.Invoke(this, EventArgs.Empty);
            }
        }

        // MVVM辅助类
        public class ViewModelBase : System.ComponentModel.INotifyPropertyChanged
        {
            public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

            protected void OnPropertyChanged(string propertyName)
            {
                PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }

        public class RelayCommand : ICommand
        {
            private readonly Action<object> _execute;
            private readonly Predicate<object> _canExecute;

            public event EventHandler CanExecuteChanged
            {
                add { CommandManager.RequerySuggested += value; }
                remove { CommandManager.RequerySuggested -= value; }
            }

            public RelayCommand(Action<object> execute, Predicate<object> canExecute = null)
            {
                _execute = execute ?? throw new ArgumentNullException(nameof(execute));
                _canExecute = canExecute;
            }

            public bool CanExecute(object parameter)
            {
                return _canExecute == null || _canExecute(parameter);
            }

            public void Execute(object parameter)
            {
                _execute(parameter);
            }
        }
    }

