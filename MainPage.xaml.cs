using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Devices.Gpio;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Composition;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace ConnectedBlinky
{
    public sealed partial class MainPage : Page
    {
        private Task _run;

        public MainPage()
        {
            this.InitializeComponent();

            Func<Task> runner = async () =>
            {
                var controller = await GpioController.GetDefaultAsync();

                var pin17 = controller.OpenPin(17);
                var pin18 = controller.OpenPin(18);

                pin17.SetDriveMode(GpioPinDriveMode.Output);
                pin18.SetDriveMode(GpioPinDriveMode.Output);

                var value = GpioPinValue.Low;

                while (true)
                {
                    pin17.Write(value);
                    value = value == GpioPinValue.Low ? GpioPinValue.High : GpioPinValue.Low;
                    pin18.Write(value);
                    await Task.Delay(1000);
                }
            };

            _run = runner();
        }
    }
}
