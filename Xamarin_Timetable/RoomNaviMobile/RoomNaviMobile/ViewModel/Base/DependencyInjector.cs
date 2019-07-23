using RoomNaviMobile.Services.Settings;
using System;
using System.Collections.Generic;
using System.Text;
using TinyIoC;
using Xamarin.Forms;
using System.Globalization;
using System.Reflection;
using RoomNaviMobile.Services;
using RoomNaviMobile.Services.Dialog;
using RoomNaviMobile.Services.Subject;

namespace RoomNaviMobile.ViewModel.Base
{
    public static class DependencyInjector
    {
        private static TinyIoCContainer _container;

        static DependencyInjector()
        {
            _container = new TinyIoCContainer();

            // View models - by default, TinyIoC will register concrete classes as multi-instance.
            _container.Register<MainViewModel>();
            _container.Register<LoginViewModel>();
            _container.Register<SubjectViewModel>();
            _container.Register<LecturerViewModel>();
            _container.Register<TimetableViewModel>();
            _container.Register<LecturerTimetableViewModel>();

            // Services - by default, TinyIoC will register interface registrations as singletons.

            _container.Register<IDialogService, DialogService>();
            _container.Register<ISettingsService, SettingsService>();
            _container.Register<ISubjectService, SubjectService>();

        }

        public static void UpdateDependencies(bool useMockServices)
        {

            if (useMockServices)
            {
                _container.Register<ISubjectService, SubjectMockService>();
            }
            else
            {
                _container.Register<ISubjectService, SubjectService>();
            }
        }

        public static void RegisterSingleton<TInterface, T>() where TInterface : class where T : class, TInterface
        {
            _container.Register<TInterface, T>().AsSingleton();
        }

        public static T Resolve<T>() where T : class
        {
            return _container.Resolve<T>();
        }
    }
}
