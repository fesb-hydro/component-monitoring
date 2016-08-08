using System.ComponentModel;
using System.IO.Ports;
using Fesb.Hydrocontest.Domain.Entities;
using Fesb.Hydrocontest.Infrastructure.Services;

namespace Fesb.Hydrocontest.Presentation
{
    public class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private readonly ILoggerService _loggerService;

        private Radio _radio;
        private Motor _motor;
        private Boat  _boat;

        public ViewModel()
        {
            _loggerService = new LoggerService();
            ShouldBeLogged = false;
        }

        public bool ShouldBeLogged { get; set; }

        public Radio Radio
        {
            get { return _radio; }
            set { _radio = value; NotifyPropertyChanged("Radio"); }
        }

        public Motor Motor
        {
            get { return _motor; }
            set { _motor = value; NotifyPropertyChanged("Motor"); }
        }

        public Boat Boat
        {
            get { return _boat; }
            set { _boat = value; NotifyPropertyChanged("Boat"); }
        }

        public string[] AvailableSerialPorts => SerialPort.GetPortNames();

        protected virtual void NotifyPropertyChanged(string invoker)
        {
            if(ShouldBeLogged) _loggerService.LogToCsvFile(_radio, _motor, _boat);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(invoker));
        }
    }
}
