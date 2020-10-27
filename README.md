# Dog Daycare  
![.NET Core](https://github.com/tscrypter/dotnet_exercise/workflows/.NET%20Core/badge.svg)  

### Description
Dog Daycare is a simple example application composed of an MVC application and a WebAPI service. Both apps have their own 
MySQL database, communicate with each other using Eureka for service registry, and can be monitored using a Spring Boot Admin. 
All necessary services can be started from the main directory with the following command:  
```bash
docker-compose up && docker-compose down
```


### Notes  
  - [ ] Scaffolding for MVC does not work on linux
    - https://github.com/dotnet/Scaffolding/issues/1418
    - https://github.com/dotnet/Scaffolding/issues/1393
    - https://github.com/dotnet/Scaffolding/issues/1384
  - [ ] Ambiguous Steeltoe documentation on connectors
    - https://docs.steeltoe.io/api/v3/connectors/mysql.html "Then add a reference to the appropriate Steeltoe connector NuGet package."
  - [x] Setting "Eureka__Client__ServiceUrl=http://eureka:8761" throws the following exception
    ```
    System.NullReferenceException: Object reference not set to an instance of an object.
       at Steeltoe.Discovery.Eureka.EurekaInstanceOptions.ApplyConfigUrls(List`1 addresses, String wildcard_hostname)
       at Steeltoe.Discovery.Eureka.EurekaPostConfigurer.UpdateConfiguration(IConfiguration config, EurekaInstanceOptions options, IApplicationInstanceInfo instanceInfo)
       at Steeltoe.Discovery.Eureka.EurekaPostConfigurer.UpdateConfiguration(IConfiguration config, EurekaServiceInfo si, EurekaInstanceOptions instOptions, IApplicationInstanceInfo appInfo)
       at Steeltoe.Discovery.Eureka.EurekaDiscoveryClientExtension.<>c__DisplayClass9_0.<ConfigureEurekaServices>b__1(EurekaInstanceOptions options)
       at Microsoft.Extensions.Options.PostConfigureOptions`1.PostConfigure(String name, TOptions options)
       at Microsoft.Extensions.Options.OptionsFactory`1.Create(String name)
       at Microsoft.Extensions.Options.OptionsMonitor`1.<>c__DisplayClass11_0.<Get>b__0()
       at System.Lazy`1.ViaFactory(LazyThreadSafetyMode mode)
       at System.Lazy`1.ExecutionAndPublication(LazyHelper executionAndPublication, Boolean useDefaultConstructor)
       at System.Lazy`1.CreateValue()
       at System.Lazy`1.get_Value()
       at Microsoft.Extensions.Options.OptionsCache`1.GetOrAdd(String name, Func`1 createOptions)
       at Microsoft.Extensions.Options.OptionsMonitor`1.Get(String name)
       at Microsoft.Extensions.Options.OptionsMonitor`1.get_CurrentValue()
       at Steeltoe.Discovery.Eureka.EurekaApplicationInfoManager.get_InstanceConfig()
       at Steeltoe.Discovery.Eureka.EurekaApplicationInfoManager..ctor(IOptionsMonitor`1 instConfig, ILoggerFactory logFactory)
    ```
    If not overriding the `ServiceUrl` property and running on the host network, the default setting works and the service registers with Eureka  
    :heavy_check_mark: Discovered this is fixed by adding a reference to `Steeltoe.Common.Hosting:3.0.1` and `UseCloudHosting(port)`