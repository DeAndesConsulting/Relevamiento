using Relevamiento.Models;
using Relevamiento.Services.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Relevamiento.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private SynchronizeDataService _synchronizeDataService;

        public SynchronizeDataConfig Config;

        public LoginViewModel()
        {
            _synchronizeDataService = new SynchronizeDataService();

            Config = _synchronizeDataService.GetSynchronizeDataConfig();
        }
    }
}
