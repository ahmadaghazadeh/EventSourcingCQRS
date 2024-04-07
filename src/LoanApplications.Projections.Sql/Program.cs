using Autofac;
using Autofac.Extensions.DependencyInjection;
using LoanApplications.Projections.Sql;
using LoanManagement.Projections.Sql;

IHost host = Host.CreateDefaultBuilder(args)
    .UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>(autofac =>
    {
        autofac.RegisterModule<ProjectionModule>();
    })
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();
    })
    
    .Build();

host.Run();
