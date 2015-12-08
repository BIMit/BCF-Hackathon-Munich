using System;
using System.Collections.Generic;
using System.Text;

namespace ConnectBCF.Client.ViewModels
{
    public class BaseViewModel
    {
        public ConnectBCFService Service { get; set; }

        public BaseViewModel(ConnectBCFService service)
        {
            Service = service;
        }
    }
}
