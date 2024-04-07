using Autofac;
using Framework.Core;
using Framework.Core.Events;
using Framework.Persistence.ES;
using LoanApplications.Projections.Sql.Framework;
using LoanApplications.Projections.Sql.Handlers;
using LoanApplications.Projections.Sql.Handlers.Model;
using LoanManagement.Projections.Sql.Framework;

namespace LoanApplications.Projections.Sql
{
	public class ProjectionModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterType<AutofacEventBus>().As<IEventBus>();
			builder.RegisterType<FakeCursor>().As<ICursor>();
			builder.RegisterType<EventTypeResolver>().As<IEventTypeResolver>();
			builder.RegisterGenericDecorator(typeof(CursorAwareHandler<>), typeof(IEventHandler<>));

			//**********--------------

			builder.RegisterAssemblyTypes(typeof(LoanRequestedHandler).Assembly)
				.As(type => type.GetInterfaces()
					.Where(interfaceType => interfaceType.IsClosedTypeOf(typeof(IEventHandler<>))))
				.InstancePerLifetimeScope();
			builder.Register(a => new LoanApplicationContext()).InstancePerLifetimeScope();

			base.Load(builder);
		}
	}
}