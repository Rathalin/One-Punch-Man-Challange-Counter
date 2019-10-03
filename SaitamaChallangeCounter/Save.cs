using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SaitamaChallangeCounter
{
    public class Save : INotifyPropertyChanged
    {
        // Constructors
        
        public Save()
        {
            CounterLimit = 1000;
            CurrentDate = DateTime.Now;
        }

        // Methodes


        // Variables

        public event PropertyChangedEventHandler PropertyChanged;

        // Attributes

        [XmlIgnore]
        public int CounterLimit { get; set; }

        [XmlIgnore]
        private int _screenNr;

        [XmlElement(ElementName = "Screen")]
        public int ScreenNr
        {
            get { return _screenNr; }
            set
            {
                _screenNr = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ScreenNr"));
            }
        }

        [XmlIgnore]
        private DateTime _currentDate;
        [XmlIgnore]
        public DateTime CurrentDate
        {
            get { return _currentDate; }
            set
            {
                _currentDate = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CurrentDate"));
            }
        }

        [XmlIgnore]
        private int _countPushUps;
        [XmlElement(ElementName = "PushUps")]
        public int CountPushUps
        {
            get { return _countPushUps; }
            set
            {
                if (value > CounterLimit)
                {
                    _countPushUps = CounterLimit;
                }
                else if (value < 0)
                {
                    _countPushUps = 0;
                }
                else
                {
                    _countPushUps = value;
                }
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CountPushUps"));
            }
        }

        [XmlIgnore]
        private int _countSquats;
        [XmlElement(ElementName = "Squats")]
        public int CountSquats
        {
            get { return _countSquats; }
            set
            {
                if (value > CounterLimit)
                {
                    _countSquats = CounterLimit;
                }
                else if (value < 0)
                {
                    _countSquats = 0;
                }
                else
                {
                    _countSquats = value;
                }
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CountSquats"));
            }
        }

        [XmlIgnore]
        private int _countSitUps;
        [XmlElement(ElementName = "SitUps")]
        public int CountSitUps
        {
            get { return _countSitUps; }
            set
            {
                if (value > CounterLimit)
                {
                    _countSitUps = CounterLimit;
                }
                else if (value < 0)
                {
                    _countSitUps = 0;
                }
                else
                {
                    _countSitUps = value;
                }
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CountSitUps"));
            }
        }

        [XmlIgnore]
        private int _countRunning;
        [XmlElement(ElementName = "Running")]
        public int CountRunning
        {
            get { return _countRunning; }
            set
            {
                if (value > CounterLimit)
                {
                    _countRunning = CounterLimit;
                }
                else if (value < 0)
                {
                    _countRunning = 0;
                }
                else
                {
                    _countRunning = value;
                }
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CountRunning"));
            }
        }
    }
}
