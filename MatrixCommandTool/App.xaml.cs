using MatrixCommandTool.Net;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using NLog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using System.IO;
using System.Reflection;
using MatrixCommandTool.ViewModel;
using MatrixCommandTool.Net.TCP;
using MatrixCommandTool.Net.TCP.Request;

namespace MatrixCommandTool
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        public new static MainWindow MainWindow;

        /// <summary>
        /// 调用UI线程
        /// </summary>
        /// <param name="action"></param>
        /// <param name="isAsync"></param>
        public static void RunInUIThread(Action action, bool isAsync = false)
        {
            if (App.Current == null || App.Current.Dispatcher == null)
                return;
            if (!isAsync)
                App.Current?.Dispatcher.Invoke(action);
            else
                App.Current?.Dispatcher.InvokeAsync(action);
        }

        private static ILogger _logger = null;
        public static ILogger Logger => _logger;

        protected override void OnStartup(StartupEventArgs e)
        {

            #region 配置依赖注入
            GlobalContext.Current.SetVMLocator(Application.Current.FindResource("Locator") as ViewModel.ViewModelLocator);
            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddSingleton<ClientSocket>();
            serviceCollection.AddSingleton<DelegateFactory>();
            serviceCollection.AddSingleton<CustomProtocol>();
            serviceCollection.AddSingleton<ScanBoardListRequest>();
            serviceCollection.RegistViewModel();
            IServiceProvider serviceProvicer = serviceCollection.BuildServiceProvider();
            GlobalContext.Current.CurrentVMLocator.SetServiceProvider(serviceProvicer);
            #endregion

            base.OnStartup(e);

        }
    }

    public static class ServiceCollectionExpand
    {
        /// <summary>
        /// ViewModel绑定类依赖注入
        /// </summary>
        /// <param name="services"></param>
        public static void RegistViewModel(this IServiceCollection services)
        {
            services.AddSingleton<ViewModel.MainViewModel>();
            services.AddSingleton<ViewModel.SetCommandViewModel>();
            services.AddSingleton<NineOneMartixSettingViewModel>();
        }



        /// <summary>
        /// 注册所有请求信息
        /// </summary>
        /// <param name="service"></param>
        public static void RegisterRequest(this IServiceCollection service)
        {
            var assemblys = Assembly.Load("MatrixCommandTool.Net");
            var types = assemblys.GetTypes().Where(w => w.IsClass && w.IsPublic && !w.IsSealed && !w.IsAbstract && typeof(Net.TCP.Request.IRequestBase).IsAssignableFrom(w));
            if (types == null || types.Count() < 1)
                return;

            foreach (var item in types)
            {
                service.Add(new ServiceDescriptor(item, item, ServiceLifetime.Singleton));

                var interfaces = item.GetInterfaces();
                if (interfaces != null && interfaces.Length > 0)
                {
                    foreach (var interfaceType in interfaces)
                    {
                        service.Add(new ServiceDescriptor(interfaceType, item, ServiceLifetime.Singleton));
                    }
                }
            }
        }
    }

    }
