using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace Relevamiento.Clases
{
    public static class CheckNetworkState
    {
        public static bool hasConnectivity = false;
        private static bool isAlreadyInitialized = false;

        public static void StartListening()
        {
            if (!isAlreadyInitialized)
            {
                Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
                isAlreadyInitialized = true;
            }

            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
                hasConnectivity = true;
        }

        public static void StopListening()
        {
            if (isAlreadyInitialized)
            {
                Connectivity.ConnectivityChanged -= Connectivity_ConnectivityChanged;
                isAlreadyInitialized = false;
            }
        }

        static void Connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            hasConnectivity = false;

            if (e.NetworkAccess == NetworkAccess.Internet)
            {
                hasConnectivity = true;
            }
        }
    }
}
