using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO.Ports;
using System.Linq;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using Fesb.Hydrocontest.Infrastructure.Extensions;
using Fesb.Hydrocontest.Infrastructure.Helpers;
using Fesb.Hydrocontest.Infrastructure.Parsers;
using Fesb.Hydrocontest.Infrastructure.Services;
using MahApps.Metro.Controls;

namespace Fesb.Hydrocontest.Presentation
{
    public partial class MainWindow : MetroWindow
    {
        private readonly ViewModel         _viewModel;
        private readonly InputQueueService _inputQueueService;
        private SerialPort _serialPort;

        public MainWindow()
        {
            _viewModel = new ViewModel();
            DataContext = _viewModel;

            _inputQueueService = new InputQueueService();

            InitializeComponent();
            StopLoggingButton.IsEnabled = false;
        }

        ~MainWindow()
        {
            if(_serialPort.IsOpen) _serialPort.Close();
        }

        private void GetNewData(object sender, ElapsedEventArgs elapsedEventArgs)
        {
            var allIds = new[] {typeof(RadioIds), typeof(MotorIds), typeof(BoatIds)};

            allIds
                .ToList()
                .ForEach(x =>
                    Enum.GetValues(x)
                        .Cast<int>()
                        .ToList()
                        .ForEach(UpdateProperty)
                );
        }

        private void UpdateProperty(int id)
        {
            var map = new Dictionary<QueueStatuses, Action<OutputDto>>()
            {
                { QueueStatuses.NoChanges,      (outputDto) => {                                     } },
                { QueueStatuses.ChangesToRadio, (outputDto) => { _viewModel.Radio = outputDto.Radio; } },
                { QueueStatuses.ChangesToMotor, (outputDto) => { _viewModel.Motor = outputDto.Motor; } },
                { QueueStatuses.ChangesToBoat,  (outputDto) => { _viewModel.Boat  = outputDto.Boat;  } },
            };

            var result = ProcessProperty(id);

            map[result.Status](result.Output);
        }

        private Result<QueueStatuses, OutputDto> ProcessProperty(int id)
        {
            var bytes = BitConverter.GetBytes(id);
            _serialPort.Write(
                buffer: bytes, 
                offset: 0,
                count:  1
            );
            var newValue = Convert.ToInt32(_serialPort.ReadLine());
            return _inputQueueService.Enqueue(new InputDto() { Id = id, Value = newValue });
        }

        private void PortSelectionChanged(object sender, SelectionChangedEventArgs selectionChangedEvent)
        {
            var splitButton = sender as SplitButton;
            if (splitButton.IsNull()) return;
            if (_serialPort.IsNotNull() && _serialPort.IsOpen) _serialPort.Close();

            _serialPort = new SerialPort(
                portName: _viewModel.AvailableSerialPorts[splitButton.SelectedIndex],
                baudRate: Convert.ToInt32(ConfigurationManager.AppSettings["Arduino:BaudRate"])
            );

            _serialPort.Open();

            var timer = new Timer()
            {
                Enabled   = true,
                AutoReset = true,
                Interval  = TimeSpan.FromSeconds(Convert
                    .ToDouble(RefreshRateInSeconds.Text, CultureInfo.InvariantCulture))
                    .TotalMilliseconds
            };
            timer.Elapsed += GetNewData;
        }
        private void StartLogging(object sender, RoutedEventArgs routedEventArgs)
        {
            _viewModel.ShouldBeLogged = true;

            TriggerButtonState(StartLoggingButton);
            TriggerButtonState(StopLoggingButton);
        }
        private void StopLogging(object sender, RoutedEventArgs routedEventArgs)
        {
            _viewModel.ShouldBeLogged = false;

            TriggerButtonState(StopLoggingButton);
            TriggerButtonState(StartLoggingButton);
        }
        private void TriggerButtonState(Button button) => button.IsEnabled = !button.IsEnabled;
    }
}
