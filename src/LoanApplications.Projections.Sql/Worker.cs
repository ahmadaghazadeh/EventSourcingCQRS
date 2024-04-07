using System.Text;
using EventStore.ClientAPI;
using Framework.Core;
using Framework.Core.Events;
using LoanApplications.Domain.Contracts;
using LoanApplications.Projections.Sql.Framework;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

namespace LoanApplications.Projections.Sql
{
	public class Worker : BackgroundService
	{
		private readonly ILogger<Worker> _logger;
		//private readonly IOptions<EventStoreConfig> _config;
		private readonly ICursor _cursor;
		private readonly IEventTypeResolver _typeResolver;
		private readonly IEventBus _eventBus;
		public Worker(ILogger<Worker> logger, ICursor cursor, IEventTypeResolver typeResolver, IEventBus eventBus)
		{
			_logger = logger;
			//_config = config;
			_cursor = cursor;
			_typeResolver = typeResolver;
			_eventBus = eventBus;
		}

		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{
			_typeResolver.AddTypesFromAssembly(typeof(LoanRequested).Assembly);
			var conn = EventStoreConnection.Create(new Uri("tcp://admin:changeit@localhost:1113"));
			await conn.ConnectAsync();

			conn.SubscribeToAllFrom(
				_cursor.CurrentPosition(),
				CatchUpSubscriptionSettings.Default,
				EventAppeared);
		}

		private async Task EventAppeared(EventStoreCatchUpSubscription arg1, ResolvedEvent @event)
		{
			try
			{
				if (!@event.OriginalEvent.EventType.StartsWith("$"))
				{
					_logger.LogInformation($"Event Appeared : {@event.OriginalEvent.EventType}");

					//TODO: consider using domain event factory
					var type = _typeResolver.GetType(@event.OriginalEvent.EventType);
					if (type != null)
					{
						var body = Encoding.UTF8.GetString(@event.OriginalEvent.Data);
						var domainEvent = (JsonConvert.DeserializeObject(body, type));

						await _eventBus.Publish((dynamic)domainEvent);      //In-Memory
					}
					else
					{
						_logger.LogWarning($"type '{@event.OriginalEvent.EventType}' not found !");
					}
				}
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.ToString());
				throw;
			}
		}
	}
}
