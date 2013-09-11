using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Navigation;
using Caliburn.Micro;
using Microsoft.Phone.Controls;
using MundlTransit.WP8.Common;
using MundlTransit.WP8.Data.Reference;
using MundlTransit.WP8.Data.Runtime;
using MundlTransit.WP8.Resources;
using MundlTransit.WP8.ViewModels;
using MundlTransit.WP8.ViewModels.Lines;
using MundlTransit.WP8.ViewModels.Routing;
using MundlTransit.WP8.ViewModels.StationInfo;
using MundlTransit.WP8.ViewModels.Stations;
using MundlTransit.WP8.Services;
using MundlTransit.WP8.ViewModels.LineInfo;
using ReviewNotifier.Apollo;

namespace MundlTransit.WP8
{
    public class AppBootstrapper : PhoneBootstrapper
    {
        public static String appForceCulture = ""; // "qps-PLOC"; 

        PhoneContainer container;

        // http://caliburnmicro.codeplex.com/discussions/346663
        protected override PhoneApplicationFrame CreatePhoneApplicationFrame()
        {
            return new TransitionFrame();
        }

        async Task PerformAsyncInitializationsAsync()
        {
            await ReferenceDataContext.CopyDatabaseAsync().ConfigureAwait(continueOnCapturedContext: false);
            await ReferenceDataContext.DeletePreviousDatabasesAsync().ConfigureAwait(continueOnCapturedContext: false);
            await RuntimeDataContext.InitializeDatabaseAsync().ConfigureAwait(continueOnCapturedContext: false);
            await ReviewNotification.InitializeAsync().ConfigureAwait(continueOnCapturedContext: false);
        }

        protected override void Configure()
        {
            container = new PhoneContainer();

#if DEBUG
            LogManager.GetLog = type => new DebugLogger(type);
#endif

            container.RegisterPhoneServices(RootFrame);

            var initTasks = new Task(() => PerformAsyncInitializationsAsync());
            initTasks.RunSynchronously();

            container.RegisterPerRequest(typeof(IConfigurationService), null, typeof(DefaultConfigurationService));
            container.RegisterPerRequest(typeof(IDataService), null, typeof(DefaultDataService));
            container.RegisterPerRequest(typeof(ILocationService), null, typeof(DefaultLocationService));
            container.RegisterPerRequest(typeof(IUIService), null, typeof(DefaultUIService));

            // container.RegisterPerRequest(typeof(IEchtzeitdatenService), null, typeof(CreateCampEchtzeitdatenService));
            container.RegisterPerRequest(typeof(IEchtzeitdatenService), null, typeof(OgdEchtzeitdatenService));


            container.PerRequest<MainPageViewModel>();
            container.PerRequest<MenuViewModel>();
            container.PerRequest<TrafficInfoViewModel>();
            container.PerRequest<FavoritesViewModel>();

            container.PerRequest<StationsPivotPageViewModel>();
            container.PerRequest<StationsListViewModel>();
            container.PerRequest<StationsSearchViewModel>();
            container.PerRequest<NearbyStationsViewModel>();

            container.PerRequest<LinesPivotPageViewModel>();
            container.PerRequest<MetroViewModel>();
            container.PerRequest<TramViewModel>();
            container.PerRequest<BusViewModel>();
            container.PerRequest<NightBusViewModel>();

            container.PerRequest<LineInfoPageViewModel>();

            container.PerRequest<MapNearbyStationsPageViewModel>();

            container.PerRequest<StationInfoPivotPageViewModel>();
            container.PerRequest<DepartureViewModel>();

            container.PerRequest<RoutingPivotPageViewModel>();
            container.PerRequest<NewRouteViewModel>();
            container.PerRequest<StationSelectorViewModel>();

            container.PerRequest<TripsViewModel>();

            container.PerRequest<SettingsPageViewModel>();
            container.PerRequest<AboutPageViewModel>();

            AddCustomConventions();
        }

        protected override void PrepareApplication()
        {
            base.PrepareApplication();

            // Phone-specific initialization
            InitializePhoneApplication();

            // Language display initialization
            InitializeLanguage();
        }

        static void AddCustomConventions()
        {
            ConventionManager.AddElementConvention<Pivot>(Pivot.ItemsSourceProperty, "SelectedItem", "SelectionChanged").ApplyBinding =
                (viewModelType, path, property, element, convention) =>
                {
                    if (ConventionManager
                        .GetElementConvention(typeof(ItemsControl))
                        .ApplyBinding(viewModelType, path, property, element, convention))
                    {
                        ConventionManager
                            .ConfigureSelectedItem(element, Pivot.SelectedItemProperty, viewModelType, path);
                        ConventionManager
                            .ApplyHeaderTemplate(element, Pivot.HeaderTemplateProperty, null, viewModelType);
                        return true;
                    }

                    return false;
                };

            ConventionManager.AddElementConvention<Panorama>(Panorama.ItemsSourceProperty, "SelectedItem", "SelectionChanged").ApplyBinding =
                (viewModelType, path, property, element, convention) =>
                {
                    if (ConventionManager
                        .GetElementConvention(typeof(ItemsControl))
                        .ApplyBinding(viewModelType, path, property, element, convention))
                    {
                        ConventionManager
                            .ConfigureSelectedItem(element, Panorama.SelectedItemProperty, viewModelType, path);
                        ConventionManager
                            .ApplyHeaderTemplate(element, Panorama.HeaderTemplateProperty, null, viewModelType);
                        return true;
                    }

                    return false;
                };


            ConventionManager.AddElementConvention<DatePicker>(DatePicker.ValueProperty, "Value", "ValueChanged");
            ConventionManager.AddElementConvention<TimePicker>(TimePicker.ValueProperty, "Value", "ValueChanged");
        }

        protected override object GetInstance(Type service, string key)
        {
            return container.GetInstance(service, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance)
        {
            container.BuildUp(instance);
        }

        // Initialize the app's font and flow direction as defined in its localized resource strings.
        //
        // To ensure that the font of your application is aligned with its supported languages and that the
        // FlowDirection for each of those languages follows its traditional direction, ResourceLanguage
        // and ResourceFlowDirection should be initialized in each resx file to match these values with that
        // file's culture. For example:
        //
        // AppResources.es-ES.resx
        //    ResourceLanguage's value should be "es-ES"
        //    ResourceFlowDirection's value should be "LeftToRight"
        //
        // AppResources.ar-SA.resx
        //     ResourceLanguage's value should be "ar-SA"
        //     ResourceFlowDirection's value should be "RightToLeft"
        //
        // For more info on localizing Windows Phone apps see http://go.microsoft.com/fwlink/?LinkId=262072.
        //
        private void InitializeLanguage()
        {
            try
            {
                // See http://blogs.windows.com/windows_phone/b/wpdev/archive/2013/03/08/tips-for-localizing-windows-phone-apps-part-2.aspx, section Pseudolocalization support 
                if (Debugger.IsAttached && !String.IsNullOrWhiteSpace(appForceCulture))
                {
                    Thread.CurrentThread.CurrentCulture = new CultureInfo(appForceCulture);
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo(appForceCulture);
                }

                // Set the font to match the display language defined by the
                // ResourceLanguage resource string for each supported language.
                //
                // Fall back to the font of the neutral language if the Display
                // language of the phone is not supported.
                //
                // If a compiler error is hit then ResourceLanguage is missing from
                // the resource file.
                RootFrame.Language = XmlLanguage.GetLanguage(AppResources.ResourceLanguage);

                // Set the FlowDirection of all elements under the root frame based
                // on the ResourceFlowDirection resource string for each
                // supported language.
                //
                // If a compiler error is hit then ResourceFlowDirection is missing from
                // the resource file.
                FlowDirection flow = (FlowDirection)Enum.Parse(typeof(FlowDirection), AppResources.ResourceFlowDirection);
                RootFrame.FlowDirection = flow;
            }
            catch
            {
                // If an exception is caught here it is most likely due to either
                // ResourceLangauge not being correctly set to a supported language
                // code or ResourceFlowDirection is set to a value other than LeftToRight
                // or RightToLeft.

                if (Debugger.IsAttached)
                {
                    Debugger.Break();
                }

                throw;
            }
        }


        // Code to execute if a navigation fails
        private void RootFrame_NavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            if (Debugger.IsAttached)
            {
                // A navigation has failed; break into the debugger
                Debugger.Break();
            }
        }

        #region Phone application initialization

        // Avoid double-initialization
        private bool phoneApplicationInitialized = false;

        // Do not add any additional code to this method
        private void InitializePhoneApplication()
        {
            if (phoneApplicationInitialized)
                return;

            // Handle navigation failures
            RootFrame.NavigationFailed += RootFrame_NavigationFailed;

            // Handle reset requests for clearing the backstack
            RootFrame.Navigated += CheckForResetNavigation;

            // Ensure we don't initialize again
            phoneApplicationInitialized = true;
        }

        private void CheckForResetNavigation(object sender, NavigationEventArgs e)
        {
            // If the app has received a 'reset' navigation, then we need to check
            // on the next navigation to see if the page stack should be reset
            if (e.NavigationMode == NavigationMode.Reset)
                RootFrame.Navigated += ClearBackStackAfterReset;
        }

        private void ClearBackStackAfterReset(object sender, NavigationEventArgs e)
        {
            // Unregister the event so it doesn't get called again
            RootFrame.Navigated -= ClearBackStackAfterReset;

            // Only clear the stack for 'new' (forward) and 'refresh' navigations
            if (e.NavigationMode != NavigationMode.New && e.NavigationMode != NavigationMode.Refresh)
                return;

            // For UI consistency, clear the entire page stack
            while (RootFrame.RemoveBackEntry() != null)
            {
                ; // do nothing
            }
        }

        #endregion
    }
}
