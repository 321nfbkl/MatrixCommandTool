using CommonServiceLocator;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using MatrixCommandTool.Net.TCP;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace MatrixCommandTool.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        private IServiceProvider mProvider;

        public void SetServiceProvider(IServiceProvider provider)
        {
            this.mProvider = provider;
        }

        public MainViewModel MainVM
        {
            get
            {
                using (var scop = this.mProvider.CreateScope())
                {
                    return scop.ServiceProvider.GetRequiredService<MainViewModel>();
                }
            }
        }

        public SetCommandViewModel SetCommandVM
        {
            get
            {
                using (var scop = this.mProvider.CreateScope())
                {
                    return scop.ServiceProvider.GetRequiredService<SetCommandViewModel>();
                }
            }
        }

        public NineOneMartixSettingViewModel NineOneMartixSettingVM
        {
            get
            {
                using (var scop = this.mProvider.CreateScope())
                {
                    return scop.ServiceProvider.GetRequiredService<NineOneMartixSettingViewModel>();
                }
            }
        }

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }

     
    }
}